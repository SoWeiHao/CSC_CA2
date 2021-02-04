using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using MySql.Data.MySqlClient;
using dto = WebApplication2.Models;
using MongoDB.Driver;
using Microsoft.AspNetCore.Authorization;
namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private MySqlDatabase MySqlDatabase { get; set; }

        public HomeController(MySqlDatabase mySqlDatabase)
        {
            this.MySqlDatabase = mySqlDatabase;
        }
        public async Task<List<dto.Talent>> GetTasks()
        {
            var ret = new List<dto.Talent>();

            var cmd = this.MySqlDatabase.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM talent";

            using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                {
                    var t = new dto.Talent()
                    {
                        Id = reader.GetFieldValue<int>(0),
                        Bio = reader.GetFieldValue<string>(1),
                        Name = reader.GetFieldValue<string>(2),
                        Photo = reader.GetFieldValue<string>(3),
                        ShortName = reader.GetFieldValue<string>(4),
                        Reknown = reader.GetFieldValue<string>(5)
                    };
                    ret.Add(t);
                }
            return ret;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View(await this.GetTasks());
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize]
        public IActionResult UploadTalent()
        {
            return View();
        }
        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Subs()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
