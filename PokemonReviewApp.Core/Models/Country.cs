using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Core.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //navigation properties
        public ICollection<Owner> Owners { get; set; } = new List<Owner>();
    }
}
