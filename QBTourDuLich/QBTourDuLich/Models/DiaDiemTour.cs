using System;
using System.Collections.Generic;

namespace QBTourDuLich.Models;

public partial class DiaDiemTour
{
    public string ViTriAnh { get; set; } = null!;

    public string MaDd { get; set; } = null!;

    public string MaTour { get; set; } = null!;

    public virtual DiemThamQuan MaDdNavigation { get; set; } = null!;

    public virtual Tour MaTourNavigation { get; set; } = null!;
}
