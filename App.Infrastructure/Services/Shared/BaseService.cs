using App.Infrastructure.Exceptions.Shared;

namespace App.Infrastructure.Services.Shared
{
    public abstract class BaseService
    {
        protected static void CheckNULLReference<T>(T model, string message = "") where T : class
        {
            if (model == null)
            {
                message = string.IsNullOrWhiteSpace(message) ? "Entity.NotFound" : message;
                throw new EntityNotFoundException(message);
            }
        }
    }
}
