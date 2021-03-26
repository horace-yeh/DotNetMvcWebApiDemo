﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetMvcWebApiDemo.Models.APIViewModels
{
    public class CustomReData
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}