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
    
    public partial class TinTuc
    {
        public int MaTinTuc { get; set; }
        public Nullable<int> MaDMTT { get; set; }
        public string AnhTinTuc { get; set; }
        public string TenTinTuc { get; set; }
        public string MoTa { get; set; }
        public string Chitiet { get; set; }
    
        public virtual DMTinTuc DMTinTuc { get; set; }
    }
}
