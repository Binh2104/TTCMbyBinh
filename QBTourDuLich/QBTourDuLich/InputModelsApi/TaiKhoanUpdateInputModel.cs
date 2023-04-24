using QBTourDuLich.Models;
namespace QBTourDuLich.InputModelsApi
{
    public class TaiKhoanUpdateInputModel
    {
        public string UserName { get; set; } = null!;
        public string Loai { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!;
    }
}
