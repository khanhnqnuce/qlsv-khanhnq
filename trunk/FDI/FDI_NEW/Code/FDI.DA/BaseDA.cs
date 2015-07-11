using FDI.Base;
using FDI.Simple;
using FDI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects.SqlClient;

namespace FDI.DA
{
    public class ParramRequest
    {
        public int CurrentPage { get; set; }

        public int RowPerPage { get; set; }

        public string Keyword { get; set; }

        public List<string> SearchInField { get; set; }

        public string FieldSort { get; set; }

        public bool TypeSort { get; set; }

        public int CategoryID { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ProductName { get; set; }

        /// <summary>
        /// add by BienLV
        /// get other parram in browse
        /// </summary>
        public string OtherParram
        {
            get
            {
                var listSytemParram = new string[] { "CategoryID", "Page", "RowPerPage", "Keyword", "SearchIn", "Field", "FieldOption", "StartDate", "EndDate", "message" };
                return HttpContext.Current.Request.QueryString.AllKeys.Where(item => !listSytemParram.Contains(item)).Aggregate(string.Empty, (current, item) => current + (item + "=" + HttpContext.Current.Request[item] + "&"));
            }
        }

        public ParramRequest(HttpRequestBase request)
        {
            CategoryID = !string.IsNullOrEmpty(request["CategoryID"]) ? Convert.ToInt32(request["CategoryID"]) : 0;

            CurrentPage = !string.IsNullOrEmpty(request["Page"]) ? Convert.ToInt32(request["page"]) : 1;

            RowPerPage = !string.IsNullOrEmpty(request["RowPerPage"]) ? Convert.ToInt32(request["RowPerPage"]) : 10;

            ProductName = !string.IsNullOrEmpty(request["ProductName"]) ? request["ProductName"].Trim() : string.Empty;

            Keyword = !string.IsNullOrEmpty(request["Keyword"]) ? request["Keyword"].Trim() : string.Empty;

            FieldSort = !string.IsNullOrEmpty(request["Field"]) ? request["Field"].Trim() : string.Empty;

            TypeSort = !string.IsNullOrEmpty(request["FieldOption"]) && Convert.ToInt32(request["FieldOption"]) > 0;

            if (!string.IsNullOrEmpty(request["StartDate"])) StartDate = DateTime.ParseExact(request["StartDate"], "dd/MM/yyyy", null);

            if (!string.IsNullOrEmpty(request["EndDate"])) EndDate = DateTime.ParseExact(request["EndDate"], "dd/MM/yyyy", null);

            SearchInField = new List<string>();
            if (string.IsNullOrEmpty(request["SearchIn"])) return;
            var temp = request["SearchIn"];
            if (temp.IndexOf(',') > 0)
                SearchInField = temp.Split(',').ToList();
            else
                SearchInField.Add(temp);
        }

        public override string ToString()
        {
            return string.Format(OtherParram + "Keyword={0}&CategoryID={5}&SearchIn={4}&RowPerPage={1}&Field={2}&FieldOption={3}&Page=", Keyword, RowPerPage, FieldSort, (TypeSort) ? 1 : 0, string.Join(",", SearchInField), CategoryID);
        }

        public string GetCategoryString()
        {
            return string.Format(OtherParram + "Keyword={0}&SearchIn={4}&RowPerPage={1}&Field={2}&FieldOption={3}&Page=1&CategoryID=", Keyword, RowPerPage, FieldSort, (TypeSort) ? 1 : 0, string.Join(",", SearchInField));
        }

        public string ParramArr
        {
            get
            {
                return string.Format(OtherParram + "Keyword={0}&RowPerPage={1}&Field={2}&FieldOption={3}&Page=", Keyword, RowPerPage, FieldSort, (TypeSort) ? 1 : 0);
            }
        }

        public string SortUrl
        {
            get
            {
                return string.Format("FieldOption={2}&Keyword={0}&SearchIn={1}", Keyword, string.Join(",", SearchInField), (TypeSort) ? 1 : 0);
            }
        }
    }

    public class BaseDA : IDisposable
    {
        protected FDIEntities _FDIdb = new FDIEntities();
        public FDIEntities FDIDB
        {
            get
            {
                return _FDIdb;
            }
        }

        private readonly Paging _objPaging = new Paging();
        public int TotalRecord = 0;
        public string PathPaging { get; set; }
        public string PathPagingext { get; set; }
        //public int CurrentPage { get; set; }
        //public int PageSize { get; set; }

        public ParramRequest Request { get; set; }

        public List<PageItem> PageItems
        {
            get
            {
                return _objPaging.getPageItems(3, Request.CurrentPage, Request.RowPerPage, TotalRecord);
            }
        }

        public string HtmlPageing
        {
            get
            {
                return _objPaging.getHtmlPage(PathPaging + Request, PathPagingext, 3, Request.CurrentPage, Request.RowPerPage, TotalRecord);
            }
        }

        public string GridHtmlPage
        {
            get
            {
                return Pager.GetPage(PathPaging + Request, Request.CurrentPage, Request.RowPerPage, TotalRecord);
            }
        }

        #region cơ chế dọn rác
        public BaseDA()
        {

        }

        private bool _isDisposed;
        public void Free()
        {
            if (_isDisposed)
                throw new ObjectDisposedException("Object Name");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~BaseDA()
        {
            //Pass false as param because no need to free managed resources when you call finalize it
            //by GC itself as its work of finalize to manage managed resources.
            Dispose(false);
        }

        //Implement dispose to free resources
        protected virtual void Dispose(bool disposedStatus)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                _FDIdb.Dispose(); // Released unmanaged Resources
                if (disposedStatus)
                {
                    // Released managed Resources
                }
            }
        }
        #endregion

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<System_File> GetListFileByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.System_File where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Tự động kiểm tra xem file đã có trong db chưa -> Chưa có thì thêm vào và trả về cho đối tượng cần thêm
        /// </summary>
        /// <param name="fileItem"></param>
        /// <returns></returns>
        public System_File GetFileData(FileItem fileItem)
        {
            int fileLength = fileItem.Data.Length;
            var fileCheck = (from f in FDIDB.System_File where f.Name == fileItem.Name && SqlFunctions.DataLength(f.Data).Value == fileLength select f).FirstOrDefault();
            if (fileCheck == null) //Nếu chưa có thì thêm vào
            {
                fileCheck = new System_File
                                {
                                    Name = fileItem.Name,
                                    CreatedDate = DateTime.Now,
                                    Data = fileItem.Data,
                                    TypeID = getFileType(fileItem)
                                };
                FDIDB.System_File.Add(fileCheck);
                if (System.IO.File.Exists(fileItem.Url))
                    System.IO.File.Delete(fileItem.Url);
            }
            return fileCheck;
        }

        /// <summary>
        /// Lấy về iD của loại file
        /// </summary>
        /// <param name="fileItem"></param>
        /// <returns></returns>
        private int getFileType(FileItem fileItem)
        {
            using (var fileTypeDB = new FDIEntities())
            {
                var fileExt = MyBase.GetFileExtend(fileItem.Name);
                var fileType = (from t in fileTypeDB.System_FileType where t.Name == fileExt select t).FirstOrDefault();
                if (fileType == null)
                {
                    fileType = new System_FileType
                                   {
                                       Name = fileExt,
                                       Icon = string.Format("/Content/Publishing/Images/FileType/{0}.png", fileExt.Replace(".", ""))
                                   };
                    fileTypeDB.System_FileType.Add(fileType);
                    fileTypeDB.SaveChanges();
                }
                return fileType.ID;
            }
        }
    }
}
