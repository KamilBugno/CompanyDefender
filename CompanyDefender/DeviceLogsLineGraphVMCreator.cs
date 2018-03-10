using CompanyDefender.Models.DeviceLogsAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDefender
{
    public class DeviceLogsLineGraphVMCreator
    {
        private DateTime startDate;
        private DateTime endDate;
        private List<AntivirusUpdateLineChartData> antivirusData;

        public DeviceLogsLineGraphVMCreator(List<AntivirusUpdateLineChartData> antivirusData,
            string startDate, string endDate)
        {
            this.startDate = DateTime.ParseExact(startDate, "yyyy-MM-dd", null);
            this.endDate = DateTime.ParseExact(endDate, "yyyy-MM-dd", null);
            this.antivirusData = antivirusData;
        }

        public LineChartViewModel CreateFromAntivirusUpdateData()
        {
            return new LineChartViewModel(CreateLabels(), CreateData());
        }

        private string[] CreateLabels()
        {
            var labels = new List<string>();
            for (DateTime date = startDate; date.Date <= endDate.Date; date = date.AddDays(1))
            {
                labels.Add(date.ToString("dd.MM"));
            }
            return labels.ToArray();
        }

        private int[] CreateData()
        {
            var data = new List<int>();
            for (DateTime date = startDate; date.Date <= endDate.Date; date = date.AddDays(1))
            {
                var dataInThisDay = antivirusData.Where(antivirusData => 
                    Int32.Parse(antivirusData.number_of_day) == date.Day).FirstOrDefault();

                if (dataInThisDay != default(AntivirusUpdateLineChartData)) 
                {
                    data.Add(Int32.Parse(dataInThisDay.number_of_updates));
                }
                else
                {
                    data.Add(0);
                }
            }

            return data.ToArray();
        }
    }
}