using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web.Http;
using System.Web.Http.Cors;
using WebMatrix.Data;

namespace EasyTutor.Controllers
{
    
    public class GetTutorsController : ApiController
    {
        // GET api/values
        [EnableCors("*", "*", "*")]
        public ListOfUsers Get()
        {
            try
            {
                var db = Database.Open("Azure_Connect");

                var selectQueryString = "SELECT Name, Bio, Email, Phone, Location, Image FROM Users";

                var query = db.Query(selectQueryString);
                var objectToSerialize = new ListOfUsers {Users = new List<User>()};
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
                Console.WriteLine(e);
            }
            return new ListOfUsers();
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
    public class ListOfUsers
    {
        [DataMember]
        public List<User> Users { get; set; }
    }
}
