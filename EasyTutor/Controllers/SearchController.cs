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
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public List<string> Index()
        {
            try
            {
                var db = Database.Open("Azure_Connect");

                var selectQueryString = "SELECT distinct topics FROM Users";

                var query = db.Query(selectQueryString);
                var topics = new List<string>();
                foreach (var row in query)
                {
                    topics.Add(row[0]);
                }
                //var output = JsonConvert.SerializeObject(objectToSerialize);
                return topics;


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new List<string>();
        }

        // GET: Search/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Search/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Search/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Search/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Search/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Search/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Search/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
