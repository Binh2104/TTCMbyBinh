﻿using QBTourDuLich.Models;
namespace QBTourDuLich.InputModelsApi
{
    public class DiemThamQuanUpdateInputModel
    {
        public string Tendiadiem { get; set; } = null!;

        public string MaDd { get; set; } = null!;

        public string MoTa { get; set; } = null!;

        public IFormFile? TenFileAnh { get; set; } = null!;

        
    }
}
