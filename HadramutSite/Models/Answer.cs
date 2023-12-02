using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HadramutSite.Models
{
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [Range(100, 5000, ErrorMessage = "the Answer shulde be more 100 and less 5000 charatcher")]
        public string Content { get; set; }

        public string Image { get; set; }

        [ForeignKey("Question")]
        public int QuestionID { get; set; }

        public Question Question { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        public User User { get; set; }
    }

}
