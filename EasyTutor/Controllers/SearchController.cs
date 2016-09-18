using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using WebMatrix.Data;

namespace EasyTutor.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search/Subjects/
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public RootObject2 Subjects()
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
                    result.Topics.Add(new Topic() {name = row[0]} );
                }
                return result;


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new RootObject2();
        }


        // GET: Search/Tutors/Math
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public RootObject Tutors(string topic)
        {
            try
            {
                var db = Database.Open("Azure_Connect");

                var selectQueryString = "SELECT distinct u.name FROM users u" +
                                        "left join userstopics ut on u.userid = ut.usersid" +
                                        "left join topics t on t.id = ut.topicsid" +
                                        "where t.name ='" + topic + "'";

                var query = db.Query(selectQueryString);
                var result = new RootObject();
                result.Users = new List<User>();
                foreach (var row in query)
                {
                    result.Users.Add(new User {  Name = row[0]});
                }
                
                return result;


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
        public string name { get; set; }
    }

    [DataContract]
    public class RootObject2
    {
        [DataMember]
        public List<Topic> Topics { get; set; }
    }

}
