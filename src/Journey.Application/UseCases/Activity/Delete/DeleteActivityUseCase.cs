using Journey.Exception.ExceptionsBase;
using Journey.Exception;
using Journey.Infrastructure;

namespace Journey.Application.UseCases.Activity.Delete
{
    public class DeleteActivityUseCase
    {
        public void Execute(Guid tripId, Guid activityId)
        {
            var dbcontext = new JorneyDbContext();

            var activity = dbcontext
                .Activities
                .FirstOrDefault(activity => activity.TripId == tripId && activity.Id == activityId);

            if (activity == null)
            {
                throw new NotFoundException(ResourceErrorMessage.ACTIVITY_NOT_FOUND);
            }

            
            dbcontext.Activities.Remove(activity);
            dbcontext.SaveChanges();
        }
    }
}
