using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDefender.Models
{
    public class PersonMail
    {
        public int Id { set; get; }
        public string Label { set; get; }
        
        public PersonMail(int id, string label)
        {
            Id = id;
            Label = label;
        }
    }
}