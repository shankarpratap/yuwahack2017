//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace yuwamt
{
    using System;
    using System.Collections.Generic;
    
    public partial class CoachAttendance
    {
        public int Coachid { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public string CoachedTeam { get; set; }
    
        public virtual CoachDetail CoachDetail { get; set; }
    }
}