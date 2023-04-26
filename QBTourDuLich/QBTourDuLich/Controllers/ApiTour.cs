using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;
using QBTourDuLich.InputModelsApi;
using QBTourDuLich.Models;
namespace QBTourDuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiTour : Controller
    {

        QbdulichContext db = new QbdulichContext();
        [HttpGet]
        public IActionResult getAllTour()
        {
            var query = (from a in db.Tours join b in db.NhanViens on a.MaNv equals b.MaNv                       
                         select new
                         {
                            b.TenNv,
                            a.MaTour,
                             a.MaNv,
                            a.TenTour,
                            a.Anh,
                        
                            a.XepHangTour,
                            a.MoTa
                            
                          
                         });
            var totalCount = query.Count();
            //var pageCount = (int)Math.Ceiling((double)totalCount / pageSize);

            var listTour= query
                .ToList();

            var result = new
            {
                TotalCount = totalCount,
                //PageCount = pageCount,
                Items = listTour
            };

            return Ok(result);

        }
        [HttpGet]
        [Route("getPagination")]
        public IActionResult GetAllTourPagination([Range(1, 100)] int pageSize,
           [Range(1, int.MaxValue)] int pageNumber)
        {
            var listTour = (from a in db.Tours
							join b in db.NhanViens on a.MaNv equals b.MaNv
							select new
                           {
                                b.TenNv,
                               a.MaTour,
                               a.MaNv,
                               a.TenTour,
                               a.Anh,
                               
                               a.XepHangTour,
                               a.MoTa
                               
                           })
                              .Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToList();
            var result = new
            {
                Items = listTour
            };
            return Ok(result);
        }
        [Route("getById")]
        [HttpGet]
        public IActionResult GetTourId(string id)
        {
            var Tour = (from a in db.Tours
						join b in db.NhanViens on a.MaNv equals b.MaNv
						where a.MaTour== id    
                          select new
                          {
                              b.TenNv,
                              a.MaTour,
                              a.MaNv,
                              a.TenTour,
                              a.Anh,
                             
                              a.XepHangTour,
                              a.MoTa
                              
                          })
                              .FirstOrDefault();
            return Ok(Tour);
        }
        [HttpPost]
        [Route("themTour")]
        public async Task<IActionResult> AddTour([FromForm] TourCreateInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Upload the image to the server
            string fileName = await UploadImage(input.Anh);
            var TourCheck = db.Tours.Select(x => x.MaTour).ToList();
            if (TourCheck.Any(x => x.Contains(input.MaTour)))
            {
                return BadRequest("Đã Tồn Tại Tour!");
            }

            var newTour = new Tour
            {
                MaTour = input.MaTour,
                MaNv = input.MaNV,
                TenTour = input.TenTour,
                XepHangTour= input.XepHangTour,
                Anh=fileName,
                MoTa=input.MoTa
                
            };

            db.Tours.Add(newTour);
            db.SaveChanges();
            return Ok(input);
        }
       
        [HttpPut]
        [Route("capnhatTour")]
        public async Task<IActionResult> UpdateTourAsync([FromForm] TourCreateInputModel input)
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
            var Tour = await db.Tours.FindAsync(input.MaTour);
            if (Tour == null)
            {
                return NotFound();
            }

            // Update the TinTuc object with the form data
            Tour.MaTour = input.MaTour;
            Tour.MaNv = input.MaNV;
            Tour.TenTour= input.TenTour;
            Tour.XepHangTour=input.XepHangTour;
            Tour.MoTa = input.MoTa;
            
          

            // Upload the image to the server and update the TinTuc object with the new image name
            if (input.Anh != null)
            {
                string fileName = await UploadImage(input.Anh);
                Tour.Anh = fileName;
            }

            // Update the TinTuc in the database
            db.Tours.Update(Tour);
            await db.SaveChangesAsync();

            return Ok();
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
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "anhdaidien", fileName);
            // Save the file to disk
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }
        [HttpDelete]
        public IActionResult DeleteTour(string input)
        {
            var TourCheck = (from a in db.Tours
                           where a.MaTour == input
                           select a).FirstOrDefault();
                               
            if (TourCheck == null)
            {
                return BadRequest("Khong tim thay Tour!");
            }

            db.Tours.Remove(TourCheck);
            db.SaveChanges();

            return Ok(input);
        }
    }
}
