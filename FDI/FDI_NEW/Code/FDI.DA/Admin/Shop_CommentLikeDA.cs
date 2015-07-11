using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;

namespace FDI.DA.Admin
{
    public partial class Shop_CommentLikeDA : BaseDA
    {
        public List<Shop_CommentLike> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from o in FDIDB.Shop_CommentLike
                        where ltsArrID.Contains(o.ID)

                        select new Shop_CommentLike
                                   {
                            ID = o.ID,
                            Like = o.Like,
                            ShopCommentID = o.ShopCommentID
                        };
            TotalRecord = query.Count();
            return query.ToList();
        }

        #region Check Exits, Add, Update, Delete

        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="shopCommentID"> </param>
        /// <returns>Bản ghi</returns>
        public Shop_CommentLike GetById(int shopCommentID)
        {
            var query = from c in FDIDB.Shop_CommentLike where c.ID == shopCommentID select c;
            return query.FirstOrDefault();
        }

        public Shop_CommentLike GetByCommentId(int shopCommentID)
        {
            var query = from c in FDIDB.Shop_CommentLike where c.ShopCommentID == shopCommentID select c;
            return query.Any() ? query.FirstOrDefault() : null;
        }

        public Shop_CommentLike GetByCommentReplyId(int shopCommentID)
        {
            var query = from c in FDIDB.Shop_CommentLike where c.ShopCommentReplyID == shopCommentID select c;
            return query.FirstOrDefault();
        }


        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Shop_CommentLike> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Shop_CommentLike where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="shopComment">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(Shop_Comment shopComment)
        {
            var query = from c in FDIDB.Shop_Comment where ((c.Title == shopComment.Title) && (c.ID != shopComment.ID)) select c;
            return query.Any();
        }


        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="commentLike">bản ghi cần thêm</param>
        public void Add(Shop_CommentLike commentLike)
        {
            FDIDB.Shop_CommentLike.Add(commentLike);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="commentLike">Xóa bản ghi</param>
        public void Delete(Shop_CommentLike commentLike)
        {
            FDIDB.Shop_CommentLike.Remove(commentLike);
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
