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
    
    public partial class SmerGodinaDokument
    {
        public int Id { get; set; }
        public Nullable<int> smerGodinaId { get; set; }
        public Nullable<int> dokumentTipId { get; set; }
    
        public virtual DokumentTip DokumentTip { get; set; }
        public virtual SmerGodina SmerGodina { get; set; }
    }
}