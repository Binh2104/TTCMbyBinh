﻿using QBTourDuLich.Models;
namespace QBTourDuLich.InputModelsApi
{
    public class KhachSanUpdateInputMode
    {
        public string MaKs { get; set; } = null!;
        public string MaNV { get; set; } = null!;
        public string TenKs { get; set; } = null!;

        public string DiaChi { get; set; } = null!;

        public string Sdt { get; set; } = null!;

        public int XepHangKs { get; set; }
        


        public IFormFile? TenFileAnh { get; set; } = null!;
    }
}
