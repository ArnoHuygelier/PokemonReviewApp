using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Core.Models
{
    public class PokemonCategory
    {
        public int PokemonID { get; set; }
        public Pokemon Pokemon { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
