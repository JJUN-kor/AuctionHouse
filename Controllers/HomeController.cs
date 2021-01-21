using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Net;
using Newtonsoft.Json;
using MySqlX.XDevAPI;
using System.Configuration;
using System.Web.Services;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // ItemData m_item = new ItemData();
            

            return View();
            

        }

        #region 디버깅 컨트롤러
        [HttpPost]
        [WebMethod]
        public JsonResult Test()
        {
            ItemData m_item = new ItemData();
            m_item.DBTest();

            return Json(1,JsonRequestBehavior.AllowGet);
        }
        #endregion

        [WebMethod]
        public JsonResult Get_All_Item_Name()
        {
            ItemData m_item = new ItemData();

            List <ItemData> items = new List<ItemData>();

            items = m_item.Get_All_items_DB();

            return Json(items, JsonRequestBehavior.AllowGet);
        }
        
        [WebMethod]
        [HttpPost]
        public JsonResult Get_Item_Name(string item_name)
        {
            ItemData item = new ItemData();
            item = item.Get_Item_Price(item_name);

            return Json(item,JsonRequestBehavior.AllowGet);
        }

        [WebMethod]
        public JsonResult Get_Top_Search_Item()
        {

            ItemData m_item = new ItemData();

            List<ItemData> items = new List<ItemData>();

            items = m_item.Get_Top_List();

            return Json(items, JsonRequestBehavior.AllowGet);
        }

    }
}