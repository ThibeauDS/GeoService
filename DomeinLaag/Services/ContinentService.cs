using DomeinLaag.Exceptions;
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

        public ContinentService()
        {
        }
        #endregion

        #region Methods
        public bool BestaatContinent(string naam)
        {
            try
            {
                return _repository.BestaatContinent(naam);
            }
            catch (Exception ex)
            {
                throw new ContinentServiceException("BestaatContinent - error", ex);
            }
        }

        public bool BestaatContinent(int id)
        {
            try
            {
                return _repository.BestaatContinent(id);
            }
            catch (Exception ex)
            {
                throw new ContinentServiceException("BestaatContinent - error", ex);
            }
        }

        public Continent ContinentToevoegen(Continent continent)
        {
            try
            {
                if (BestaatContinent(continent.Naam))
                {
                    throw new ContinentServiceException("Continent bestaat al met deze naam.");
                }
                return _repository.ContinentToevoegen(continent);
            }
            catch (Exception ex)
            {
                throw new ContinentServiceException("ContinentToevoegen - error", ex);
            }
        }

        public Continent ContinentWeergeven(int continentId)
        {
            try
            {
                if (!_repository.BestaatContinent(continentId))
                {
                    throw new ContinentServiceException("Continent bestaat niet met deze id.");
                }
                return _repository.ContinentWeergeven(continentId);
            }
            catch (Exception ex)
            {
                throw new ContinentServiceException("ContinentWeergeven - error", ex);
            }
        }

        public void ContinentVerwijderen(int continentId)
        {
            try
            {
                if (!_repository.BestaatContinent(continentId))
                {
                    throw new ContinentServiceException("Continent bestaat niet met deze id.");
                }
                _repository.ContinentVerwijderen(continentId);
            }
            catch (Exception ex)
            {
                throw new ContinentServiceException("ContinentVerwijderen - error", ex);
            }
        }

        public Continent ContinentUpdaten(Continent continent)
        {
            if (continent == null)
            {
                throw new ContinentServiceException("Continent is null.");
            }
            if (!_repository.BestaatContinent(continent.Id))
            {
                throw new ContinentServiceException("Klant bestaat niet.");
            }
            Continent continentDb = ContinentWeergeven(continent.Id);
            if (continentDb == continent)
            {
                throw new ContinentServiceException("Er zijn geen verschillen met het origineel.");
            }
            _repository.ContinentUpdaten(continent);
            return continent;
        }
        #endregion
    }
}
