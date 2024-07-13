using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.GetById
{
    public class DeleteByIdUseCase
    {
        public ResponseTripJson Execute(Guid id)
        {
            var dbcontext = new JorneyDbContext();

            var trip = dbcontext
                .Trips
                .Include(trip => trip.Activities)
                .FirstOrDefault(trip => trip.Id == id);

            if (trip == null)
            {
                throw new NotFoundException(ResourceErrorMessage.TRIP_NOT_FOUND);
            }

            return new ResponseTripJson
            {
                Id = trip.Id,
                EndDate = trip.EndDate,
                StartDate = trip.StartDate,
                Name = trip.Name,
                Activities = trip.Activities.Select(activity => new ResponseActivityJson
                {
                    Id = activity.Id,
                    Name = activity.Name,
                    Status = (Communication.Enums.ActivityStatus) activity.Status,
                    Date = activity.Date,
                }).ToList()
            };

        }
    }
}
