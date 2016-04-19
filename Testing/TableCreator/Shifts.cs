﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using TH_Database;
using TH_Configuration;
using TH_ShiftTable;

namespace TableCreator
{
    public static class Shifts
    {
        
        public static void Create(string configPath, string[] args)
        {
            DatabasePluginReader.ReadPlugins();

            var config = Configuration.Read(configPath);
            if (config != null)
            {
                Global.Initialize(config.Databases_Server);

                DateTime start = DateTime.Now.Subtract(TimeSpan.FromDays(1));
                DateTime end = DateTime.Now;

                if (args.Length > 2)
                {
                    DateTime.TryParse(args[2], out start);
                    DateTime.TryParse(args[3], out end);
                }

                var shiftsPlugin = new ShiftTable();
                var genEventsPlugin = new TH_GeneratedData.GeneratedEvents.GeneratedData();
                genEventsPlugin.Initialize(config);
                shiftsPlugin.Initialize(config);
                AddRows(config, shiftsPlugin, start, end);
            }
            else Console.WriteLine("ERROR :: Configuration Not Found");
        }

        public static ShiftRowInfo Get(Shift shift, Segment segment, DateTime timestamp, TH_GeneratedData.GeneratedEvents.GeneratedEventsConfiguration gec)
        {
            var info = new ShiftRowInfo();
            info.shift = shift.name;
            info.date = new ShiftDate(timestamp);
            info.id = timestamp.ToString("yyyyMMdd") + "_" + shift.id.ToString("00") + "_" + segment.id.ToString("00");
            info.totalTime = 300;
            info.start = segment.beginTime;
            info.end = segment.endTime;
            info.start_utc = segment.beginTime.ToUTC();
            info.end_utc = segment.endTime.ToUTC();
            info.type = segment.type;
            info.segmentId = segment.id;

            var duration = segment.endTime - segment.beginTime;


            foreach (var e in gec.Events)
            {
                int[] seconds = Tools.GetRandomNumbers(Convert.ToInt32(duration.TotalSeconds), e.Values.Count + 1);
                int secondsIndex = 0;

                foreach (var value in e.Values)
                {
                    var gInfo = new GenEventRowInfo();
                    gInfo.columnName = TH_ShiftTable.Tools.FormatColumnName(e, value);

                    gInfo.seconds = seconds[secondsIndex];
                    secondsIndex += 1;

                    info.genEventRowInfos.Add(gInfo);
                }

                var defaultInfo = new GenEventRowInfo();
                defaultInfo.columnName = TH_ShiftTable.Tools.FormatColumnName(e.Name, 0, e.Default.Value);

                defaultInfo.seconds = seconds[secondsIndex];
                secondsIndex += 1;

                info.genEventRowInfos.Add(defaultInfo);
            }

            return info;
        }

        private static void AddRows(Configuration config, ShiftTable plugin, DateTime start, DateTime end)
        {
            var infos = new List<ShiftRowInfo>();

            var sc = ShiftConfiguration.Get(config);
            var gec = TH_GeneratedData.GeneratedEvents.GeneratedEventsConfiguration.Get(config);

            DateTime timestamp = start;

            while (timestamp < end)
            {
                foreach (var shift in sc.shifts)
                {
                    foreach (var segment in shift.segments)
                    {
                        var info = new ShiftRowInfo();
                        info.shift = shift.name;
                        info.date = new ShiftDate(timestamp);
                        info.id = timestamp.ToString("yyyyMMdd") + "_" + shift.id.ToString("00") + "_" + segment.id.ToString("00");
                        info.totalTime = 300;
                        info.start = segment.beginTime;
                        info.end = segment.endTime;
                        info.start_utc = segment.beginTime.ToUTC();
                        info.end_utc = segment.endTime.ToUTC();
                        info.type = segment.type;
                        info.segmentId = segment.id;

                        var duration = segment.endTime - segment.beginTime;


                        foreach (var e in gec.Events)
                        {
                            int[] seconds = Tools.GetRandomNumbers(Convert.ToInt32(duration.TotalSeconds), e.Values.Count + 1);
                            int secondsIndex = 0;

                            foreach (var value in e.Values)
                            {
                                var gInfo = new GenEventRowInfo();
                                gInfo.columnName = TH_ShiftTable.Tools.FormatColumnName(e, value);

                                gInfo.seconds = seconds[secondsIndex];
                                secondsIndex += 1;

                                info.genEventRowInfos.Add(gInfo);
                            }

                            var defaultInfo = new GenEventRowInfo();
                            defaultInfo.columnName = TH_ShiftTable.Tools.FormatColumnName(e.Name, 0, e.Default.Value);

                            defaultInfo.seconds = seconds[secondsIndex];
                            secondsIndex += 1;

                            info.genEventRowInfos.Add(defaultInfo);
                        }

                        infos.Add(info);
                    }
                }

                // Increment a day
                timestamp = timestamp.AddDays(1);
            }

            plugin.UpdateShiftRows(infos);
        }

    }
}
