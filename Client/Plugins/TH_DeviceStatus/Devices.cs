﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TH_Configuration;
using TH_Global.Functions;

namespace TH_StatusTable
{
    public partial class StatusTable
    {

        List_Functions.ObservableCollectionEx<DeviceInfo> _deviceInfos;
        /// <summary>
        /// Collection of TH_Configuration.Configuration objects that represent the devices
        /// </summary>
        public List_Functions.ObservableCollectionEx<DeviceInfo> DeviceInfos
        {
            get
            {
                if (_deviceInfos == null)
                    _deviceInfos = new List_Functions.ObservableCollectionEx<DeviceInfo>();
                return _deviceInfos;
            }

            set
            {
                _deviceInfos = value;
            }
        }

        public void AddDevice(Configuration config)
        {
            var info = new DeviceInfo();
            info.Configuration = config;
            LoadManufacturerLogo(config.FileLocations.Manufacturer_Logo_Path, info);
            DeviceInfos.Add(info);

            DeviceInfos.Sort();
        }

    }
}
