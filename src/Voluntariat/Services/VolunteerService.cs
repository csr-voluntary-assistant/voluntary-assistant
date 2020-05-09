using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
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
        List<ApplicationUser> GetVolunteersInRangeForBeneficiary(ApplicationUser beneficiary, int radiusInKm);
        void NotifyUnaffiliatedVolunteers();
        void DeleteUnaffiliatedVolunteers();
    }

    public class VolunteerService : IVolunteerService
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IVolunteerMatchingService _volunteerMatchingService;
        private readonly IEmailSender _emailSender;

        public VolunteerService(IVolunteerRepository volunteerRepository, IVolunteerMatchingService volunteerMatchingService, IEmailSender emailSender)
        {
            _volunteerRepository = volunteerRepository;
            _volunteerMatchingService = volunteerMatchingService;
            _emailSender = emailSender;
        }

        public List<ApplicationUser> GetVolunteersInRangeForBeneficiary(ApplicationUser beneficiary, int radiusInKm)
        {
            IQueryable<ApplicationUser> volunteers = _volunteerRepository.GetVolunteers();
            return _volunteerMatchingService.GetVolunteersInRange(volunteers, beneficiary, radiusInKm).ToList();
        }

        public void NotifyUnaffiliatedVolunteers()
        {
            var volunteerUsers = _volunteerRepository.GetVolunteers();
            var volunteers = _volunteerRepository.GetUnaffiliatedVolunteers();
            var availableForNotificationVolunteerIds = volunteers
                .Where(v => v.UnaffiliationStartTime.Value.Date.AddMonths(1) < DateTime.UtcNow.Date)
                .Select(v => v.ID.ToString())
                .ToList();

            var availableForNotificationVolunteers = volunteerUsers.Where(u => availableForNotificationVolunteerIds.Contains(u.Id));
            foreach (var volunteer in availableForNotificationVolunteers)
            {
                _emailSender.SendEmailAsync(volunteer.Email, "Unaffiliation notification", 
                    $"Dear {volunteer.UserName}, <br/> Your account will be deleted due to unaffiliation in 48 hours.");
            }
        }

        public void DeleteUnaffiliatedVolunteers()
        {
            var volunteers = _volunteerRepository.GetUnaffiliatedVolunteers();
            var toBeRemovedVolunteerIds = volunteers
                .Where(v => v.UnaffiliationStartTime.Value.Date.AddMonths(1).AddDays(2) < DateTime.UtcNow.Date)
                .AsNoTracking()
                .Select(v => v.ID)
                .ToArray();

            _volunteerRepository.RemoveVolunteers(toBeRemovedVolunteerIds);
        }
    }
}
