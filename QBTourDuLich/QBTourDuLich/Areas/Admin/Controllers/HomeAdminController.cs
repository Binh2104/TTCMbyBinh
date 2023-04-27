using QBTourDuLich.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QBTourDuLich.Models.Authenciation;
using X.PagedList;

//asdasdd
namespace QBTourDuLich.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("HomeAdmin")]
    [Route("Admin/HomeAdmin")]
    public class HomeAdminController : Controller
    {
        QbdulichContext db = new QbdulichContext();
        //test
        [Route("")]
        [Route("Index")]
        [Authenciation_Admin]
        public IActionResult Index()
        {
           var user= HttpContext.Session.GetString("UserName");
            var listX = (from a in db.NhanViens where a.UserName == user select a.TenNv).ToList(); ViewBag.username = listX[0].ToString();
           
            /*  if (Session["U"])*/
            return View();
        }
        //diem tham quan
        [Route("DSDTQuan")]
        [Authenciation_Admin]
        public IActionResult DSDTQuan()
		{
            var user = HttpContext.Session.GetString("UserName");
            var listX = (from a in db.NhanViens where a.UserName == user select a.TenNv).ToList(); ViewBag.username = listX[0].ToString();
            var lstDTQ = (from a in db.DiemThamQuans select a).ToList();
			return View(lstDTQ);
		}
        //tour du lich
        [Route("DSTour")]
        [Authenciation_Admin]
        public IActionResult DSTour()
        {
            var user = HttpContext.Session.GetString("UserName");
            var listX = (from a in db.NhanViens where a.UserName == user select a.TenNv).ToList(); ViewBag.username = listX[0].ToString();
            var lstU = (from a in db.NhanViens select a).ToList();
            ViewBag.U = lstU;
            var lstTour = (from a in db.Tours select a).ToList();
            return View(lstTour);
        }
        [Route("ThemDDchoTour")]
        [Authenciation_Admin]
        public IActionResult ThemDDchoTour()
        {
            var user = HttpContext.Session.GetString("UserName");
            var listX = (from a in db.NhanViens where a.UserName == user select a.TenNv).ToList(); ViewBag.username = listX[0].ToString();
            var lstTour = (from a in db.Tours
                         select new
                         {
                             a.MaTour,
                             a.TenTour
                         }).ToList();
            var lstDD = (from a in db.DiemThamQuans
                         select new
                         {
                             a.MaDd,
                             a.Tendiadiem
                         }).ToList();
            ViewBag.DD=lstDD;
            ViewBag.Tour = lstTour;
            return View();
        }
        
        //Nhan Vien
        [Route("danhsachnhansu")]
        [Authenciation_Admin]
        public IActionResult danhsachnhansu()
		{
            var user = HttpContext.Session.GetString("UserName");
            var listX = (from a in db.NhanViens where a.UserName == user select a.TenNv).ToList(); ViewBag.username = listX[0].ToString();
            var lstNV = (from a in db.NhanViens select a).ToList();
            var lstU = (from a in db.TaiKhoans
                        where !db.NhanViens.Select(nv => nv.UserName).Contains(a.UserName)
                        select a).ToList();
            ViewBag.U = lstU;
            return View(lstNV);

		}
        [Route("taikhoan")]
        [Authenciation_Admin]
        public IActionResult taikhoan()
        {
            var user = HttpContext.Session.GetString("UserName");
            var listX = (from a in db.NhanViens where a.UserName == user select a.TenNv).ToList(); ViewBag.username = listX[0].ToString();
            var lstTK = (from a in db.TaiKhoans select a).ToList();
            return View(lstTK);
        }

        [Route("danhsachtintuc")]
        [Authenciation_Admin]
        public IActionResult danhsachtintuc()
        {
            var user = HttpContext.Session.GetString("UserName");
            var listX = (from a in db.NhanViens where a.UserName == user select a.TenNv).ToList(); ViewBag.username = listX[0].ToString();
            var lstTT = (from a in db.News select a).ToList();
            var lstU = (from a in db.NhanViens  select a).ToList();
            ViewBag.U = lstU;
            return View(lstTT);
        } 
        [Route("danhsachsukien")]
        [Authenciation_Admin]
        public IActionResult sukien()
        {
            var user = HttpContext.Session.GetString("UserName");
            var listX = (from a in db.NhanViens where a.UserName == user select a.TenNv).ToList(); ViewBag.username = listX[0].ToString();
            var lstTT = (from a in db.Events select a).ToList();
            var lstU = (from a in db.NhanViens select a).ToList();
            ViewBag.U = lstU;
            return View(lstTT);
        }
        
        [Route("khachsan")]
        [Authenciation_Admin]
        public IActionResult khachsan()
        {
            var user = HttpContext.Session.GetString("UserName");
            var listX = (from a in db.NhanViens where a.UserName == user select a.TenNv).ToList(); ViewBag.username = listX[0].ToString();
            var lstKS = (from a in db.KhachSans select a).ToList();
            var lstU = (from a in db.NhanViens select a.MaNv).ToList();
            ViewBag.U = lstU;
            return View(lstKS);
        }
       
    }
}
