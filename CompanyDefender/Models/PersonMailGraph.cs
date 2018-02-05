using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDefender.Models
{
    public class PersonMailGraph
    {
        public int Id { set; get; }
        public string Label { set; get; }
        
        public PersonMailGraph(int id, string label)
        {
            Id = id;
            Label = label;
        }
    }
}