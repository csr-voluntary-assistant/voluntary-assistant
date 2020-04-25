using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voluntariat.Data.Repositories;
using Voluntariat.Models;

namespace Voluntariat.Services
{
    public interface IVolunteerService 
    {
        List<Volunteer> GetVolunteersInRangeForBeneficiary(ApplicationUser beneficiary, int radiusInMeters);
    }

    public class VolunteerService : IVolunteerService
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IVolunteerMatchingService _volunteerMatchingService;

        public VolunteerService(IVolunteerRepository volunteerRepository, IVolunteerMatchingService volunteerMatchingService)
        {
            _volunteerRepository = volunteerRepository;
            _volunteerMatchingService = volunteerMatchingService;
        }

        public List<Volunteer> GetVolunteersInRangeForBeneficiary(ApplicationUser beneficiary, int radiusInMeters) 
        {
            IQueryable<Volunteer> volunteers = _volunteerRepository.GetVolunteers();
            return _volunteerMatchingService.GetVolunteersInRange(volunteers, beneficiary, radiusInMeters).ToList();
        }
    }
}
