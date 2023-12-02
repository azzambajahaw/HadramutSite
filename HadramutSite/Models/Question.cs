using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HadramutSite.Models
{
    // from controller to db
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "write your question")]
        public string Content { get; set; }

        public string Image { get; set; }

        [ForeignKey("Subject")]
        public int SubjectID { get; set; }

        public Subject? Subject { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        public User? User { get; set; }

        List<Answer>? Answers { get; set; }
    }
}
