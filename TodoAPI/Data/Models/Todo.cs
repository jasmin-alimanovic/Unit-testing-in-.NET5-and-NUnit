using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAPI.Data.Models
{
    public class Todo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? FinishedAt { get; set; }

        public bool IsDeleted { get; set; }

        /// <summary>
        /// todo category props
        /// </summary>
        public int TodoCategoryId { get; set; }

        public TodoCategory TodoCategory { get; set; }

        /// <summary>
        /// status props
        /// </summary>
        public int StatusId { get; set; }

        public Status Status { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        public Todo()
        {
            CreatedAt = DateTime.Now;
            IsDeleted = false;
            StatusId = 1;
            FinishedAt = null;
        }
    }
}
