using Geolocation;
using System.Collections.Generic;
using System.Linq;
using Voluntariat.Models;

namespace Voluntariat.Services
{
    public interface IVolunteerMatchingService
    {
        bool IsInRange(ApplicationUser volunteer, ApplicationUser beneficiary);

        IEnumerable<ApplicationUser> GetVolunteersInRange(IQueryable<ApplicationUser> volunteers, ApplicationUser beneficiary, int radiusInKm);

        List<ApplicationUser> GetBeneficiariesInRange(List<ApplicationUser> beneficiaries, ApplicationUser volunteer, int radiusInKm);
    }

    public class VolunteerMatchingService : IVolunteerMatchingService
    {
        public bool IsInRange(ApplicationUser volunteer, ApplicationUser beneficiary)
        {
            Coordinate volunteerCoordinates = new Coordinate(volunteer.Latitude, volunteer.Longitude);
            Coordinate beneficiaryCoordinates = new Coordinate(beneficiary.Latitude, beneficiary.Longitude);

            double distance = GeoCalculator.GetDistance(volunteerCoordinates, beneficiaryCoordinates, 15, DistanceUnit.Kilometers);

            return distance <= (double)volunteer.RangeInKm;
        }

        public IEnumerable<ApplicationUser> GetVolunteersInRange(IQueryable<ApplicationUser> volunteers, ApplicationUser beneficiary, int radiusInKm)
        {
            CoordinateBoundaries boundaries = new CoordinateBoundaries(beneficiary.Latitude, beneficiary.Longitude, radiusInKm, DistanceUnit.Kilometers);

            return volunteers
               .Where(v => v.Latitude >= boundaries.MinLatitude && v.Latitude <= boundaries.MaxLatitude)
               .Where(v => v.Longitude >= boundaries.MinLongitude && v.Longitude <= boundaries.MaxLongitude)
               .AsEnumerable()
               .Where(v => IsInRange(v, beneficiary));
        }

        public List<ApplicationUser> GetBeneficiariesInRange(List<ApplicationUser> beneficiaries, ApplicationUser volunteer, int radiusInKm)
        {
            CoordinateBoundaries boundaries = new CoordinateBoundaries(volunteer.Latitude, volunteer.Longitude, radiusInKm, DistanceUnit.Meters);

            return beneficiaries
               .Where(b => b.Latitude >= boundaries.MinLatitude && b.Latitude <= boundaries.MaxLatitude)
               .Where(b => b.Longitude >= boundaries.MinLongitude && b.Longitude <= boundaries.MaxLongitude)
               .AsEnumerable()
               .Where(b => IsInRange(volunteer, b)).ToList();
        }
    }
}
