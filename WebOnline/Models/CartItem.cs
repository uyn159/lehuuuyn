using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    [Serializable]
    public class CartItem
    {
        public Product Product { set; get; }
        public int Quantity { set; get; }
    }
}
//model chứa các dữ liệu giúp cho luân chuyển dữ liệu trong ứng dụng tốt hơn, chuyên thao tác dữ liệu
//views hiển thị dự liệu và form để cho người dùng tương tác
//nhận request người dùng từ view chuyển điều hướng thông tin thông qua models , đóng gói dữ liệu qua models rồi trả về view