using CompanyDefender.Models.DeviceLogsAnalysis;
using CompanyDefender.Models.InternalSystemsLogsAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDefender
{
    public class FailedLoginsNameVMCreator
    {
        private List<EmployeesAccountNameFailedLogins> failedLogins;
        private List<string> labels;
        private List<int> data;

        public FailedLoginsNameVMCreator(List<EmployeesAccountNameFailedLogins> failedLogins)
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
            foreach (EmployeesAccountNameFailedLogins fl in failedLogins)
            {
                labels.Add(fl.name);
                data.Add(fl.number_of_failed_login);
            }
        }


    }
}