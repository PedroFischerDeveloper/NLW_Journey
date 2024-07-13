using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripUseCase
    {
        public ResponseShortTripJson Execute(RequestRegisterTripJson request)
        {
            Validate(request);
            
            var validator = new RegisterTripValidator();

            var dbcontext = new JorneyDbContext();

            var entity = new Trip
            {
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            };


            dbcontext.Trips.Add(entity);
            dbcontext.SaveChanges();

            return new ResponseShortTripJson
            {
                EndDate = entity.EndDate,
                StartDate = entity.StartDate,
                Name = request.Name,
                Id = entity.Id
            };
        }
        
        private void Validate(RequestRegisterTripJson request) 
        {
            var validator = new RegisterTripValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var erroMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            
                throw new ErrorOnValidateException(erroMessages);
            }


        }
    }
}
