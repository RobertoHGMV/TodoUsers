using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace TodoUsers.Common.Entities
{
    public class Entity
    {
        public Entity()
        {
            _errors = new List<ValidationFailure>();
            Valid = true;
        }

        IList<ValidationFailure> _errors { get; set; }
        public IList<ValidationFailure> Notifications => _errors;

        public bool Valid { get; private set; }
        public bool Invalid => !Valid;

        public void Validate<T>(T entity, AbstractValidator<T> validator)
        {
            var result = validator.Validate(entity);
            _errors.Clear();
            _errors = result.Errors;
            Valid = result.IsValid;
        }
    }
}
