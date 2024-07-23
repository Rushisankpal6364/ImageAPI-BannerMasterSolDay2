using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day2Soliteck.Model
{
    public class User
    {

        public int Id { get; set; }
        [Required]
        [DisplayName("Name")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("Email")]
        public String UserEmail { get; set; }
        [Required]
        [DisplayName("Phone No")]
        public String UserPhoneno { get; set; }
        [Required]
        [DisplayName("Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [DisplayName("RoleId")]
        public int RoleId { get; set; }

    }
}