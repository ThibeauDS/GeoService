using DomeinLaag.Interfaces;
using DomeinLaag.Klassen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeinLaag.Services
{
    public class ContinentService
    {
        #region Properties
        private readonly IContinentRepository _repository;
        #endregion

        #region Constructors
        public ContinentService(IContinentRepository repository)
        {
            _repository = repository;
        }

        public bool BestaatContinent(string naam)
        {
            throw new NotImplementedException();
        }

        public Continent ContinentToevoegen(Continent continent)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Methods

        #endregion
    }
}
