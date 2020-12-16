using Model.DAO;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebOnline.Common;

namespace WebOnline.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Slides = new SlideDao().ListAll();// laasy slide trong database gan vao view
            var productDao = new ProductDao();
            ViewBag.NewProducts = productDao.ListNewProduct(4);// lấy ra 4 san pham
            ViewBag.ListFeatureProducts = productDao.ListFeatureProduct(4);// nhung san pham hot 
            return View();
        }
        [ChildActionOnly]// không gọi trực tiếp từ trang được
        public ActionResult MainMenu()
        {
            var model = new MenuDao().ListByGroupId(1);
            return PartialView(model);
        }
        [ChildActionOnly]// không gọi trực tiếp từ trang được
        public ActionResult TopMenu()
        {
            var model = new MenuDao().ListByGroupId(2);
            return PartialView(model);
        }
        [ChildActionOnly]// không gọi trực tiếp từ trang được
        public ActionResult Footer()
        {
            var model = new FooterDao().GetFooter();
            return PartialView(model);
        }
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return PartialView(list);
        }
    }
}