using CompanyDefender.Models.DeviceLogsAnalysis;
using CompanyDefender.Models.InternalSystemsLogsAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDefender
{
    public class FailedLoginsVMCreator
    {
        private List<EmployeesAccountFailedLogins> failedLogins;
        private List<string> labels;
        private List<int> data;

        public FailedLoginsVMCreator(List<EmployeesAccountFailedLogins> failedLogins)
        {
            this.failedLogins = failedLogins;
            labels = new List<string>();
            data = new List<int>();
        }

        public LineChartViewModel CreateFromEmployeesAccountFailedLogins()
        {
            TransformDataForLineChart();
            return new LineChartViewModel(labels.ToArray(), data.ToArray());
        }

        private void TransformDataForLineChart()
        {
            foreach (EmployeesAccountFailedLogins fl in failedLogins)
            {
                labels.Add(fl.name);
                data.Add(fl.number_of_failed_login);
            }
        }


    }
}