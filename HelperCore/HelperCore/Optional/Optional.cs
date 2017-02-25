using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperCore.Optional
{
    /// <summary>
    /// This class was made specfically to fight against the idea of nulls that exists in the langauge.
    /// This is because nulls are a horrible way of communicating a value is missing, and the location of where the null was is not really explicit.
    /// Therefore, this class exists to better communicate and mitigate the null problem.
    /// This class forces the person to check whether a value is null or not before proceeding.
    /// 
    /// The object inside this class should not be mutable as it comes from an external source or somewhere else.
    /// This would imply that the functions here will not mutate the value that was passed in here initially.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Optional<T>
    {
        //initalization methods
        /// <summary>
        /// Create an optional with the type and the given object
        /// </summary>
        /// <param name="obj">the object</param>
        /// <returns>An optional object of the type the object is, along with the object wrapped in it</returns>
        public static Optional<T> From(T obj)
        {
            return new Optional<T>(obj);
        }

        /// <summary>
        /// Create an optional with the type but with a null object
        /// </summary>
        /// <returns>An object of the type but with null for the value</returns>
        public static Optional<T> FromNull()
        {
            return new Optional<T>(null);
        }
        
        /// <summary>
        /// Get the value that is inside the optional. If the value is null, a null reference exception is thrown
        /// </summary>
        public T Value {
            get
            {
                if (_value == null)
                {
                    throw new NullReferenceException("Attempted to get a value from an Optional<> that ended up being a null");
                }
                else
                {
                    return (T)_value;
                }
            }
        }

        private object _value;

        private Optional(object obj)
        {
            _value = obj;
        }

        /// <summary>
        /// Check to see if the value is present or not
        /// </summary>
        /// <returns>Whether the value is present or not</returns>
        public bool IsPresent()
        {
            return _value != null;
        }

        /// <summary>
        /// Returns either the value if present or the other if it's not
        /// </summary>
        /// <param name="other">the value to be returned if there are no values present, it can be null</param>
        /// <returns>the value if present, otherwise the other</returns>
        public T OrElse(T other)
        {
            return _value != null ? Value : other;
        }

        /// <summary>
        /// Returns either the value if present or default of the type if it's not
        /// </summary>
        /// <returns>the value if present, otherwise default(T)</returns>
        public T OrDefault()
        {
            return _value != null ? Value : default(T);
        }
    }
}
