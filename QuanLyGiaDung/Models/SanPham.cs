//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyGiaDung.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SanPham
    {
        public int MaSP { get; set; }
        public Nullable<int> MaDMSP { get; set; }
        public string AnhSP { get; set; }
        public string TenSP { get; set; }
        public string Mota { get; set; }
        public string Gia { get; set; }
        public string Sale { get; set; }
    
        public virtual DMSP DMSP { get; set; }
    }
}