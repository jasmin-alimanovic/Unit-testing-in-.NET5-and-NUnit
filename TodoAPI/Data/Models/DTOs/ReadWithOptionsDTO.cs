using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAPI.Data.Models.DTOs
{
    public class ReadWithOptionsDTO<T>
    {
        public int Count { get; set; }
        public string Previous { get; set; }

        public string Next { get; set; }

        public IEnumerable<T> Results { get; set; }
    }
}
