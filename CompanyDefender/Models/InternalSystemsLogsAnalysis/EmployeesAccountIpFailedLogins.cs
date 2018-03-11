using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDefender.Models.InternalSystemsLogsAnalysis
{
    public class EmployeesAccountIpFailedLogins
    {
        public string ip { get; set; }
        public int number_of_failed_login { get; set; }
    }
}