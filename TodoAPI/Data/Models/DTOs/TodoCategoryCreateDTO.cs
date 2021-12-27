using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAPI.Data.Models.DTOs
{
    public class TodoCategoryCreateDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
