using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAPI.Data.Models.DTOs
{
    public class TodoUpdateDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime FinishedAt { get; set; }

        public bool IsDeleted { get; set; }

        /// <summary>
        /// todo category props
        /// </summary>
        public int TodoCategoryId { get; set; }

        /// <summary>
        /// status props
        /// </summary>
        public int StatusId { get; set; }
    }
}
