using DotNetMvcWebApiDemo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Management;

namespace DotNetMvcWebApiDemo.Controllers
{
    [RoutePrefix("api/Test")]
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("GetPerson")]
        public List<Person> GetPerson()
        {
            var temp = new List<Person> 
            {
                new Person
                {
                   Age = 100,
                   Name = "皮卡丘"
                },
                new Person
                {
                   Age = 10,
                   Name = "小火龍"
                },
                new Person
                {
                   Age = 101,
                   Name = "妙蛙種子"
                },
            };
            return temp;
        }

        [HttpGet]
        [Route("GetPerson123")]
        public List<Person> GetPerson2()
        {
            var temp = new List<Person>
            {
                new Person
                {
                   Age = 123,
                   Name = "皮卡丘"
                },
                new Person
                {
                   Age = 123,
                   Name = "小火龍"
                },
                new Person
                {
                   Age = 123,
                   Name = "妙蛙種子"
                },
            };
            return temp;
        }
    }
}
