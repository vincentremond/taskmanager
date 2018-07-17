﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaskManager.Contract.ViewModel.Model.Todo
{
    public class Edit
    {
        public string TodoId { get; set; }
        [Required] public string Title { get; set; }
        [Range(1, 9999)] [Required] public int Complexity { get; set; }
        public string Description { get; set; }
        public string ContextId { get; set; }
        public string ProjectId { get; set; }
    }
}