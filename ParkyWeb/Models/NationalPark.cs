using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb.Models
{
    public class NationalPark
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string State { get; set; }

        public DateTime Created { get; set; }
        public byte[] Picture { get; set; }

        public DateTime Established { get; set; }

        //[NotMapped]
        //public IFormFile PictureFile { get; set; }
    }
}