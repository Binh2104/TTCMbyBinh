using Microsoft.AspNetCore.Mvc;
using QBTourDuLich.InputModelsApi;
using QBTourDuLich.Models;
using System.ComponentModel.DataAnnotations;

namespace QBTourDuLich.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class APITinTuc : Controller
    {

        QbdulichContext db = new QbdulichContext();
        [HttpGet]
        public IActionResult getAllTinTuc()
        {
            var x = (from a in db.News
                     select new
                     {
                         a.MaTin,
                         a.MaNv,
                         a.TenFileAnh,
                         a.MoTa,
                         a.NoiDung
                     });
            var totalCount = x.Count();
            var listTT = x
                .ToList();

            var result = new
            {
                TotalCount = totalCount,
                //PageCount = pageCount,
                Items = listTT
            };

            return Ok(result);
        }

        [HttpGet]
        [Route("getPagination")]
        public IActionResult GetAllTintucPagination([Range(1, 100)] int pageSize,
           [Range(1, int.MaxValue)] int pageNumber)
        {
            var listTT = (from a in db.News
                          select new
                          {
                              a.MaTin,
                              a.MaNv,
                              a.TenFileAnh,
                              a.MoTa,
                              a.NoiDung
                          })
                              .Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToList();
            var result = new
            {
                Items = listTT
            };
            return Ok(result);
        }
        [Route("getById")]
        [HttpGet]
        public IActionResult GetTintucId(string id)
        {
            var TT = (from a in db.News
                      where a.MaTin == id
                      select new
                      {
                          a.MaTin,
                          a.MaNv,
                          a.TenFileAnh,
                          a.MoTa,
                          a.NoiDung
                      }) .FirstOrDefault();
            return Ok(TT);
        }
        [HttpPost]
        [Route("themTT")]
        public async Task<IActionResult> AddTT([FromForm] TinTucCreateInputMode input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Upload the image to the server
            string fileName = await UploadImage(input.Anh);
            var DDCheck = db.News.Select(x => x.MaTin).ToList();
            if (DDCheck.Any(x => x.Contains(input.MaTin)))
            {
                return BadRequest("Đã Tồn Tại tin tuc!");
            }

            var newTT = new News
            {
                MaTin = input.MaTin,
                MaNv = input.MaNv,
                TenFileAnh = fileName,
                MoTa = input.MoTa,
                NoiDung = input.NoiDung

            };

            db.News.Add(newTT);
            db.SaveChanges();
            return Ok(input);
        }

        [HttpPut]
        [Route("capnhatTT")]
        public async Task<IActionResult> UpdateTTAsync([FromForm] TinTucCreateInputMode input)
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            var username = HttpContext.Session.GetString("UserName");
            var userid = (from a in db.TaiKhoans
                          where a.UserName == username
                          select a.UserName.ToString()).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find the DiemThamQuan in the database by id
            var TT = await db.News.FindAsync(input.MaTin);
            if (TT == null)
            {
                return NotFound();
            }

            // Update the TinTuc object with the form data
            TT.MaTin = input.MaTin;
            TT.MaNv = input.MaNv;
            TT.MoTa = input.MoTa;
            TT.NoiDung = input.NoiDung;


            // Upload the image to the server and update the TinTuc object with the new image name
            if (input.Anh != null)
            {
                string fileName = await UploadImage(input.Anh);
                TT.TenFileAnh = fileName;
            }

            // Update the TinTuc in the database
            db.News.Update(TT);
            await db.SaveChangesAsync();

            return Ok();
        }

        private Task<string> UploadImage(string anh)
        {
            throw new NotImplementedException();
        }

        private async Task<string> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }
            // Get the file name and extension
            string fileName = file.FileName;
            // Set the file path
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "AnhTinTuc", fileName);
            // Save the file to disk
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }

        [HttpDelete]
        public IActionResult DeleteTT(string input)
        {
            var DDCheck = (from a in db.News
                           where a.MaTin == input
                           select a).FirstOrDefault();
            var check = (from a in db.News
                         join b in db.NhanViens on a.MaNv equals b.MaNv
                         where b.MaNv == input
                         select a).FirstOrDefault();
            if (DDCheck == null)
            {
                return BadRequest("Khong tim thay tin tuc!");
            }

            else if (check != null)
            {
                return BadRequest("Khong thể xóa");
            }

            db.News.Remove(DDCheck);
            db.SaveChanges();

            return Ok(input);
        }
    }
}
