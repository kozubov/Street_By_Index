using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DZ1.Models;

namespace DZ1.Controllers
{
    public class HomeController : Controller
    {
        private Singelton singelton = Singelton.Instens;
        // GET: Home
        [HandleError(ExceptionType = typeof(Exception), View = "ServerError")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexTable()
        {
            try
            {
                string str = singelton.Client("index");
                singelton.Index_Name(str);
                return View(singelton.IndeList);
            }
            catch (Exception e)
            {
                return View("ServerError");
            }

        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Index");
            }
            var index = singelton.IndeList.Find(z => z.Id == id);
            if (index == null)
            {
                return View("Index");
            }

            ViewBag.Name_index = index.Name_Index;
            string str = singelton.Client(index.Name_Index);
            singelton.Street_Name(str);
            return View(singelton.StreetList);
        }

        [HttpGet]
        public ActionResult IndexSearch()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IndexSearch(Index ind)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    string str = singelton.Client(ind.Name_Index);
                    singelton.Street_Name(str);
                    return RedirectToAction("DetailedSearch", ind);
                }
                catch (Exception e)
                {
                    return View("ServerError");
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult DetailedSearch(Index ind)
        {
            ViewBag.Name_index = ind.Name_Index;
            return View(singelton.StreetList);
        }

        public ActionResult ServerError()
        {
            return View();
        }
    }
}