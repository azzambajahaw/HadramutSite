using HadramutSite.Models;
using System.ComponentModel.DataAnnotations;

namespace HadramutSite.ModelFile
{
    // from view to controller
    public class QuestionFile : Question
    {
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }
        public int v = 7;
    }
}
