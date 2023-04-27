using System;
using System.Collections.Generic;

namespace QBTourDuLich.Models;

public partial class Event
{
    public string MaSk { get; set; } = null!;

    public string MoTa { get; set; } = null!;

    public string NoiDung { get; set; } = null!;

    public string? TenFileAnh { get; set; } = null!;
    public string MaNv { get; set; } = null!;

    public virtual NhanVien MaNvNavigation { get; set; } = null!;
}
