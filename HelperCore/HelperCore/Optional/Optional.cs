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
    public class Optional
    {

        /// <summary>
        /// Create an optional with the type and the given object
        /// </summary>
        /// <param name="obj">the object</param>
        /// <returns>An optional object of the type the object is, along with the object wrapped in it</returns>
        public static Optional<T> From<T>(T obj)
        {
            return Optional<T>.From(obj);
        }
    }
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
                    throw new NullReferenceException($"Attempted to get a value from an Optional<{typeof(T).Name}> that ended up being a null");
                }
                else
                {
                    return (T)_value;
                }
            }
        }

        /// <summary>
        /// Gets whether the value is present or not
        /// </summary>
        public bool Present
        {
            get
            {
                return _value != null;
            }
        }

        private object _value;

        private Optional(object obj)
        {
            _value = obj;
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

        /// <summary>
        /// Compare the equality of the given object and the object that is wrapped inside the optional
        /// </summary>
        /// <param name="obj">object to compare to</param>
        /// <returns>whether the object and the optional wrapped object are equal</returns>
        public override bool Equals(object obj)
        {
            return !Present || obj == null ? false : _value.Equals(obj);
        }

        /// <summary>
        /// Get the hashcode for the wrapped object, 0 if the optional was null
        /// </summary>
        /// <returns>the hashcode of the wrapped object</returns>
        public override int GetHashCode()
        {
            return Present ? _value.GetHashCode() : 0;
        }
    }
}
