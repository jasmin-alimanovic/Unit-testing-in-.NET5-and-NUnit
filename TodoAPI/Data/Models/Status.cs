﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAPI.Data.Models
{
    public class Status
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Todo> Todos { get; set; }
    }
}
