using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Infrastructure.Entities;
using Journey.Infrastructure;

namespace Journey.Application.UseCases.Trips.GetAll
{
    public class GetAllUseCase
    {
        public ResponseTripsJson Execute()
        {            
            var dbcontext = new JorneyDbContext();

            var trips = dbcontext.Trips.ToList();

            return new ResponseTripsJson
            {
                Trips = trips.Select(trip => new ResponseShortTripJson
                {
                    Id = trip.Id,
                    EndDate = trip.EndDate,
                    StartDate = trip.StartDate,
                    Name = trip.Name
                }).ToList()
            };

        }
    }
}
