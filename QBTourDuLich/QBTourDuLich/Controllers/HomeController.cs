using Microsoft.AspNetCore.Mvc;
using QBTourDuLich.Models;
using System.Diagnostics;
using X.PagedList;
using Microsoft.EntityFrameworkCore;

namespace QBTourDuLich.Controllers
{
    public class HomeController : Controller
    {
        QbdulichContext db= new QbdulichContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var top5tour = (from a in db.Tours where a.XepHangTour == 5 select a).Take(5).ToList();
            return View(top5tour);
        }
		public IActionResult DiemDen(int page = 1)
		{
			int pageNumber = page;
			int pageSize = 3;
			var lstsanpham = db.Tours.OrderBy(x => x.TenTour).ToList();
			PagedList<Tour> lst = new PagedList<Tour>(lstsanpham, pageNumber, pageSize);
			return View(lst);
		}
		public IActionResult ChiTietTour(string MaTour)
		{
			var chiDD= (from a in db.DiaDiemTours join b in db.DiemThamQuans
                        on a.MaDd equals b.MaDd where a.MaTour==MaTour select b).ToList();
           var chi = (from a in db.Tours
					   where a.MaTour == MaTour
					   select a
					 ).ToList();
			ViewBag.chi = chi;
			ViewBag.chiDD = chiDD;
			return View();
			/*return View(chitiet);*/
		}
		public IActionResult ThuVien(int page = 1)
		{
			int pageNumber = page;
			int pageSize = 8;
			var lsthinhanh = db.DiemThamQuans.OrderBy(x => x.MaDd).ToList();
			PagedList<DiemThamQuan> lst = new PagedList<DiemThamQuan>(lsthinhanh, pageNumber, pageSize);
			return View(lst);
		}
		public IActionResult TinTuc(int page = 1)
		{
			int pageSize = 5;
			int pageNumber = page;
			var listSP = db.News.AsNoTracking().OrderBy(x => x.MaTin);
			PagedList<News> lst = new PagedList<News>(listSP, pageNumber, pageSize);
			var nguoidang = (from a in db.News
							 join b in db.NhanViens on a.MaNv equals b.MaNv
							 select b.TenNv).ToList();
			ViewBag.nguoidang = nguoidang;
			return View(lst);
		}
		public IActionResult ChiTietTinTuc(String maTin)
		{

			var Tin = db.News.SingleOrDefault(x => x.MaTin == maTin);
			/*var anhTin = db.AnhTins.Where(x => x.MaTin == maTin).ToList();*/
			var tinTuc = db.News.Where(x => x.MaTin == maTin).ToList();
			/*ViewBag.anhTin = anhTin;*/
			ViewBag.tinTuc = tinTuc;
			var listSP = db.News.AsNoTracking();
			ViewBag.Tint = listSP;

			return View(Tin);
		}
        public IActionResult KhachSan(int page = 1)
        {
            int pageNumber = page;
            int pageSize = 3;
            var lstsanpham = db.KhachSans.OrderBy(x => x.TenKs).ToList();
            PagedList<KhachSan> lst = new PagedList<KhachSan>(lstsanpham, pageNumber, pageSize);
            return View(lst);
        }
		public IActionResult Event(int page = 1)
		{
			int pageSize = 2;
			int pageNumber = page;
			var listSP = db.Events.AsNoTracking().OrderBy(x => x.MaSk);
			PagedList<Event> lst = new PagedList<Event>(listSP, pageNumber, pageSize);
			var nguoidang = (from a in db.Events
							 join b in db.NhanViens on a.MaNv equals b.MaNv		
							 select b.TenNv).ToList();
			ViewBag.nguoidang = nguoidang;
			return View(lst);
		}
		public IActionResult ChiTietEvent(String mask)
		{
			var sukien = db.Events.SingleOrDefault(x => x.MaSk == mask);
			/*var anhTin = db.AnhTins.Where(x => x.MaTin == maTin).ToList();*/
			var sk = db.Events.Where(x => x.MaSk == mask).ToList();
			/*ViewBag.anhTin = anhTin;*/
			ViewBag.sk = sk;
			var listSP = db.Events.AsNoTracking();
			ViewBag.Tint = listSP;
			return View(sukien);
		}


		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}