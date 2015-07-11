using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.Admin
{
    public partial class News_LikeDA : BaseDA
    {
        public List<CommentLikeItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from o in FDIDB.Comment_Like
                        where ltsArrID.Contains(o.ID)

                        select new CommentLikeItem
                                   {
                            ID = o.ID,
                            Like = o.Like,
                            CommentID = o.CommentID
                        };
            TotalRecord = query.Count();
            return query.ToList();
        }

        #region Check Exits, Add, Update, Delete
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="commentID">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Comment_Like GetById(int commentID)
        {
            var query = from c in FDIDB.Comment_Like where c.ID == commentID select c;
            return query.FirstOrDefault();
        }

        public Comment_Like GetByCommentId(int commentID)
        {
            var query = from c in FDIDB.Comment_Like where c.CommentID == commentID select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Comment_Like> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Comment_Like where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="newsComment">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(News_Comment newsComment)
        {
            var query = from c in FDIDB.News_Comment where ((c.Title == newsComment.Title) && (c.ID != newsComment.ID)) select c;
            return query.Any();
        }


        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="commentLike">bản ghi cần thêm</param>
        public void Add(Comment_Like commentLike)
        {
            FDIDB.Comment_Like.Add(commentLike);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="commentLike">Xóa bản ghi</param>
        public void Delete(Comment_Like commentLike)
        {
            FDIDB.Comment_Like.Remove(commentLike);
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
