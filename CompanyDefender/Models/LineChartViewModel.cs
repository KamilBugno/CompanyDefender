using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDefender.Models.DeviceLogsAnalysis
{
    public class LineChartViewModel
    {
        public string[] Labels;
        public int[] Data;

        public LineChartViewModel(string[] labels, int[] data)
        {
            Labels = labels;
            Data = data;
        }
    }
}