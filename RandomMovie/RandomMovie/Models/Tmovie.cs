using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RandomMovie.Models
{
    public partial class Tmovie
    {
        public int Idmovie { get; set; }
        public string Title { get; set; }
        public int? Year { get; set; }
        public bool? Pick { get; set; }
        public DateTime? PickDate { get; set; }
    }
}
