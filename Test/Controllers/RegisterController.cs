using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        JoinFunEntities db = new JoinFunEntities();

        public ActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterAdmin(string admId, string admAcc, string admPass, string admNick)
        {
            Administrator adm = new Administrator();
            //random salt
            string salt = Guid.NewGuid().ToString();

            byte[] passwordAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(admPass + salt);
            byte[] hashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAndSaltBytes);
            string hashString = Convert.ToBase64String(hashBytes);
            admId = db.Database.SqlQuery<string>("Select dbo.GetAdmId()").FirstOrDefault();
            adm.admId = admId;
            adm.admAcc = admAcc;
            adm.admSalt = salt;
            adm.admPass = hashString;
            adm.admNick = admNick;
            db.Administrator.Add(adm);
            db.SaveChanges();
            return View();
        }

        public ActionResult RegisterMem()
        {
            ViewBag.County = new SelectList(db.County, "CountyNo", "CountyName");
            ViewBag.District = new SelectList(db.District, "DistrictSerial", "DistrictName");
            List<SelectListItem> list = new List<SelectListItem>();
            list.AddRange(new[] {
                new SelectListItem() { Text = "男", Value = "M", Selected = true },
                new SelectListItem() { Text = "女", Value = "F", Selected = false },
            });
            ViewBag.Sex = new SelectList(list, "Value", "Text");
            return View();
        }

        [HttpPost]
        public ActionResult RegisterMem(Member newMember, FormCollection form, string Account, string Password)
        {
            Acc_Pass newAccount = new Acc_Pass();
            //random salt
            string salt = Guid.NewGuid().ToString();
            DateTime dt = DateTime.Now;

            byte[] passwordAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(Password + salt);
            byte[] hashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAndSaltBytes);
            string hashString = Convert.ToBase64String(hashBytes);
            newAccount.Account = Account;
            newAccount.Password = hashString;
            //pass.Account = Account;
            newAccount.Salt = salt;
            //pass.Password = hashString;
            string memId = db.Database.SqlQuery<string>("Select dbo.GetMemId()").FirstOrDefault();
            newAccount.memId = memId;
            newMember.memId = memId;
            short county = Int16.Parse(form["County"].ToString());
            short district = Int16.Parse(form["District"].ToString());
            string sex = form["Sex"].ToString();
            newMember.memCounty = county;
            newMember.memDistrict = district;
            newMember.timeReg = dt;
            newMember.Sex = sex;

            ViewBag.Account = Account;
            ViewBag.Password = Password;

            db.Member.Add(newMember);
            db.Acc_Pass.Add(newAccount);
            db.SaveChanges();
            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            //{

            //    throw ex;
            //}


            return RedirectToAction("Index", "Activity");
        }
    }
}