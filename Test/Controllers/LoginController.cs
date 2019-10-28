using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers
{
    public class LoginController : Controller
    {
        JoinFunEntities db = new JoinFunEntities();
        //SqlConnection connector = new SqlConnection(ConfigurationManager.ConnectionStrings["JoinFunEntities_Sql"].ConnectionString);
        SqlConnection connector = new SqlConnection("data source = 127.0.0.1; initial catalog = JoinFun; integrated security = True; MultipleActiveResultSets=True;App=EntityFramework&quot;");
        // GET: Login
        public ActionResult Index()
        {
            var act = db.Join_Fun_Activities.OrderBy(m => m.actId).ToList();
            return View(act);
        }

        public ActionResult LoginMember()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginMember(string Account, string Password)
        {
            string sql = "select * from Acc_Pass where Account=@Account and Password=@Password";
            SqlCommand cmd = new SqlCommand(sql, connector);
            

            var result = db.Acc_Pass.Where(m => m.Account == Account).FirstOrDefault();
            string salt = result.Salt;
            byte[] passwordAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(Password + salt);
            byte[] hashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAndSaltBytes);
            string hashString = Convert.ToBase64String(hashBytes);

            cmd.Parameters.AddWithValue("@Account", Account);
            cmd.Parameters.AddWithValue("@Password", hashString);
            SqlDataReader reader;
            connector.Open();
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Session["Account"] = reader["Account"].ToString();
                Session["memId"] = reader["memId"].ToString();
                connector.Close();
                return RedirectToAction("Index", "Activity");
            }
            
            ViewBag.LoginError = "帳號或密碼錯誤!!";

            return View();
        }

        public ActionResult LogoutMember()
        {
            //Session.Clear();
            return RedirectToAction("LoginMember", "Login");
        }
    }
}