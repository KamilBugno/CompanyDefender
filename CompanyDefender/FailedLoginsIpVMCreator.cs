using CompanyDefender.Models.DeviceLogsAnalysis;
using CompanyDefender.Models.InternalSystemsLogsAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDefender
{
    public class FailedLoginsIpVMCreator
    {
        private List<EmployeesAccountIpFailedLogins> failedLogins;
        private List<string> labels;
        private List<int> data;

        public FailedLoginsIpVMCreator(List<EmployeesAccountIpFailedLogins> failedLogins)
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
            foreach (EmployeesAccountIpFailedLogins fl in failedLogins)
            {
                labels.Add(fl.ip);
                data.Add(fl.number_of_failed_login);
            }
        }
    }
}