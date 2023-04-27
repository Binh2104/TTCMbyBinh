namespace QBTourDuLich.InputModelsApi
{
    public class EventCreateInputMode
    {
        public string MaSK { get; set; } = null!;

        public IFormFile? Anh { get; set; } = null!;

        public string MoTa { get; set; } = null!;

        public string NoiDung { get; set; } = null!;
    }
}
