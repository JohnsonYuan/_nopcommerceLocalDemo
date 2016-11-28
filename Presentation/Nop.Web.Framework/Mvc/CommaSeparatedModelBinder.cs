using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Nop.Web.Framework.Mvc
{
    public class CommaSeparatedModelBinder : DefaultModelBinder
    {
        private static readonly MethodInfo ToArrayMethod = typeof(Enumerable).GetMethod("ToArray");

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return BingCsv(bindingContext.ModelType, bindingContext.ModelName, bindingContext)
                ?? base.BindModel(controllerContext, bindingContext);
        }

        protected override object GetPropertyValue(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, IModelBinder propertyBinder)
        {
            return BingCsv(propertyDescriptor.PropertyType, propertyDescriptor.Name, bindingContext)
                ?? base.GetPropertyValue(controllerContext, bindingContext, propertyDescriptor, propertyBinder);
        }

        private object BingCsv(Type type, string name, ModelBindingContext bindingContext)
        {
            if (type.GetInterface(typeof(IEnumerable).Name) != null)
            {
                var actualValue = bindingContext.ValueProvider.GetValue(name);

                if (actualValue != null)
                {
                    var valueType = type.GetElementType() ?? type.GetGenericArguments().FirstOrDefault();

                    var list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(valueType));

                    foreach (var splitValue in actualValue.AttemptedValue.Split(new[] { ',' }))
                    {
                        if (!String.IsNullOrEmpty(splitValue))
                            list.Add(Convert.ChangeType(splitValue, valueType));
                    }

                    if (type.IsArray)
                        return ToArrayMethod.MakeGenericMethod(valueType).Invoke(this, new[] { list });

                    return list;
                }
            }
            return null;
        }
    }
}
