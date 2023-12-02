using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HadramutSite.Models
{
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "write the subject name")]
        [MaxLength(255)]

        public string Name { get; set; }
        List<User_Subject> UserSubject { get; set; }
        List<Question> Questions { get; set; }



    }
}
