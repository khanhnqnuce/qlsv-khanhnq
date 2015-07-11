using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FDI.Common
{
    /// <summary>
    /// Create by BienLV
    /// 03-01-2014
    /// </summary>
    public static class StaticEnum
    {
        public enum PaymentStatus : int
        {
            //Đang chờ
            Watting = 10,
            //Được chứng thực
            AuthenAuthentication = 20,
            //Đặt cọc
            Deposit = 25,
            //Đã thanh toán
            Paid = 30,
            //Trả lại 1 phần
            ReturnPart = 35,
            //Đã hoàn lại
            Returned = 40,
            //Mất hiệu lực
            Failed = 50
        }

        public enum OrderStatus : int
        {
            Pending = 10,    // đang chờ
            IsSent = 12,     // đã gửi sang sap
            Open = 15,       // sap đã xem
            Processing = 20, // đang xư lý
            Complete = 30,   // đã hoàn thành
            Cancel = 35,     // chờ hủy
            Cancelled = 40,  // đã hủy
            Refunded = 50,   // trả hàng
            IsOrdered = 60,  // đặt cọc
            Failed = 70      // Thất bại
        }

        public enum OrderContact : int
        {
            //Chưa liên lạc
            NoContact = 1,
            //Đã liên lạc
            Contact = 10,
            //Gọi lần 1
            FirstCall = 15,
            //Gọi lần 2
            SecondCall = 20,
            //Không liên lạc được
            NotContact = 30,
            //Gọi lại sau
            CallBack = 35, 
        }

        public enum OrderType : int
        {
            //Bình thường
            Normal = 10,
            //Mua ngay
            BuyNow = 20,
            //Mua trên Mobile
            BuyNowByMobile = 21,
            //Trả góp
            Recurring = 30,
            //Đặt hàng
            Ordered = 40,
            //Giờ vàng
            GoldenTime = 50,
            //Sendo
            Sendo = 60
        }

        public enum ShippingStatus : int
        {
            //Không yêu cầu vận chuyển
            ShippingNotRequired = 10,
            //Chưa chuyển hàng
            NotYetShipped = 20,
            //Đã gửi sang sengo
            InSengo = 22,
            //Sengo đang chuẩn bị xử lý
            Processing = 23,
            //Đã giao hàng 1 phần
            PartiallyShipped = 25,
            //Sengo đang trên đường lấy hàng
            Pickingup = 27,
            //Đã được chuyển
            Shipped = 30,
            //Sengo đang giao hàng
            Delivering = 35,
            //Đã giao hàng
            Transform = 40
        }

        public enum ContactStatus : int
        {
            NoContactYet = 0,  // chưa liên lạc
            Contacted = 10,    // Đã liên lạc
            FirstCall = 20,    // Gọi lần 1
            SecondCall = 30,   // Gọi lần 2
            NoContact = 40,    // Không liên lạc dc
            CallBack = 50,     // Gọi lại sau
        }

        public enum PaymentMethod : int
        {
            //On Delivery (COD)
            OnDeliveryCod = 1,
            //Chuyển khoản qua tài khoản ATM
            UseAtm = 2,
            //Trực tiếp qua thẻ Visa, Master Card (OnePAY)
            VisaMasteCardOnePay = 3,
            //Trực tiếp qua ATM nội địa (OnePAY)
            AtmOnpay = 4,
            //Thanh toán qua đường Bưu điện
            PostOffice = 5,
            //Thanh toán trả góp
            Recurring = 6,
            //Payoo
            Payoo = 7

        }

        public enum ShippingMethod : int
        {
            //Nhận hàng tại FPT Shop
            ShippingAtShop = 1,
            //Nhận hàng tại địa chỉ trên
            ShippingAtAddress = 2
        }

        public enum DiscountType : int
        {
            DiscountToOrderTotal = 1,    // Được gán vào tổng tiền đơn hàng
            DiscountToSkus = 2,          // Được gán vào các SKUs
            DiscountToProductType = 5,   // Được gán vào loại sản phẩm
            DiscountToShipping = 10,     // Được gán vào vận chuyển
            DiscountToOrder = 20,        // Được gán vào thành tiền đơn hàng
            DiscountToShoppingCart = 30, // Được gán vào giảm giá online  
            DiscountToSpecial = 40,      // Được gán vào giảm giá đặc biệt
            OrderedToCrossell = 50       // Được gán vào giảm giá tặng kèm
        }

        public enum CustomerType : int
        {
            Student = 1,    // Sinh viên
            Worker = 2      // Người đi làm
        }


        public enum TypeFilterProduct
        {
            PreOrder = 1,
            Old = 2
        }

        public enum Menu
        {
            Tintuc = 14
        }
    }
}