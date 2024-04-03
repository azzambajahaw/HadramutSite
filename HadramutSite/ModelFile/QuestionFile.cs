using HadramutSite.Models;
using System.ComponentModel.DataAnnotations;

namespace HadramutSite.ModelFile
{
    // from view to controller
    public class QuestionFile : Question
    {
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }
<<<<<<< Updated upstream
        public int v = 7;
=======
        public int bb {get; set;}

>>>>>>> Stashed changes
    }
}
