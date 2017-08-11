namespace CustomerContact.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserSys
    {
       
        [NotMapped]
        public UserRole Role { get; set; }
    }
}
