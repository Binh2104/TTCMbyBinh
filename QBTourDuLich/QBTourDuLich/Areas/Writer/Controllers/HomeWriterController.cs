using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using QBTourDuLich.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using QBTourDuLich.Models.Authenciation;

namespace QBTourDuLich.Areas.Writer.Controllers
{
    [Area("writer")]
    [Route("writer")]
    [Route("writer/homewriter")]
    public class HomeWriterController : Controller
    {
        QbdulichContext db = new QbdulichContext();
        //test
        [Route("")]
        [Route("Index")]
        [Authenciation_Writer]
        public IActionResult Index()
        {
            var user = HttpContext.Session.GetString("UserName");
            var listX = (from a in db.NhanViens where a.UserName == user select a.TenNv).ToList(); ViewBag.username = listX[0].ToString();
            return View();
        }

        [Route("danhsachtintuc")]
        [Authenciation_Writer]
        public IActionResult danhsachtintuc()
        {
            var user = HttpContext.Session.GetString("UserName");
            var listX = (from a in db.NhanViens where a.UserName == user select a.TenNv).ToList(); ViewBag.username = listX[0].ToString();
            var lstTT = (from a in db.News select a).ToList();
            var lstU = (from a in db.NhanViens select a).ToList();
            ViewBag.U = lstU;
            return View(lstTT);
        }
        [Route("danhsachsukien")]
        [Authenciation_Writer]
        public IActionResult sukien()
        {
            var user = HttpContext.Session.GetString("UserName");
            var listX = (from a in db.NhanViens where a.UserName == user select a.TenNv).ToList(); ViewBag.username = listX[0].ToString();
            var lstTT = (from a in db.Events select a).ToList();
            var lstU = (from a in db.NhanViens select a).ToList();
            ViewBag.U = lstU;
            return View(lstTT);
        }
    }
}
