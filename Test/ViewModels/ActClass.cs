using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test.Models;

namespace Test.ViewModels
{
    public class ActClass
    {  
        public List<Join_Fun_Activities> ActivityList { get; set; }
        public List<Activity_Class> ClassList { get; set; }
    }
}