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
    
    public partial class StudentDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudentDetail()
        {
            this.Siblings = new HashSet<Sibling>();
            this.AttendanceRecords = new HashSet<AttendanceRecord>();
        }
    
        public int id { get; set; }
        public Nullable<System.Guid> O365ID { get; set; }
        public System.DateTime Birthdate { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Student_Pic { get; set; }
        public string Blood_Group { get; set; }
        public string Village { get; set; }
        public Nullable<int> Phone { get; set; }
        public Nullable<System.DateTime> DOJ_Yuwa { get; set; }
        public Nullable<int> Familyid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string middlename { get; set; }
        public Nullable<bool> is_student { get; set; }
        public Nullable<bool> is_coach { get; set; }
    
        public virtual FamilyDetail FamilyDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sibling> Siblings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttendanceRecord> AttendanceRecords { get; set; }
    }
}