using System;
using System.Collections.Generic;

namespace QBTourDuLich.Models;

public partial class NhanVien
{
    public string MaNv { get; set; } = null!;

    public string TenNv { get; set; } = null!;

    public string GioiTinh { get; set; } = null!;

    public string SoDienThoai { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<KhachSan> KhachSans { get; set; } = new List<KhachSan>();

    public virtual ICollection<News> News { get; set; } = new List<News>();

    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();

    public virtual TaiKhoan UserNameNavigation { get; set; } = null!;
}
