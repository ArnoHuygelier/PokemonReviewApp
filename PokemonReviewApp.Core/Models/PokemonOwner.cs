﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Core.Models
{
    public class PokemonOwner
    {
        public int PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
