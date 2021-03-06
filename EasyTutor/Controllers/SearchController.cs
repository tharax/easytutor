﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web.Http;
using System.Web.Http.Cors;
using WebMatrix.Data;

namespace EasyTutor.Controllers
{
    public class SearchController : ApiController
    {
        // GET: api/search
        [EnableCors("*", "*", "*")]
        public RootObject2 Get()
        {
            try
            {
                var db = Database.Open("Azure_Connect");

                var selectQueryString = "SELECT distinct name FROM topics";

                var query = db.Query(selectQueryString);
                var result = new RootObject2();
                result.Topics = new List<Topic>();
                foreach (var row in query)
                {
                    result.Topics.Add(new Topic() {Name = row[0]} );
                }
                return result;


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new RootObject2();
        }


        // GET: api/search/string
        [EnableCors("*", "*", "*")]
        public RootObject GetTutors(string id)
        {
            var error = "";
            var selectQueryString = "";
            try
            {
                var db = Database.Open("Azure_Connect");
                
                selectQueryString = " SELECT u.name, u.bio, u.email, u.phone, u.location, u.image FROM users u " +
                                        " left join userstopics ut on u.userid = ut.usersid " +
                                        " left join topics t on t.id = ut.topicsid " +
                                        " where t.name = @0 ";

                var query = db.Query(selectQueryString, id);
                var objectToSerialize = new RootObject();
                objectToSerialize.Users = new List<User>();
                foreach (var row in query)
                {
                    objectToSerialize.Users.Add(new User()
                    {
                        
                        Name = row[0],
                        Bio = row[1],
                        Email = row[2],
                        Phone = row[3],
                        Location = row[4],
                        Image = row[5]
                    });
                }
                
                return objectToSerialize;


            }
            catch (Exception e)
            {
                error = e.ToString();
                Console.WriteLine(e);
            }
            return new RootObject() {Users = new List<User>() {new User() {Name = id, Bio = error, Location = selectQueryString } } };

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
