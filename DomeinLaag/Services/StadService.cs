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
    public class StadService
    {
        #region Properties
        private readonly IStadRepository _repository;
        #endregion

        #region Constructors
        public StadService(IStadRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public List<Stad> GeefStedenLand(int id)
        {
            try
            {
                return _repository.GeefStedenLand(id);
            }
            catch (Exception ex)
            {
                throw new StadServiceException("GeefLandenContinent - error", ex);
            }
        }

        public bool HeeftSteden(int landId)
        {
            try
            {
                return _repository.HeeftSteden(landId);
            }
            catch (Exception ex)
            {
                throw new StadServiceException("HeeftSteden - error", ex);
            }
        }

        public Stad StadToevoegen(Stad stad)
        {
            try
            {
                if (_repository.BestaatStad(stad.Id))
                {
                    throw new StadServiceException("Stad bestaat al.");
                }
                return _repository.StadToevoegen(stad);
            }
            catch (Exception ex)
            {
                throw new StadServiceException("StadToevoegen - error", ex);
            }
        }

        public Stad StadWeergeven(int stadId)
        {
            try
            {
                if (!_repository.BestaatStad(stadId))
                {
                    throw new StadServiceException("Stad bestaat niet.");
                }
                return _repository.StadWeergeven(stadId);
            }
            catch (Exception ex)
            {
                throw new StadServiceException("StadWeergeven - error", ex);
            }
        }

        public bool BestaatStad(int stadId)
        {
            try
            {
                return _repository.BestaatStad(stadId);
            }
            catch (Exception ex)
            {
                throw new StadServiceException("BestaatStad - error", ex);
            }
        }

        public void StadVerwijderen(int stadId)
        {
            try
            {
                if (!_repository.BestaatStad(stadId))
                {
                    throw new StadServiceException("Stad bestaat niet.");
                }
                _repository.StadVerwijderen(stadId);
            }
            catch (Exception ex)
            {
                throw new StadServiceException("StadVerwijderen - error", ex);
            }
        }

        public Stad StadUpdaten(Stad stad)
        {
            try
            {
                if (!_repository.BestaatStad(stad.Id))
                {
                    throw new StadServiceException("Stad bestaat niet.");
                }
                return _repository.StadUpdaten(stad);
            }
            catch (Exception ex)
            {
                throw new StadServiceException("StadUpdaten - error", ex);
            }
        }

        public bool ControleerBevolkingsaantal(int landId, int bevolkingsaantal)
        {
            try
            {
                return _repository.ControleerBevolkingsaantal(landId, bevolkingsaantal);
            }
            catch (Exception ex)
            {
                throw new StadServiceException("ControleerBevolkingsaantal - error", ex);
            }
        }
        #endregion
    }
}
