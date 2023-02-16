using EmployeeAppCore.Core.IServices;
using EmployeeAppCore.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Management.Controllers
{
    public class EmployeeController : Controller
    {
        private IServices _Iservices;

        #region declaration
        public EmployeeController(IServices Iservices)
        {
            _Iservices = Iservices;
        }
        #endregion

        #region Empolyee Log In Details Get And Post method
        public IActionResult WorkerLogInPage()
        {
            TempData["isLoginPage"] = true;
            return View();
        }
        [HttpPost]
        public IActionResult Login(LogIn_Details userNames)
        {
             
            if (userNames != null)
            {
                var LogIn = _Iservices.LoginDetails(userNames);
                if(LogIn!=null && LogIn.UserId>0 && LogIn.IsAdminUser)
                {
                    HttpContext.Session.SetString("UserId", LogIn.UserId.ToString());
                    return RedirectToAction("ListAllEmpolyeeDetail");
                    
                }
                else if (LogIn != null && LogIn.UserId > 0 && !LogIn.IsAdminUser)
                {
                    HttpContext.Session.SetString("UserId", LogIn.UserId.ToString());
                    return RedirectToAction("ListParticularEmpolyeeDetail", new { userId = LogIn.UserId });
                }
            }
            
            return RedirectToAction("WorkerLogInPage");
        }
        #endregion

        #region Update the Loction and user detail for dropdwon
        public IActionResult GetEmpolyeeDetails(int EmpId)
        {
            List<WorkerLocation> dropdown = new List<WorkerLocation>();
            dropdown = _Iservices.GetLocations();
            if (dropdown != null && dropdown.Count > 1)
            {
                ViewBag.DropdwonButton = dropdown;
            }
            List<LogIn_Details> User = new List<LogIn_Details>();
            User = _Iservices.GetUser();
            if (User != null && User.Count > 1)
            {
                ViewBag.User = User;
            }
            if (EmpId >0)
            {
                var Edit = _Iservices.EnterDataInEdit(EmpId);
                return View(Edit);
            }
            else
            {
                return View();
            }           
        }
        #endregion

        #region Save And Update the Empolyee Details
        [HttpPost]
        public IActionResult SaveandUpdateEmpolyeeDetails(EmployeeDetails Empdata)
        {
            if(Empdata != null && Empdata.UserId>0)
            {               
                _Iservices.AddandEditEmpolyeeDetails(Empdata);
                return RedirectToAction("ListParticularEmpolyeeDetail", new { userId = Empdata.UserId });                
            }
            else
            {
                _Iservices.AddandEditEmpolyeeDetails(Empdata);
                return RedirectToAction("ListAllEmpolyeeDetail");
            }            
        }
        #endregion

        #region  ALl Empolyee Details Shown
        public IActionResult ListAllEmpolyeeDetail()
        {
            List<EmployeeDetails> ListData = new List<EmployeeDetails>();
             ListData = _Iservices.ListEmpolyeeDetails();
            int userId = ListData.Select(x => x.UserId).FirstOrDefault();            
            ViewBag.userid = userId;            
            return View(ListData);
        }
         #endregion

        #region Employee Details Shown 
        public IActionResult ListParticularEmpolyeeDetail(int userId)
        {
            var List = _Iservices.ListDetails(userId);
            return View(List);
        }
        #endregion

        #region Delete The Empolyee Details
        public IActionResult DeleteDetails(int EmpId)
        {
            if(EmpId > 0)
            {
                 _Iservices.DeleteEmpolyeeDetails(EmpId);
                return RedirectToAction("ListAllEmpolyeeDetail");
            }
            else
            {
                return View();
            }
        }
#endregion

        #region Enable a Edit Option
        public IActionResult EnableUpdateOption(int EmpId)
        {
            if (EmpId > 0)
            {
                 _Iservices.UpdateOption(EmpId);
                return RedirectToAction("ListAllEmpolyeeDetail");
            }
            else
            {
                return View();
            }
        }
#endregion

        #region Details for partial view
        public IActionResult _ViewDetails(int EmpId)
        {            
             var List=   _Iservices.ViewInfo(EmpId);
                return View(List);         

        }
        #endregion
    }
}
