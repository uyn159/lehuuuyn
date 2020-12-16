﻿using System;
using System.Collections.Generic;
using System.Globalization;//
using System.Linq;
using System.Threading;//   
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebOnline.Common;

namespace WebOnline.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)// tích hợp đa ngôn ngữ 
        {
            base.Initialize(requestContext);    
            if (Session[CommonConstants.CurrentCulture] != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session[CommonConstants.CurrentCulture].ToString());
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session[CommonConstants.CurrentCulture].ToString());
            }
            else
            {
                Session[CommonConstants.CurrentCulture] = "vi";
                Thread.CurrentThread.CurrentCulture = new CultureInfo("vi");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("vi");
            }
        }
        public ActionResult ChangeCulture(string ddlCulture, string returnUrl)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(ddlCulture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(ddlCulture);

            Session[CommonConstants.CurrentCulture] = ddlCulture;
            return Redirect(returnUrl);
        }
        protected override void OnActionExecuted(ActionExecutedContext filterContext)// chặn truy cập bằng url khi chưa login
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];// key của session
            if(session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { Controller = "Login", action="Index", Area="Admin" }));
            }
            base.OnActionExecuted(filterContext);
        }
        protected void SetAlert(string message, string type)//lớp nào kế thừa từ base này điều sử dụng được
        {
            TempData["AlertMessage"] = message;//lấy thông tin từ user truyền về
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
    
}