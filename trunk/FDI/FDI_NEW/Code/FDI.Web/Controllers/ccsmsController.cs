using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Code.Web.Controllers
{
    public class ccsmsController : Controller
    {
        //[HttpPost]
        public ActionResult json()
        {
            var data = new VANSI();
            var request = HttpUtility.UrlDecode(Request.Form.ToString());
            //var result = JsonConvert.DeserializeObject<ObjJson>(content);
            try
            {
                var obj = JsonConvert.DeserializeObject<VANSI>(request);
                data.Domain = obj.Domain;
                data.Messages = obj.Messages;
                data.Stop = obj.Stop;
                data.UrlRedicrect = "";

                if (obj.Domain == "adminfdi.local")
                    data.Stop = true;

                return Json(data);
            }
            catch (Exception)
            {
                ViewBag.Nam = "";
                return View();
            }
        }

        //[HttpPost]
        public ActionResult Jsonpost()
        {
            var w = new WebClient();
            var js = new JavaScriptSerializer();
            var jsonString = js.Serialize(GetData());
            w.Encoding = Encoding.UTF8;
            w.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            var data = w.UploadString("http://adminfdi.local/ccsms/json", "POST", jsonString);

            var obj = JsonConvert.DeserializeObject<VANSI>(data);
            return Json(obj);
            
            //ViewBag.Name = data;
            //return PartialView();
        }

        public static VANSI GetData()
        {
            return new VANSI
            {
                Domain = "adminfdi.local",
                Messages = "",
                Stop = false,
                UrlRedicrect = ""
            };
        }
    }

    public class VANSI
    {
        public string Domain { get; set; }
        public string Messages { get; set; }
        public bool Stop { get; set; }
        public string UrlRedicrect { get; set; }
    }

}
