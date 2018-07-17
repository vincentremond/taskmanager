﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaskManager.Contract.ViewModel.Model.Todo
{
    public class EditViewModel : Edit
    {
        public Data ViewData { get; set; }

        public class Data
        {
            public IEnumerable<SelectListItem> Contexts { get; set; }
        }
    }
}