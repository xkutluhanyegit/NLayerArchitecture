using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using FluentValidation;

namespace Core.Aspects.AspectCore.Validation
{
    public class ValidationAspect : AbstractInterceptorAttribute
    {
        private readonly Type _validatorType;

        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new ArgumentException("Geçersiz validator tipi.");
            }

            _validatorType = validatorType;
        }

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            // Metodun parametrelerini al
            var parameters = context.Parameters;

            // Validator örneği oluştur
            var validator = (IValidator)Activator.CreateInstance(_validatorType);

            foreach (var parameter in parameters)
            {
                var validationContext = new ValidationContext<object>(parameter);
                var validationResult = await validator.ValidateAsync(validationContext);

                if (!validationResult.IsValid)
                {
                    var result = validator.Validate((IValidationContext)parameter);
                    if (!result.IsValid)
                    {
                        throw new ValidationException(result.Errors);
                    }
                }
            }

            await next(context);
        }
    }


    
}