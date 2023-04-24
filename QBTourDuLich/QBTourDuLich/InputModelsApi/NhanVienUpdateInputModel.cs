using QBTourDuLich.Models;
namespace QBTourDuLich.InputModelsApi
{
    public class NhanVienUpdateInputModel
    {
        public string MaNV { get; set; } = null!;
        public string TenNV { get; set; } = null!;
        public string GioiTinh { get; set; } = null!;

        public string SoDienThoai { get; set; } = null!;
        public string DiaChi { get; set; } = null!;
    }
}
