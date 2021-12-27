using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAPI.Data.Models.DTOs
{
    public class TodoCreateDTO
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [ForeignKey("TodoCategory")]
        public int TodoCategoryId { get; set; }

        [Required]
        public int StatusId { get; set; }



    }
}
