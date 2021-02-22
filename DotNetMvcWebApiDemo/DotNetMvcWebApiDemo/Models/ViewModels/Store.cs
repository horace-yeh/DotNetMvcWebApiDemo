using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetMvcWebApiDemo.Models.ViewModels
{
    public class Store
    {
        public int BusinessEntityID { get; set; }
        public string Name { get; set; }
        public int SalesPersonID { get; set; }
        public string Demographics { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}