using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CustomerContact.Core.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="This Field is Required")]
        public string email { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string password { get; set; }
    }
}
