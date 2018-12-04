using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StarterAPI.Models
{
    public class Todo : Entity
    {
        [MinLength(3)]
        public string Title { get; set; }
        public bool IsDone { get; set; }
    }
}
