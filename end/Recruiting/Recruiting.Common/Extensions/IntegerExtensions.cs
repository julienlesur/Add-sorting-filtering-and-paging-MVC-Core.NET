

using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace System
{
    public static class IntegerExtensions
    {
        public static bool TryParsePropertyExtension(this int @this, PropertyInfo propertyInfo, ValidationContext validationContext, out int result)
        {
            return int.TryParse(propertyInfo.GetValue(validationContext.ObjectInstance, null)?.ToString(), out result);
        }
    }
}
