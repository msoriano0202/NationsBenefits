using NationsBenefits.Application.Constants;

namespace NationsBenefits.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key) : base(string.Format(ErrorMessages.EntityNotFound, name, key))
        {
        }
    }
}
