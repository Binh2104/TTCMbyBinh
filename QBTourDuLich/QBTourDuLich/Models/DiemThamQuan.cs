using System;
using System.Collections.Generic;

namespace QBTourDuLich.Models;

public partial class DiemThamQuan
{
    public string MaDd { get; set; } = null!;

    public string Tendiadiem { get; set; } = null!;

    public string MoTa { get; set; } = null!;

    public string TenFileAnh { get; set; } = null!;

    public virtual ICollection<DiaDiemTour> DiaDiemTours { get; set; } = new List<DiaDiemTour>();
}
