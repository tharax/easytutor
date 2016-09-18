﻿using System;
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
    public class SearchController : ApiController
    {

        // GET: api/search/english
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public RootObject Get(string topic)
        {
            try
            {
                var db = Database.Open("Azure_Connect");

                var selectQueryString = "SELECT u.name FROM users u" +
                                        "left join userstopics ut on u.userid = ut.usersid" +
                                        "left join topics t on t.id = ut.topicsid" +
                                        "where t.name ='" + topic + "'";

                var query = db.Query(selectQueryString);
                var objectToSerialize = new RootObject();
                objectToSerialize.Users = new List<User>();
                foreach (var row in query)
                {
                    objectToSerialize.Users.Add(new User
                    {
                        Name = row[0]
                        
                    });
                }
                
                return objectToSerialize;


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new RootObject();

        }
    }

    [DataContract]
    public class Topic
    {
        [DataMember]
        public string Name { get; set; }
    }

    [DataContract]
    public class RootObject2
    {
        [DataMember]
        public List<Topic> Topics { get; set; }
    }

}
