//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyChuoiCuaHangCoffee.Models.DataProvider
{
    using System;
    using System.Collections.Generic;
    
    public partial class CHUCDANH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CHUCDANH()
        {
            this.NHANVIENs = new HashSet<NHANVIEN>();
        }
    
        public string MACD { get; set; }
        public string TENCHUCDANH { get; set; }
        public Nullable<double> HESOLUONG { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHANVIEN> NHANVIENs { get; set; }
    }
}
