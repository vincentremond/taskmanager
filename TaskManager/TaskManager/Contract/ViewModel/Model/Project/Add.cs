using System.ComponentModel.DataAnnotations;

namespace TaskManager.Contract.ViewModel.Model.Project
{
    public class Add
    {
        [Required]
        public string  Title { get; set; }
        [Required, RegularExpression(Constants.ColorRegex)]
        public string Color { get; set; }
    }
}
