using System.ComponentModel.DataAnnotations;

namespace TaskManager.Contract.ViewModel.Model.Context
{
    public class Edit
    {
        public string ContextId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required, RegularExpression(Constants.ColorRegex)]
        public string Color { get; set; }
    }
}
