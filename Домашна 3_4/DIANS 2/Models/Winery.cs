using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DIANS_2.Models
{
    public class Winery
    {
        [Key]
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public float DefaultRating { get; set; } = 0;
        [Range(1, 10)]
        public float Rating { get; set; } 

        public Winery() { }

    }
}