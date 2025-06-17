using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Core.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //navigation properties
        public ICollection<PokemonCategory> PokemonCategory { get; set; } = new List<PokemonCategory>();
    }
}
