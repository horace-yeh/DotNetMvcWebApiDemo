using DotNetMvcWebApiDemo.DBHelper;
using DotNetMvcWebApiDemo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DotNetMvcWebApiDemo.Controllers
{
    // REF: DB https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks

    [RoutePrefix("api/DBTest")]
    public class DBTestController : ApiController
    {
        private readonly DapperTool dapperTool;

        public DBTestController()
        {
            this.dapperTool = new DapperTool();
        }

        [Route("StoreGetAll")]
        [HttpGet]
        public List<Store> StoreGetAll()
        {
            var sql = @"select top 5 * from [Sales].[Store]";
            var data = this.dapperTool.DapperQuery<Store>(sql, null);
            return data;
        }
    }
}
