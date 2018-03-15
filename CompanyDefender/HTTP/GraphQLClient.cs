using CompanyDefender.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;

namespace CompanyDefender.HTTP
{
    public class GraphQLClient : Client
    {
        public String GetPersonWithDevicesNumber(string key)
        {
            var query = "{" +
                   "person(id: {key}){" +
                    " name, " +
                    " mail, " +
                    " department, " +
                    " devices_number, " +
                    " roles " +
                   " }}";

            query = Regex.Replace(query, "{key}", key);


            return GetAction(ApplicationConstant.urlGraphQLService, query);
        }

        public String GetPerson(string key)
        {
            var query = "{" +
                    "person(id: {key}){" +
                    " name, " +
                    " mail, " +
                    " department, " +
                    " roles " +
                   " }}";

            query = Regex.Replace(query, "{key}", key);

            return GetAction(ApplicationConstant.urlGraphQLService, query);
        }
    }
}