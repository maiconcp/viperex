using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Viper.Common
{
    public abstract class Enumeration<T> : IComparable where T : Enumeration<T>
    {
        public string Description { get; private set; }

        public int Id { get; private set; }

        protected Enumeration()
        { }

        protected Enumeration(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public override string ToString() => Description;

        public static IEnumerable<T> GetAll()
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration<T>;

            if (otherValue == null)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static int AbsoluteDifference(Enumeration<T> firstValue, Enumeration<T> secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);
            return absoluteDifference;
        }

        public static T FromValue(int value)
        {
            var matchingItem = Parse<int>(value, "value", item => item.Id == value);
            return matchingItem;
        }

        public static T FromDescription(string description) 
        {
            var matchingItem = Parse<string>(description, "description", item => item.Description == description);
            return matchingItem;
        }

        private static T Parse<K>(K value, string description, Func<T, bool> predicate) 
        {
            var matchingItem = GetAll().FirstOrDefault(predicate);

            if (matchingItem == null)
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

            return matchingItem;
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration<T>)other).Id);
    }
}
