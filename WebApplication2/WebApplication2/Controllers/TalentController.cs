using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;

using MySql.Data.MySqlClient;
using WebApplication2.Models;
using dto = WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class TalentController : ApiController
    {
        private MySqlDatabase MySqlDatabase { get; set; }
        public TalentController(MySqlDatabase mySqlDatabase)
        {
            this.MySqlDatabase = mySqlDatabase;
        }


        public Boolean Upload(string name, string shortname, string photo, string reknown, string bio)
        {
            Boolean response = false;

            var cmd = this.MySqlDatabase.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO talent (name, shortname, photo, reknown, bio) VALUES ( @Name, @Shortname, @Photo, @Reknown, @Bio);";
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Shortname", shortname);
            cmd.Parameters.AddWithValue("@Photo", photo);
            cmd.Parameters.AddWithValue("@Reknown", reknown);
            cmd.Parameters.AddWithValue("@Bio", bio);

            var recs = cmd.ExecuteNonQuery();

            if (recs == 1)
                response = true;
            else
                response = false;

            return response;
        }
    }
}