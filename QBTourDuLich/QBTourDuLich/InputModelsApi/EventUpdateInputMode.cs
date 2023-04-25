namespace QBTourDuLich.InputModelsApi
{
    public class EventUpdateInputMode
    {
        public string MaSK { get; set; } = null!;
        public string MaNv { get; set; } = null!;

        public IFormFile? Anh { get; set; } = null!;

        public string MoTa { get; set; } = null!;

        public string NoiDung { get; set; } = null!;

        
    }
}
