//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyChuoiCuaHangCoffee.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class XUATKHO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public XUATKHO()
        {
            this.CTXUATKHOes = new HashSet<CTXUATKHO>();
        }
    
        public string MAPHIEU { get; set; }
        public string IDNHANVIEN { get; set; }
        public string TENKHO { get; set; }
        public System.DateTime NGXUATKHO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTXUATKHO> CTXUATKHOes { get; set; }
        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}