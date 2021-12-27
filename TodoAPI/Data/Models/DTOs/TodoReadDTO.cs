using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAPI.Data.Models.DTOs
{
    public class TodoReadDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        
        public DateTime CreatedAt { get; set; }


        public DateTime FinishedAt { get; set; }

        /// <summary>
        /// todo category props
        /// </summary>

        public TodoCategoryReadDTO TodoCategory { get; set; }

        /// <summary>
        /// status props
        /// </summary>

        public StatusReadDTO Status { get; set; }
    }
}
