//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Documentum
{
    using System;
    using System.Collections.Generic;
    
    public partial class UcenikDokument
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UcenikDokument()
        {
            this.UcenikBookmarks = new HashSet<UcenikBookmark>();
        }
    
        public int Id { get; set; }
        public Nullable<int> ucenikId { get; set; }
        public Nullable<int> dokumentTipId { get; set; }
        public string dokumentPath { get; set; }
        public Nullable<int> status { get; set; }
    
        public virtual DokumentTip DokumentTip { get; set; }
        public virtual Ucenik Ucenik { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UcenikBookmark> UcenikBookmarks { get; set; }
    }
}
