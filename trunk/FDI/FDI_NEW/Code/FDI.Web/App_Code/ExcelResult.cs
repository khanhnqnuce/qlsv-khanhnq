using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace FDI.Common
{
    public class ExcelResult<T>:ActionResult
    {
        private readonly List<T> _data;
        private readonly string _filename;

        public ExcelResult(List<T> data, string filename)
        {
            _data = data;
            _filename = filename;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (_data != null)
            {
                var grid = new System.Web.UI.WebControls.GridView
                               {
                                   DataSource = from o in _data
                                                select o
                               };
                grid.DataBind();
                context.HttpContext.Response.ClearContent();
                context.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + _filename);
                context.HttpContext.Response.ContentType = "application/excel";
                var sw = new StringWriter();
                var htw = new HtmlTextWriter(sw);
                grid.RenderControl(htw);
                context.HttpContext.Response.Write(sw.ToString());
                context.HttpContext.Response.End();
            }
        }
    }
}