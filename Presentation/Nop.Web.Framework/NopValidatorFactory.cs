using System;
using FluentValidation;
using FluentValidation.Attributes;
using Nop.Core.Infrastructure;

namespace Nop.Web.Framework
{
    public class NopValidatorFactory : AttributedValidatorFactory
    {
        //我的注释 GetValidator.cs source:
        //By default, FluentValidation ships with an AttributedValidatorFactory that allows you to link a validator to the type that it validates by decorating the class to validate with an attribute that identifies its corresponding validator:
        //https://github.com/JeremySkinner/FluentValidation/blob/master/src/FluentValidation/Attributes/AttributedValidatorFactory.cs

        //private readonly InstanceCache _cache = new InstanceCache();
        public override IValidator GetValidator(Type type)
        {
            if (type != null)
            {
                // 我的注释, 调用的时候给class加属性 [Validator(typeof(PersonValidator))]
                var attribute = (ValidatorAttribute)Attribute.GetCustomAttribute(type, typeof(ValidatorAttribute));
                if ((attribute != null) && (attribute.ValidatorType != null))
                {
                    //validators can depend on some customer specific settings (such as working language)
                    //that's why we do not cache validators
                    //var instance = _cache.GetOrCreateInstance(attribute.ValidatorType,
                    //                           x => EngineContext.Current.ContainerManager.ResolveUnregistered(x));
                    var instance = EngineContext.Current.ContainerManager.ResolveUnregistered(attribute.ValidatorType);
                    return instance as IValidator;
                }
            }

            return null;
        }
    }
}
