﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Media;

using System.Data;

using TH_Global;
using TH_Global.Functions;

namespace TH_StatusTable
{
    public class HourData : INotifyPropertyChanged
    {

        public HourData()
        {
            SegmentDatas = new SegmentData[12];
        }

        public string Title { get; set; }

        private double _totalSeconds;
        public double TotalSeconds
        {
            get { return _totalSeconds; }
            set
            {
                var val = _totalSeconds;
                _totalSeconds = value;
                if (val != _totalSeconds) NotifyChanged("TotalSeconds");
            }
        }


        private double _productionSeconds;
        public double ProductionSeconds
        {
            get { return _productionSeconds; }
            set
            {
                var val = _productionSeconds;
                _productionSeconds = value;
                if (val != _productionSeconds) NotifyChanged("ProductionSeconds");
            }
        }

        private double _idleSeconds;
        public double IdleSeconds
        {
            get { return _idleSeconds; }
            set
            {
                var val = _idleSeconds;
                _idleSeconds = value;
                if (val != _idleSeconds) NotifyChanged("IdleSeconds");
            }
        }

        private double _alertSeconds;
        public double AlertSeconds
        {
            get { return _alertSeconds; }
            set
            {
                var val = _alertSeconds;
                _alertSeconds = value;
                if (val != _alertSeconds) NotifyChanged("AlertSeconds");
            }
        }


        private int _status;
        public int Status
        {
            get { return _status; }
            set
            {
                var val = _status;
                _status = value;
                if (val != _status) NotifyChanged("Status");
            }
        }


        private SegmentData[] _segmentDatas;
        public SegmentData[] SegmentDatas
        {
            get { return _segmentDatas; }
            set
            {
                var val = _segmentDatas;
                _segmentDatas = value;
                if (val != _segmentDatas) NotifyChanged("SegmentDatas");
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
