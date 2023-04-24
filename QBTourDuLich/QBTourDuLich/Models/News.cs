using System;
using System.Collections.Generic;

namespace QBTourDuLich.Models;

public partial class News
{
    public string MaTin { get; set; } = null!;

    public string MoTa { get; set; } = null!;

    public string TenFileAnh { get; set; } = null!;

    public string NoiDung { get; set; } = null!;

    public string MaNv { get; set; } = null!;

    public virtual NhanVien MaNvNavigation { get; set; } = null!;
}
