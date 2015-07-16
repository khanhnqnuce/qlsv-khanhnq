using System;
using System.Linq;
using System.Web.Mvc;
using Validate.Common;
using Validate.Models;

namespace Validate.Controllers
{
    public class CustomerController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(CustomerModel custModel)
        {
            using (CCNEntities db = new CCNEntities())
            {
                tbl_Customer tblCustomer = (custModel.id == 0) ? new tbl_Customer() : db.tbl_Customer.Where(x => x.id == custModel.id).FirstOrDefault();
                tblCustomer.FirstName = custModel.FirstName;
                tblCustomer.LastName = custModel.LastName;
                tblCustomer.Address = custModel.Address;
                tblCustomer.ContactNo = custModel.ContactNo;
                tblCustomer.BookingDate = custModel.BookingDate.ToString();
                if (custModel.id == 0)
                {
                    db.tbl_Customer.Add(tblCustomer);
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public JsonResult ValidationForWeekEnds(CustomerModel custModel)
        {
            var dt = Convert.ToDateTime(custModel.BookingDate);
            custModel.BookingDate = dt.DayOfWeek.ToString();
            if (custModel.BookingDate.Contains("Saturday") || custModel.BookingDate.Contains("Sunday"))
            {
                return Json("We do not allow booking at Weekends", JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        } 
    }
}