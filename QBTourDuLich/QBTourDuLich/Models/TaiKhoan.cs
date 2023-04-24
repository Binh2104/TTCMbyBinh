using System;
using System.Collections.Generic;

namespace QBTourDuLich.Models;

public partial class TaiKhoan
{
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Loai { get; set; }

    public string? ConfirmPassword { get; set; }

    public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
}
