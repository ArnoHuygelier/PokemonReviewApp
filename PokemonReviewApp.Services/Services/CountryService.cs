using PokemonReviewApp.Core.Interfaces;
using PokemonReviewApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Services.Services
{
    public class CountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public ICollection<Country> GetCountries()
        {
            return _countryRepository.GetCountries();
        }

        public Country? GetCountry(int countryId)
        {
            return _countryRepository.GetCountry(countryId);
        }

        public Country? GetCountry(string countryName)
        {
            return _countryRepository.GetCountry(countryName);
        }

        public Country? GetCountryByOwner(int ownerId)
        {
            return _countryRepository.GetCountryByOwner(ownerId);
        }

        public ICollection<Owner> GetOwnersFromCountry(int countryId)
        {
            return _countryRepository.GetOwnersFromCountry(countryId);
        }

        public bool CountryExists(int countryId)
        {
            return _countryRepository.CoutryExists(countryId);
        }

        public bool CreateCountry(Country country)
        {
            if (country == null)
            {
                return false;
            }
            return _countryRepository.CreateCountry(country);
        }

        public bool UpdateCountry(Country country)
        {
            if (country == null)
            {
                return false;
            }
            return _countryRepository.UpdateCountry(country);
        }
    }
}
