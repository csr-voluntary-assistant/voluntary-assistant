using Geolocation;
using System.Collections.Generic;
using System.Linq;
using Voluntariat.Models;

namespace Voluntariat.Services
{
    public interface IVolunteerMatchingService
    {
        bool IsInRange(Volunteer volunteer, ApplicationUser beneficiary);

        IEnumerable<Volunteer> GetVolunteersInRange(IQueryable<Volunteer> volunteers, ApplicationUser beneficiary, int radiusInMeters);

        List<ApplicationUser> GetBeneficiariesInRange(List<ApplicationUser> beneficiaries, Volunteer volunteer, int radiusInMeters);
    }

    public class VolunteerMatchingService : IVolunteerMatchingService
    {
        public bool IsInRange(Volunteer volunteer, ApplicationUser beneficiary)
        {
            Coordinate volunteerCoordinates = new Coordinate(volunteer.User.Latitude, volunteer.User.Longitude);
            Coordinate beneficiaryCoordinates = new Coordinate(beneficiary.Latitude, beneficiary.Longitude);

            double distance = GeoCalculator.GetDistance(volunteerCoordinates, beneficiaryCoordinates, 15, DistanceUnit.Meters);

            return distance <= volunteer.RangeInMeters.Value;
        }

        public IEnumerable<Volunteer> GetVolunteersInRange(IQueryable<Volunteer> volunteers, ApplicationUser beneficiary, int radiusInMeters)
        {
            CoordinateBoundaries boundaries = new CoordinateBoundaries(beneficiary.Latitude, beneficiary.Longitude, radiusInMeters, DistanceUnit.Meters);

            return volunteers
               .Where(x => x.User.Latitude >= boundaries.MinLatitude && x.User.Latitude <= boundaries.MaxLatitude)
               .Where(x => x.User.Longitude >= boundaries.MinLongitude && x.User.Longitude <= boundaries.MaxLongitude)
               .AsEnumerable()
               .Where(v => IsInRange(v, beneficiary));
        }

        public List<ApplicationUser> GetBeneficiariesInRange(List<ApplicationUser> beneficiaries, Volunteer volunteer, int radiusInMeters)
        {
            CoordinateBoundaries boundaries = new CoordinateBoundaries(volunteer.User.Latitude, volunteer.User.Longitude, radiusInMeters, DistanceUnit.Meters);

            return beneficiaries
               .Where(b => b.Latitude >= boundaries.MinLatitude && b.Latitude <= boundaries.MaxLatitude)
               .Where(b => b.Longitude >= boundaries.MinLongitude && b.Longitude <= boundaries.MaxLongitude)
               .AsEnumerable()
               .Where(b => IsInRange(volunteer, b)).ToList();
        }
    }
}
