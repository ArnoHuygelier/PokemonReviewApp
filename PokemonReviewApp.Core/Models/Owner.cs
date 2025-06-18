using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Core.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gym { get; set; }

        // Navigation properties
        public Country Country { get; set; }
        public ICollection<PokemonOwner> PokemonOwners { get; set; } = new List<PokemonOwner>();
    }
}
