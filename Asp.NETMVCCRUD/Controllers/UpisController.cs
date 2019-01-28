using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp.NETMVCCRUD.Models;
using System.Data.Entity;
using Asp.NETMVCCRUD.ViewModel;

namespace Asp.NETMVCCRUD.Controllers
{
    public class UpisController : Controller
    {
        //
        // GET: /Upis/
        public ActionResult Index()
        {
            FakultetEntities obj = new FakultetEntities();
            var mymodel = new multipleData();
            mymodel.Student = obj.Studentis.ToList();
            mymodel.Kolegij = obj.Kolegijis.ToList();
            return View(mymodel);
            //return View();
        }

        //public ActionResult MultiData()
        //{
        //    FakultetEntities obj = new FakultetEntities();
        //    var mymodel = new multipleData();
        //    mymodel.Student = obj.Studentis.ToList();
        //    mymodel.Kolegij = obj.Kolegijis.ToList();
        //    return View(mymodel);
        //}


        public ActionResult GetData()
        {        

            using (FakultetEntities db = new FakultetEntities())
            {
                List<Upisi> empList = db.Upisis.ToList<Upisi>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            FakultetEntities sti = new FakultetEntities();
            List<Studenti> stiList = sti.Studentis.ToList();
            ViewBag.ListaStudenataIme = new SelectList(stiList, "Ime", "Ime");

            FakultetEntities stp = new FakultetEntities();
            List<Studenti> stpList = stp.Studentis.ToList();
            ViewBag.ListaStudenataPrezime = new SelectList(stpList, "Prezime", "Prezime");

            FakultetEntities kol = new FakultetEntities();
            List<Kolegiji> kolList = kol.Kolegijis.ToList();
            ViewBag.ListaKolegija = new SelectList(kolList, "Naziv", "Naziv");

  


            if (id == 0)
                return View(new Upisi());
            else
            {
                using (FakultetEntities db = new FakultetEntities())
                {
                    return View(db.Upisis.Where(x => x.IdUpis == id).FirstOrDefault<Upisi>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(Upisi emp)
        {
            using (FakultetEntities db = new FakultetEntities())
            {
                if (emp.IdUpis == 0)
                {
                    db.Upisis.Add(emp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Uspjesno spremljeno" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Uspjesno editirano" }, JsonRequestBehavior.AllowGet);
                }
            }


        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (FakultetEntities db = new FakultetEntities())
            {
                Upisi emp = db.Upisis.Where(x => x.IdUpis == id).FirstOrDefault<Upisi>();
                db.Upisis.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Uspjesno obrisano" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}