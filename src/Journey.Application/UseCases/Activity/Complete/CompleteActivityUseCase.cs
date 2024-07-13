using Journey.Exception.ExceptionsBase;
using Journey.Exception;
using Journey.Infrastructure;
using Journey.Infrastructure.Enums;

namespace Journey.Application.UseCases.Activity.Complete
{
    public class CompleteActivityUseCase
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

            activity.Status = ActivityStatus.Done;

            dbcontext.Activities.Update(activity);
            dbcontext.SaveChanges();
        }
    }
}
