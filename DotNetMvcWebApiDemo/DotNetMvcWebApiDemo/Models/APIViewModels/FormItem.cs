using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DotNetMvcWebApiDemo.Models.APIViewModels
{
    public class FormItem
    {
        public FormItem() { }
        public string name { get; set; }
        public byte[] data { get; set; }
        public string fileName { get; set; }
        public string mediaType { get; set; }
        public string value { get { return Encoding.Default.GetString(data); } }
        public bool isAFileUpload { get { return !String.IsNullOrEmpty(fileName); } }
    }
}