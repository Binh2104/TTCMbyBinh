using Microsoft.AspNetCore.Mvc;
using QBTourDuLich.Models;


namespace QBTourDuLich.Controllers
{
    public class AccessController : Controller
    {
       QbdulichContext db = new QbdulichContext();

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                if (HttpContext.Session.GetString("Loai") == "1")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Writer");
                }

            }

        }
        public IActionResult Dashboard()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))){
                return RedirectToAction("Login");
            }
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetString("Loai");
            return View();
        }
        [HttpPost]
        public IActionResult Login(TaiKhoan user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.TaiKhoans.Where(x => x.UserName.Equals(user.UserName) && x.Password.Equals(user.Password)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("Loai", u.Loai.ToString());
                    HttpContext.Session.SetString("UserName", u.UserName.ToString());
                    if (u.Loai == 1)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (u.Loai == 0)
                    {
                        return RedirectToAction("Index", "Writer");
                    }
                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("Index", "Admin");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("Loai");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register(TaiKhoan user)
        {
            if (ModelState.IsValid)
            {
                var check = db.TaiKhoans.Where(x => x.UserName.Equals(user.UserName) && x.Loai.Equals(2)).ToList().FirstOrDefault();
                if (check == null)
                {
                    //user.Password = getMD5(user.Password);                 
                    db.TaiKhoans.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Access");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
    }
}
