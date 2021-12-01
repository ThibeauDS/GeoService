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
    public class LandService
    {
        #region Properties
        private readonly ILandRepository _repository;
        #endregion

        #region Constructors
        public LandService(ILandRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public List<Land> GeefLandenContinent(int id)
        {
            try
            {
                return _repository.GeefLandenContinent(id);
            }
            catch (Exception ex)
            {
                throw new LandServiceException("GeefLandenContinent - error", ex);
            }
        }

        public bool HeeftLanden(int continentId)
        {
            try
            {
                return _repository.HeeftLanden(continentId);
            }
            catch (Exception ex)
            {
                throw new LandServiceException("HeeftLanden - error", ex);
            }
        }

        public Land LandToevoegen(Land land)
        {
            try
            {
                return _repository.LandToevoegen(land);
            }
            catch (Exception ex)
            {
                throw new LandServiceException("LandToevoegen - error", ex);
            }
        }

        public Land LandWeergeven(int landId)
        {
            try
            {
                if (!_repository.BestaatLand(landId))
                {
                    throw new LandServiceException("Land bestaat niet.");
                }
                return _repository.LandWeergeven(landId);
            }
            catch (Exception ex)
            {
                throw new LandServiceException("LandWeergeven - error", ex);
            }
        }

        public void LandVerwijderen(int landId)
        {
            try
            {
                if (!_repository.BestaatLand(landId))
                {
                    throw new LandServiceException("Land bestaat niet.");
                }
                _repository.LandVerwijderen(landId);
            }
            catch (Exception ex)
            {
                throw new LandServiceException("LandVerwijderen - error", ex);
            }
        }

        public Land LandUpdaten(Land land)
        {
            try
            {
                if (!_repository.BestaatLand(land.Id))
                {
                    throw new LandServiceException("Land bestaat niet.");
                }
                return _repository.LandUpdaten(land);
            }
            catch (Exception ex)
            {
                throw new LandServiceException("LandUpdaten - error", ex);
            }
        }

        public bool BestaatLand(int landId)
        {
            try
            {
                return _repository.BestaatLand(landId);
            }
            catch (Exception ex)
            {
                throw new LandServiceException("BestaatLand - error", ex);
            }
        }
        #endregion
    }
}
