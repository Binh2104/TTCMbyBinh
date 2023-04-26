using QBTourDuLich.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QBTourDuLich.Models.Authentication;
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
        [Authentication]
        public IActionResult Index()
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");

            /*  if (Session["U"])*/
            return View();
        }
        //diem tham quan
        [Route("DSDTQuan")]
        [Authentication]
		public IActionResult DSDTQuan()
		{
			ViewBag.Username = HttpContext.Session.GetString("UserName");
			var lstDTQ = (from a in db.DiemThamQuans select a).ToList();
			return View(lstDTQ);
		}
        //tour du lich
        [Route("DSTour")]
        [Authentication]
        public IActionResult DSTour()
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            var lstU = (from a in db.NhanViens select a).ToList();
            ViewBag.U = lstU;
            var lstTour = (from a in db.Tours select a).ToList();
            return View(lstTour);
        }
        [Route("ThemDDchoTour")]
        [Authentication]
        public IActionResult ThemDDchoTour()
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
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
		[Authentication]
		public IActionResult danhsachnhansu()
		{
			ViewBag.Username = HttpContext.Session.GetString("UserName");
			var lstNV = (from a in db.NhanViens select a).ToList();
            var lstU = (from a in db.TaiKhoans
                        where a.Loai == 1 && !db.NhanViens.Select(nv => nv.UserName).Contains(a.UserName)
                        select a.UserName).ToList();
            ViewBag.U = lstU;
            return View(lstNV);

		}
        [Route("taikhoan")]
        [Authentication]
        public IActionResult taikhoan()
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            var lstTK = (from a in db.TaiKhoans select a).ToList();
            return View(lstTK);
        }

        [Route("danhsachtintuc")]
        [Authentication]
        public IActionResult danhsachtintuc()
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            var lstTT = (from a in db.News select a).ToList();
            var lstU = (from a in db.NhanViens  select a).ToList();
            ViewBag.U = lstU;
            return View(lstTT);
        }
        
        [Route("khachsan")]
        [Authentication]
        public IActionResult khachsan()
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            var lstKS = (from a in db.KhachSans select a).ToList();
            var lstU = (from a in db.NhanViens select a.MaNv).ToList();
            ViewBag.U = lstU;
            return View(lstKS);
        }
        [Route("danhsachsukien")]
        [Authentication]
        public IActionResult sukien()
        {
            //ViewBag.Username = HttpContext.Session.GetString("UserName");
            var lstTT = (from a in db.Events select a).ToList();
            var lstU = (from a in db.NhanViens select a).ToList();
            ViewBag.U = lstU;
            return View(lstTT);
        }
    }
}
