using System;
using System.Collections.Generic;

namespace QBTourDuLich.Models;

public partial class KhachSan
{
    public string MaKs { get; set; } = null!;

    public string TenKs { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string Sdt { get; set; } = null!;

    public int XepHangKs { get; set; }

    public string TenFileAnh { get; set; } = null!;

    public string MaNv { get; set; } = null!;

    public virtual NhanVien MaNvNavigation { get; set; } = null!;
}
