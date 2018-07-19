using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaskManager.Contract.ViewModel.Model.Todo
{
    public class EditViewModel : Edit
    {
        public Data ViewData { get; set; }

        public class Data
        {
            public IEnumerable<SelectListItem> Contexts { get; set; }
            public IEnumerable<SelectListItem> Projects { get; set; }
            public IEnumerable<SelectListItem> RepeatTypes { get; set; }
            public IEnumerable<SelectListItem> RepeatUnits { get; set; }
        }
    }
}
