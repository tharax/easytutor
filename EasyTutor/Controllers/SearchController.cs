using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<string> Subjects()
        {
            try
            {
                var db = Database.Open("Azure_Connect");

                var selectQueryString = "SELECT distinct names FROM topics";

                var query = db.Query(selectQueryString);
                var topics = new List<string>();
                foreach (var row in query)
                {
                    topics.Add(row[0]);
                }
                return topics;


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new List<string>();
        }


        // GET: Search/Tutors/Math
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public List<TopicThing> Tutors(string topic)
        {
            try
            {
                var db = Database.Open("Azure_Connect");

                var selectQueryString = "SELECT distinct u.userid, u.name FROM users u" +
                                        "left join userstopics ut on u.userid = ut.usersid" +
                                        "left join topics t on t.id = ut.topicsid" +
                                        "where topic ='" + topic + "'";

                var query = db.Query(selectQueryString);
                var topics = new List<TopicThing>();
                foreach (var row in query)
                {
                    topics.Add(new TopicThing { id = row[0], name = row[1]});
                }
                return topics;


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new List<TopicThing>();
        }
    }

    public class TopicThing
    {
        public string id;
        public string name;
    }
}
