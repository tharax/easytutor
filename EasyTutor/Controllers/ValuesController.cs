﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using WebMatrix.Data;

namespace EasyTutor.Controllers
{
    
    public class ValuesController : ApiController
    {
        // GET api/values
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public string Get()
        {
            var db = Database.Open("easytutordb");
            var selectQueryString = "SELECT TOP 100 * FROM Users";
            
            var result = db.Query(selectQueryString);
            var json = "";
            foreach (var row in result)
            {
                json += row.ToString();
            }


            return json;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }

    public class User
    {
    }
}
