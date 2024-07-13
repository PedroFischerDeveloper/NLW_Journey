using FluentValidation.Results;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;


namespace Journey.Application.UseCases.Activity.Register
{
    public class RegisterActivityUseCase
    {
        public ResponseActivityJson Execute(Guid tripId, RequestRegisterActivityJson request)
        {
            var dbcontext = new JorneyDbContext();

            var trip = dbcontext
                .Trips
                .FirstOrDefault(trip => trip.Id == tripId);

            if (trip == null)
            {
                throw new NotFoundException(ResourceErrorMessage.TRIP_NOT_FOUND);
            }

            Validate(trip, request);

            var entity = new Infrastructure.Entities.Activity
            {
                Name = request.Name,
                Date = request.Date,
                Id = trip.Id,
            };
            
            trip!.Activities.Add(entity);    

            dbcontext.Activities.Add(entity);
            dbcontext.SaveChanges();

            return new ResponseActivityJson
            {
                Date = entity.Date,
                Id = entity.Id,
                Name = entity.Name,
            };
        }

        private void Validate(Trip trip, RequestRegisterActivityJson request)
        {
            var validator = new RegisterActivityValidator();

            var result = validator.Validate(request);

            if (request.Date < trip.StartDate && request.Date <= trip.EndDate == false)
            {
                result.Errors.Add(new ValidationFailure("Date", ResourceErrorMessage.DATE_NOT_WIITHIN_TRAVEL_PERIOD));
            }

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            }

        }
    }
}
