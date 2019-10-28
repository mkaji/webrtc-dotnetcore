using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webrtc_dotnetcore.Models;

namespace webrtc_dotnetcore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult WebRTC()
        {
            return View();
        }

        public class RoomDataModel
        {
            public string RoomID { get; set; }
            public string Owner { get; set; }
        }

        public JsonResult GetRoomData()
        {
            var hoge2 = new List<RoomDataModel>();
            hoge2.Add(new RoomDataModel
            {
                RoomID = "1",
                Owner = "owner"
            });

            return new JsonResult(new { data = hoge2 });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
