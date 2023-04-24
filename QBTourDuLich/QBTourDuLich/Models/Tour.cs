using System;
using System.Collections.Generic;

namespace QBTourDuLich.Models;

public partial class Tour
{
    public string MaTour { get; set; } = null!;

    public string TenTour { get; set; } = null!;

    public string Anh { get; set; } = null!;

    public int XepHangTour { get; set; }

    public string MoTa { get; set; } = null!;

    public string MaNv { get; set; } = null!;

    public virtual ICollection<DiaDiemTour> DiaDiemTours { get; set; } = new List<DiaDiemTour>();

    public virtual NhanVien MaNvNavigation { get; set; } = null!;
}
