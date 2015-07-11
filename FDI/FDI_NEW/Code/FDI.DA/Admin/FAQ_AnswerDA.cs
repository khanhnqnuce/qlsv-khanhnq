using System.Collections.Generic;
using System.Linq;
using System.Web;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.Admin
{
    public partial class FAQ_AnswerDA : BaseDA
    {
        #region Constructer
        public FAQ_AnswerDA()
        {
        }

        public FAQ_AnswerDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public FAQ_AnswerDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<FAQAnswerItem> GetAllListSimple()
        {
            var query = from c in FDIDB.FAQ_Answer
                        orderby c.Title
                        select new FAQAnswerItem
                                   {
                                       ID = c.ID,
                                       Title = c.Title
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <param name="isShow">Kiểm tra hiển thị</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<FAQAnswerItem> GetListSimpleAll(bool isShow)
        {
            var query = from c in FDIDB.FAQ_Answer
                        where (c.IsShow == isShow)
                        orderby c.Title
                        select new FAQAnswerItem
                                   {
                                       ID = c.ID,
                                       Title = c.Title
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<FAQAnswerItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.FAQ_Answer
                        orderby c.Title
                        where c.Title.StartsWith(keyword)
                        select new FAQAnswerItem
                                   {
                                       ID = c.ID,
                                       Title = c.Title
                                   };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <param name="isShow"> </param>
        /// <returns></returns>
        public List<FAQAnswerItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.FAQ_Answer
                        orderby c.Title
                        where c.IsShow == isShow
                        && c.Title.StartsWith(keyword)
                        select new FAQAnswerItem
                                   {
                                       ID = c.ID,
                                       Title = c.Title
                                   };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<FAQAnswerItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.FAQ_Answer
                        select new FAQAnswerItem
                                   {
                                       ID = o.ID,
                                       Title = o.Title,
                                       TitleAscii = o.TitleAscii,
                                       DateCreated = o.DateCreated
                                   };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<FAQAnswerItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from o in FDIDB.FAQ_Answer
                        where ltsArrID.Contains(o.ID)
                        orderby o.ID descending
                        select new FAQAnswerItem
                                   {
                                       ID = o.ID,
                                       Title = o.Title,
                                       TitleAscii = o.TitleAscii
                                   };
            TotalRecord = query.Count();
            return query.ToList();
        }

        #region Check Exits, Add, Update, Delete
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="answerID">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public FAQ_Answer GetById(int answerID)
        {
            var query = from c in FDIDB.FAQ_Answer where c.ID == answerID select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<FAQ_Category> GetListCategoryByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.FAQ_Category where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<FAQ_Answer> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.FAQ_Answer where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="faqAnswer">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(FAQ_Answer faqAnswer)
        {
            var query = from c in FDIDB.FAQ_Answer where ((c.Title == faqAnswer.Title) && (c.ID != faqAnswer.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="answerTitle">Tên bản ghi</param>
        /// <returns>Bản ghi</returns>
        public FAQ_Answer GetByName(string answerTitle)
        {
            var query = from c in FDIDB.FAQ_Answer where ((c.Title == answerTitle)) select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="faqAnswer">bản ghi cần thêm</param>
        public void Add(FAQ_Answer faqAnswer)
        {
            FDIDB.FAQ_Answer.Add(faqAnswer);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="faqAnswer"> </param>
        public void Delete(FAQ_Answer faqAnswer)
        {
            FDIDB.FAQ_Answer.Remove(faqAnswer); // Xóa câu trả lời
        }

        /// <summary>
        /// save bản ghi vào DB
        /// </summary>
        public void Save()
        {
            FDIDB.SaveChanges();
        }
        #endregion
    }
}
