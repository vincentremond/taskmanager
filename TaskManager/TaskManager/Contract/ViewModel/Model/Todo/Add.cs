using System.ComponentModel.DataAnnotations;

namespace TaskManager.Contract.ViewModel.Model.Todo
{
    public class Add
    {
        [Required]
        public string  Title { get; set; }
    }
}