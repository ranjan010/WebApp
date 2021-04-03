using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Model
{
    public class EmployeeModel
    {
      
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter your name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Second Name is Compulsory")]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage ="Please Enter Valid Email ")]
        public string Email { get; set; }
        public int? AddressId { get; set; }
        public string Code { get; set; }
        public AddressModel Address{ get; set; }

    }
}
