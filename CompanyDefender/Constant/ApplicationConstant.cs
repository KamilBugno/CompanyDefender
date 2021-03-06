﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDefender.Constant
{
    public static class ApplicationConstant
    {
        public static string urlFoxxService = "http://127.0.0.1:8529/_db/_system/foxx-service/";
        public static string urlGraphQLService = "http://127.0.0.1:8529/test3/?query=";
        public static string urlElasticSearchService = "http://127.0.0.1:9200";
        public static string elastcSearchIndex = "mail";
        public static string uploadFileAction = "save-file/";
        public static string downloadFileAction = "download-file/";
        public static string searchMailsByBodyAction = "get-mails-by-body/";
        public static string searchMailsByAttachmentAction = "get-mails-by-attachment/";
        public static string getAllMails = "get-all-mails/";
        public static string getMailByKey = "/get-mails-elastic-search/";
        public static string getAntivirusDateForLineChart = "antivirus-line-chart/";
        public static string getAntivirusDateForPieChart = "antivirus-pie-chart/";
        public static string getPeopleWhoDoNotUpdateAntivirus = "antivirus-people-list/";
        public static string getPersonDetails = "correspondence-table-people/";
        public static string getNameForEmployeesAccountFailedLogin = "internal-system-employees-accounts/";
        public static string getIpForEmployeesAccountFailedLogin = "internal-system-login-ip/";
    }
}