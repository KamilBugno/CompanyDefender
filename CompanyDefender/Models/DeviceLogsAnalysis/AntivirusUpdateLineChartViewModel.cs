using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDefender.Models.DeviceLogsAnalysis
{
    public class AntivirusUpdateLineChartViewModel
    {
        public string[] Labels;
        public int[] Data;

        public AntivirusUpdateLineChartViewModel(string[] labels, int[] data)
        {
            Labels = labels;
            Data = data;
        }
    }
}