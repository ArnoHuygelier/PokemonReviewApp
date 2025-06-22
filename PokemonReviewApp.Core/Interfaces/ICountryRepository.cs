using PokemonReviewApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Core.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country? GetCountry(int countryId);
        Country? GetCountryByOwner(int ownerId);
        ICollection<Owner> GetOwnersFromCountry(int countryId);
        bool CoutryExists(int countryId);

    }
}
