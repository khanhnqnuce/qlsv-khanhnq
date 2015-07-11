using FDI.Base;
using FDI.Simple;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FDI.DA.Admin
{
    public partial class FAQQuestionDA : BaseDA
    {
        #region Constructer
        public FAQQuestionDA()
        {
        }

        public FAQQuestionDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public FAQQuestionDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<FAQQuestionItem> GetAllListSimple()
        {
            var query = from c in FDIDB.FAQ_Question
                        orderby c.Title
                        select new FAQQuestionItem
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
        public List<FAQQuestionItem> GetListSimpleAll(bool isShow)
        {
            var query = from c in FDIDB.FAQ_Question
                        where (c.IsShow == isShow)
                        orderby c.Title
                        select new FAQQuestionItem
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
        public List<FAQQuestionItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.FAQ_Question
                        orderby c.Title
                        where c.Title.StartsWith(keyword)
                        select new FAQQuestionItem
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
        public List<FAQQuestionItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.FAQ_Question
                        orderby c.Title
                        where c.IsShow == isShow
                        && c.Title.StartsWith(keyword)
                        select new FAQQuestionItem
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
        public List<FAQQuestionItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.FAQ_Question
                        select new FAQQuestionItem
                                   {
                                       ID = o.ID,
                                       Title = o.Title,
                                       TitleAscii = o.TitleAscii,
                                       IsShow = o.IsShow,
                                       CategoryID = o.FAQ_Category.ID,
                                       FAQ_Category = new FAQCategoryItem
                                                          {
                                                              ID = o.FAQ_Category.ID,
                                                              Name = o.FAQ_Category.Name,
                                                              NameAscii = o.FAQ_Category.NameAscii
                                                          },
                                       DateCreated = o.DateCreated,
                                       Email = o.Email,
                                       Fullname = o.Fullname,
                                       Phone = o.Phone,
                                       TotalAnswers = o.FAQ_Answer.Count()
                                   };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<FAQQuestionItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from o in FDIDB.FAQ_Question
                        where ltsArrID.Contains(o.ID)
                        orderby o.ID descending
                        select new FAQQuestionItem
                                   {
                                       ID = o.ID,
                                       Title = o.Title,
                                       TitleAscii = o.TitleAscii,
                                       IsShow = o.IsShow,
                                       CategoryID = o.FAQ_Category.ID,
                                       FAQ_Category = new FAQCategoryItem
                                       {
                                           ID = o.FAQ_Category.ID,
                                           Name = o.FAQ_Category.Name,
                                           NameAscii = o.FAQ_Category.NameAscii
                                       },
                                       DateCreated = o.DateCreated,
                                       Email = o.Email,
                                       Fullname = o.Fullname,
                                       Phone = o.Phone,
                                       TotalAnswers = o.FAQ_Answer.Count()
                                   };
            TotalRecord = query.Count();
            return query.ToList();
        }

        #region Check Exits, Add, Update, Delete
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="questionID">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public FAQ_Question GetById(int questionID)
        {
            var query = from c in FDIDB.FAQ_Question where c.ID == questionID select c;
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
        public List<FAQ_Question> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.FAQ_Question where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="faqQuestion">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(FAQ_Question faqQuestion)
        {
            var query = from c in FDIDB.FAQ_Question where ((c.Title == faqQuestion.Title) && (c.ID != faqQuestion.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="questionTitle">Tên bản ghi</param>
        /// <returns>Bản ghi</returns>
        public FAQ_Question GetByName(string questionTitle)
        {
            var query = from c in FDIDB.FAQ_Question where ((c.Title == questionTitle)) select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="faqQuestion">bản ghi cần thêm</param>
        public void Add(FAQ_Question faqQuestion)
        {
            FDIDB.FAQ_Question.Add(faqQuestion);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="faqQuestion">Xóa bản ghi</param>
        public void Delete(FAQ_Question faqQuestion)
        {
            foreach (var answer in faqQuestion.FAQ_Answer)
                FDIDB.FAQ_Answer.Remove(answer); //Xóa câu hỏi
            FDIDB.FAQ_Question.Remove(faqQuestion); // Xóa câu trả lời
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
