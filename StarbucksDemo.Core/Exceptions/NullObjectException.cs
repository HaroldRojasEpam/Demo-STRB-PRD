using StarbucksDemo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbucksDemo.Core.Exceptions
{
    public class NullObjectException(string message) : Exception
    {
        public static void ThrowObjectNullable(object obj, EntityType enumType)
        {
            if (obj == null)
            {
                ThrowException(enumType);
            }
        }
        public static void ThrowException(EntityType enumType)
        {
            throw new NullObjectException($"The {enumType} was not found");
        }
    }
}
