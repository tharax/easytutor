using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Helpers;
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
        public RootObject Get()
        {
            try
            {
                var db = Database.Open("Azure_Connect");

                var selectQueryString = "SELECT TOP 100 * FROM Users";

                var query = db.Query(selectQueryString);
                var objectToSerialize = new RootObject();
                objectToSerialize.Users = new List<User>();
                foreach (var row in query)
                {
                    objectToSerialize.Users.Add(new User() {Name = row[0]});
                }
                //var output = JsonConvert.SerializeObject(objectToSerialize);
                return objectToSerialize;
                

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new RootObject();
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

    [DataContract]
    public class User
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Bio { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Location { get; set; }
        [DataMember]
        public string Image { get; set; }

    }

    [DataContract]
    public class RootObject
    {
        [DataMember]
        public List<User> Users { get; set; }
    }
}
