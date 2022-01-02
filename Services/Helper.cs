using System;
using System.Linq;

namespace DarkSoulsOBSOverlay.Services
{
    public static class Helper
    {
        /// <summary>
        /// Compares the properties of two objects of the same type and returns if all properties are equal.
        /// </summary>
        /// <param name="objectA">The first object to compare.</param>
        /// <param name="objectB">The second object to compre.</param>
        /// <returns><c>true</c> if all property values are equal, otherwise <c>false</c>.</returns>
        public static bool AreEqual<T>(T objectA, T objectB)
        {
            if (objectA == null && objectB == null) return true;

            if (objectA == null || objectB == null) return false;

            return objectA.GetType().GetProperties().ToList().All(p =>
            {
                if(CanDirectlyCompare(p.PropertyType))
                {
                    if (p.GetValue(objectA) == null && p.GetValue(objectB) == null) return true;
                    if (p.GetValue(objectA) == null || p.GetValue(objectB) == null) return false;
                    return p.GetValue(objectA).Equals(p.GetValue(objectB));
                } else
                {
                    return AreEqual(p.GetValue(objectA), p.GetValue(objectB));
                }
            });
        }

        /// <summary>
        /// Determines whether value instances of the specified type can be directly compared.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if this value instances of the specified type can be directly compared; otherwise, <c>false</c>.
        /// </returns>
        private static bool CanDirectlyCompare(Type type)
        {
            return typeof(IComparable).IsAssignableFrom(type) || type.IsPrimitive || type.IsValueType;
        }
    }
}
