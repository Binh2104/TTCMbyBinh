using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QBTourDuLich.InputModelsApi;
using QBTourDuLich.Models;
using System.ComponentModel.DataAnnotations;

namespace QBTourDuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiEvent : ControllerBase
    {
        QbdulichContext db = new QbdulichContext();
        [HttpGet]
        public IActionResult getAllSukien()
        {
            var x = (from a in db.Events
                     select new
                     {
                         a.MaSk,
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
        public IActionResult getAllSukienPagination([Range(1, 100)] int pageSize,
           [Range(1, int.MaxValue)] int pageNumber)
        {
            var listTT = (from a in db.Events
                          select new
                          {
                              a.MaSk,
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
        public IActionResult GetSukienId(string id)
        {
            var TT = (from a in db.Events
                      where a.MaSk == id
                      select new
                      {
                          a.MaSk,
                          a.MaNv,
                          a.TenFileAnh,
                          a.MoTa,
                          a.NoiDung
                      }).FirstOrDefault();
            return Ok(TT);
        }
        [HttpPost]
        [Route("themSK")]
        public async Task<IActionResult> AddSK([FromForm] EventCreateInputMode input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Upload the image to the server
            string fileName = await UploadImage(input.Anh);
            var DDCheck = db.Events.Select(x => x.MaSk).ToList();
            if (DDCheck.Any(x => x.Contains(input.MaSK)))
            {
                return BadRequest("Đã Tồn Tại su kien!");
            }

            var newTT = new Event
            {
                MaSk = input.MaSK,
                MaNv = input.MaNv,
                TenFileAnh = fileName,
                MoTa = input.MoTa,
                NoiDung = input.NoiDung

            };

            db.Events.Add(newTT);
            db.SaveChanges();
            return Ok(input);
        }

        [HttpPut]
        [Route("capnhatSK")]
        public async Task<IActionResult> UpdateSKAsync([FromForm] EventCreateInputMode input)
        {
            //ViewBag.Username = HttpContext.Session.GetString("UserName");
            var username = HttpContext.Session.GetString("UserName");
            var userid = (from a in db.TaiKhoans
                          where a.UserName == username
                          select a.UserName.ToString()).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find the DiemThamQuan in the database by id
            var TT = await db.Events.FindAsync(input.MaSK);
            if (TT == null)
            {
                return NotFound();
            }

            // Update the Event object with the form data
            TT.MaSk = input.MaSK;
            TT.MaNv = input.MaNv;
            TT.MoTa = input.MoTa;
            TT.NoiDung = input.NoiDung;


            // Upload the image to the server and update the Event object with the new image name
            if (input.Anh != null)
            {
                string fileName = await UploadImage(input.Anh);
                TT.TenFileAnh = fileName;
            }

            // Update the Event in the database
            db.Events.Update(TT);
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
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "AnhEvent", fileName);
            // Save the file to disk
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }

        [HttpDelete]
        public IActionResult DeleteSK(string input)
        {
            var DDCheck = (from a in db.Events
                           where a.MaSk == input
                           select a).FirstOrDefault();
            var check = (from a in db.Events
                         join b in db.NhanViens on a.MaNv equals b.MaNv
                         where b.MaNv == input
                         select a).FirstOrDefault();
            if (DDCheck == null)
            {
                return BadRequest("Khong tim thay su kien!");
            }

            else if (check != null)
            {
                return BadRequest("Khong thể xóa");
            }

            db.Events.Remove(DDCheck);
            db.SaveChanges();

            return Ok(input);
        }
    }
}
