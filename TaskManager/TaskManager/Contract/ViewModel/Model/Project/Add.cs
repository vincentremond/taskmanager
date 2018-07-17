using System.ComponentModel.DataAnnotations;

namespace TaskManager.Contract.ViewModel.Model.Project
{
    public class Add
    {
        [Required]
        public string  Title { get; set; }
    }
}