using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Models;
using System.Data.Entity;
using Test.ViewModels;
using System.Net;

namespace Test.Controllers
{
    public class ActivityController : Controller
    {
        JoinFunEntities db = new JoinFunEntities();
        // GET: Activity
        public ActionResult Index(string actClassId="cls002")
        {
            if (actClassId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.actClassName = db.Activity_Class.Where(m => m.actClassId == actClassId).FirstOrDefault().actClassName;
            ViewBag.actClassId = actClassId;
            //var mem = (from a in db.Join_Fun_Activities
            //            from c in db.Member
            //            where a.hostId == c.memId
            //            select c).ToList();
            //var mem = db.Member.ToList();
            ActClass classList = new ActClass()
            {
                ActivityList = db.Join_Fun_Activities.Where(m => m.actClassId == actClassId).ToList(),
                ClassList = db.Activity_Class.ToList()
            };
            
            //Member join = new Member();
            //string id = join.memId;
            //ViewBag.host = act1.Select(m => m.memNick).FirstOrDefault();
            return View(classList);
        }

        public ActionResult Create()
        {
            if (Session["Account"] == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Activity_Class = new SelectList(db.Activity_Class, "actClassId", "actClassName");
            ViewBag.Age_Restriction = new SelectList(db.Age_Restriction, "serial", "age");
            ViewBag.Gender_Restriction = new SelectList(db.Gender_Restriction, "genderSerial", "gender");
            ViewBag.People_Restriction = new SelectList(db.People_Restriction, "peoSerial", "PeoRestriction");
            ViewBag.Budget_Restriction = new SelectList(db.Budget_Restriction, "BudgetNo", "Budget");
            ViewBag.Payment_Restriction = new SelectList(db.Payment_Restriction, "paymentSerial", "payment");
            ViewBag.County = new SelectList(db.County, "CountyNo", "CountyName");
            ViewBag.District = new SelectList(db.District, "DistrictSerial", "DistrictName");
            List<SelectListItem> list = new List<SelectListItem>();
            list.AddRange(new[] {
                new SelectListItem() { Text = "是", Value = "True", Selected = true },
                new SelectListItem() { Text = "否", Value = "False", Selected = false },
            });
            ViewBag.Drop = list;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Join_Fun_Activities act, FormCollection form)
        {
            string clsValue = form["Activity_Class"].ToString();
            short age = Int16.Parse(form["Age_Restriction"].ToString());
            short people = Int16.Parse(form["People_Restriction"].ToString());
            short budget = Int16.Parse(form["Budget_Restriction"].ToString());
            short payment = Int16.Parse(form["Payment_Restriction"].ToString());
            short county = Int16.Parse(form["County"].ToString());
            short district = Int16.Parse(form["District"].ToString());
            bool drop = Convert.ToBoolean(form["Drop"].ToString());
            string actId = db.Database.SqlQuery<string>("Select dbo.GetActId()").FirstOrDefault();
            act.actId = actId;
            act.actClassId = clsValue;
            act.ageRestrict = age;
            act.maxNumPeople = people;
            act.maxBudget = budget;
            act.paymentTerm = payment;
            act.actCounty = county;
            act.actDistrict = district;
            act.acceptDrop = drop;
            db.Join_Fun_Activities.Add(act);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            if (Session["Account"] == null)
            {
                return RedirectToAction("Index");
            }
            var act = db.Join_Fun_Activities.Where(m => m.actId == id).FirstOrDefault();
            ViewBag.Activity_Class = new SelectList(db.Activity_Class, "actClassId", "actClassName", act.actClassId);
            ViewBag.Age_Restriction = new SelectList(db.Age_Restriction, "serial", "age", act.ageRestrict);
            ViewBag.Gender_Restriction = new SelectList(db.Gender_Restriction, "genderSerial", "gender", act.gender);
            ViewBag.People_Restriction = new SelectList(db.People_Restriction, "peoSerial", "PeoRestriction", act.maxNumPeople);
            ViewBag.Budget_Restriction = new SelectList(db.Budget_Restriction, "BudgetNo", "Budget", act.maxBudget);
            ViewBag.Payment_Restriction = new SelectList(db.Payment_Restriction, "paymentSerial", "payment");
            ViewBag.County = new SelectList(db.County, "CountyNo", "CountyName", act.actCounty);
            ViewBag.District = new SelectList(db.District, "DistrictSerial", "DistrictName", act.actDistrict);
            List<SelectListItem> list = new List<SelectListItem>();
            list.AddRange(new[] {
                new SelectListItem() { Text = "是", Value = "True", Selected = true },
                new SelectListItem() { Text = "否", Value = "False", Selected = false },
            });
            ViewBag.Drop = new SelectList(list, "Value", "Text", act.acceptDrop);

            return View(act);
        }

        [HttpPost]
        public ActionResult Edit(string id, FormCollection form)
        {
            var act = db.Join_Fun_Activities.Where(m => m.actId == id).FirstOrDefault();
            string clsValue = form["Activity_Class"].ToString();
            short age = Int16.Parse(form["Age_Restriction"].ToString());
            short people = Int16.Parse(form["People_Restriction"].ToString());
            short budget = Int16.Parse(form["Budget_Restriction"].ToString());
            short payment = Int16.Parse(form["Payment_Restriction"].ToString());
            short county = Int16.Parse(form["County"].ToString());
            short district = Int16.Parse(form["District"].ToString());
            bool drop = Convert.ToBoolean(form["Drop"].ToString());
            act.actClassId = clsValue;
            act.ageRestrict = age;
            act.maxNumPeople = people;
            act.maxBudget = budget;
            act.paymentTerm = payment;
            act.actCounty = county;
            act.actDistrict = district;
            act.acceptDrop = drop;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            var act = db.Join_Fun_Activities.Where(m => m.actId == id).FirstOrDefault();
            db.Join_Fun_Activities.Remove(act);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}