using System.ComponentModel.DataAnnotations;

namespace TaskManager.Contract.ViewModel.Model.Context
{
    public class Add
    {
        [Required]
        public string  Title { get; set; }
    }
}