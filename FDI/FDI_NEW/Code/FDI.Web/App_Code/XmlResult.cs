using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace FDI.Common
{
    public class XmlResult: ActionResult
    {
        private readonly object _data;
        private readonly string _filename;

        public XmlResult(object data,string filename)
        {
            _data = data;
            _filename = filename;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (_data != null)
            {
                var sb = new StringBuilder();
                var stringWriter = new StringWriter(sb);
                var xmlWriter = new XmlTextWriter(stringWriter);
                var serializer = new XmlSerializer(_data.GetType());
                serializer.Serialize(xmlWriter, _data);
                var document = new XmlDocument();
                document.LoadXml(stringWriter.ToString());
                var decl = document.FirstChild as XmlDeclaration;
                if (decl != null)
                {
                    decl.Encoding = "utf-8";
                }
                context.HttpContext.Response.Charset = "utf-8";
                context.HttpContext.Response.ContentType = "text/xml";
                context.HttpContext.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", _filename));
                context.HttpContext.Response.BinaryWrite(Encoding.UTF8.GetBytes(document.InnerXml));
                context.HttpContext.Response.End();
            }
        }
    }
}