using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HadramutSite.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        
        [Required(ErrorMessage = "write your user name")]
        [MaxLength(255)]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "write your user password")]
        [MaxLength(255)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DefaultValue(1)]
        public bool IsAdmin { get; set; }

        List<User_Subject> UserSubject = new List<User_Subject>();
    }
}
