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
            // Upload the image to the server
            string fileName = await UploadImage(input.Anh);
            /*var DDCheck = (from a in db.Events
                           where a.MaSk == input.MaSK
                           select a).ToList();
            if (DDCheck != null)
            {
                return BadRequest("Đã Tồn Tại !");
            }*/

            var newEvent = new Event
            {
                MaSk = input.MaSK,
                MaNv=input.MaNv,
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


        [HttpDelete]
        [Route("xoa")]
        public IActionResult DeleteRecord(string matour, string madd)
        {
            var tour = db.Tours.FirstOrDefault(x => x.MaTour == matour);
            var dtq = db.DiemThamQuans.FirstOrDefault(x => x.MaDd == madd);

            if (tour == null || dtq == null)
            {
                return BadRequest("Tour hoặc điểm thăm quan không tồn tại.");
            }

            var recordtourdtq = db.DiaDiemTours.FirstOrDefault(x => x.MaTour == matour && x.MaDd == madd);

            if (recordtourdtq == null)
            {
                return BadRequest("Không tìm thấy bản ghi để xóa.");
            }

            db.DiaDiemTours.Remove(recordtourdtq);
            db.SaveChanges();

            return Ok();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
