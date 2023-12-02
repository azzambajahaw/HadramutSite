using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HadramutSite.Models
{
    public class User_Subject
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        public User User { get; set; }

        [ForeignKey("Subject")]
        public int SubjectID { get; set; }

        public Subject Subject { get; set; }
    }

}
