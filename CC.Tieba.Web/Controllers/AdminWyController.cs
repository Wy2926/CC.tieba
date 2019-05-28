using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CC.Tieba.Web.Controllers
{
    /// <summary>
    /// 管理员页面
    /// </summary>
    public class AdminWyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}