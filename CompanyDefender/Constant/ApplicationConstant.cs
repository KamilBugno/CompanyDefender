using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDefender.Constant
{
    public static class ApplicationConstant
    {
        public static string urlService = "http://127.0.0.1:8529/_db/_system/foxx-service/";
        public static string uploadFileAction = "save-file/";
        public static string downloadFileAction = "download-file/";
        public static string searchMailsByBodyAction = "get-mails-by-body/";
        public static string searchMailsByAttachmentAction = "get-mails-by-attachment/";
        public static string getAllMails = "get-all-mails/";
        public static string getAntivirusDateForLineChart = "antivirus-line-chart/";
        public static string getAntivirusDateForPieChart = "antivirus-pie-chart/";
        public static string getPeopleWhoDoNotUpdateAntivirus = "antivirus-people-list/";
        public static string getPersonDetails = "correspondence-table-people/";
    }
}