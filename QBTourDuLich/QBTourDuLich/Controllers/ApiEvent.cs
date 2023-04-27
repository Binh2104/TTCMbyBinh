using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;
using QBTourDuLich.InputModelsApi;
using QBTourDuLich.Models;

namespace QBTourDuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiEvent : Controller
    {
        QbdulichContext db = new QbdulichContext();
        [HttpGet]
        public IActionResult getAllEvent()
        {
            var query = (from a in db.Events join b in db.NhanViens on a.MaNv equals b.MaNv
                         select new
                         {
                             b.TenNv,
                            a.MaSk,
                            a.TenFileAnh,
                            a.MoTa,
                            a.NoiDung,
                         });
            var totalCount = query.Count();
            //var pageCount = (int)Math.Ceiling((double)totalCount / pageSize);

            var listEvent = query.ToList();

            var result = new
            {
                TotalCount = totalCount,
                //PageCount = pageCount,
                Items = listEvent
            };

            return Ok(result);

        }
        [HttpGet]
        [Route("getPagination")]
        public IActionResult GetAllEventPagination([Range(1, 100)] int pageSize,
         [Range(1, int.MaxValue)] int pageNumber)
        {
            var listEvent = (from a in db.Events
                             join b in db.NhanViens on a.MaNv equals b.MaNv
                             select new
                             {
                                 b.TenNv,
                                 a.MaSk,
                                 a.TenFileAnh,
                                 a.MoTa,
                                 a.NoiDung,
                             })
                              .Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToList();
            var result = new
            {
                Items = listEvent
            };
            return Ok(result);
        }
        [Route("getById")]
        [HttpGet]
        public IActionResult GetEventId(string id)
        {
            var Event = (from a in db.Events
                         join b in db.NhanViens on a.MaNv equals b.MaNv
                         where a.MaSk==id
                         select new
                         {
                             b.TenNv,
                             a.MaSk,
                             a.TenFileAnh,
                             a.MoTa,
                             a.NoiDung,
                         })
                              .FirstOrDefault();
            return Ok(Event);
        }

        [HttpPost]
        [Route("themEvent")]
        public async Task<IActionResult> AddEvent([FromForm] EventCreateInputMode input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var username = HttpContext.Session.GetString("UserName");
            
            var userid = (from a in db.TaiKhoans
                          join b in db.NhanViens on a.UserName equals b.UserName
                          where a.UserName == username
                          select b.MaNv.ToString()).FirstOrDefault();
            Console.WriteLine(userid);
            // Upload the image to the server
            string fileName = await UploadImage(input.Anh);
            var DDCheck = (from a in db.Events
                           where a.MaSk == input.MaSK
                           select a).ToList();
            if (DDCheck.Count() > 0)
            {
                return BadRequest("Đã Tồn Tại !");
            }

            var newEvent = new Event
            {
                MaSk = input.MaSK,
                MaNv=userid,
                MoTa=input.MoTa,
                NoiDung=input.NoiDung,
                TenFileAnh=fileName,
            };

            db.Events.Add(newEvent);
            db.SaveChanges();
            return Ok(input);
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
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "anhEvent", fileName);
            // Save the file to disk
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }
        [HttpPut]
        [Route("capnhatEvent")]
        public async Task<IActionResult> UpdateTTAsync([FromForm] EventCreateInputMode input)
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            var username = HttpContext.Session.GetString("UserName");
            var userid = (from a in db.TaiKhoans
                          join b in db.NhanViens on a.UserName equals b.UserName
                          where a.UserName == username
                          select b.MaNv.ToString()).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find the DiemThamQuan in the database by id
            var SK = await db.Events.FindAsync(input.MaSK);
            if (SK == null)
            {
                return NotFound();
            }

            // Update the TinTuc object with the form data
            SK.MaSk = input.MaSK;
            SK.MoTa = input.MoTa;
            SK.MaNv = userid;
            SK.NoiDung = input.NoiDung;


            // Upload the image to the server and update the TinTuc object with the new image name
            if (input.Anh != null)
            {
                string fileName = await UploadImage(input.Anh);
                SK.TenFileAnh = fileName;
            }

            // Update the TinTuc in the database
            db.Events.Update(SK);
            await db.SaveChangesAsync();

            return Ok();
        }


        [HttpDelete]
        public IActionResult DeleteTT(string input)
        {
            var DDCheck = (from a in db.Events
                           where a.MaSk == input
                           select a).FirstOrDefault();
            if (DDCheck == null)
            {
                return BadRequest("Khong tim thay sự kiện nào!");
            }
            db.Events.Remove(DDCheck);
            db.SaveChanges();

            return Ok(input);
        }
    }
}
