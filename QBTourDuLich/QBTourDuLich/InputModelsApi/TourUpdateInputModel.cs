using QBTourDuLich.Models;
namespace QBTourDuLich.InputModelsApi
{
    public class TourUpdateInputModel
    {
        public string MaTour { get; set; } = null!;
        public string MaNV { get; set; } = null!;

        public string TenTour { get; set; } = null!;



        public string Anh { get; set; } = null!;



        public int XepHangTour { get; set; }

        public string MoTa { get; set; } = null!;
    }
}
