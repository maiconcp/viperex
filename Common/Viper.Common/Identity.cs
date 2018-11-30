using System;
using System.Collections.Generic;
using System.Text;

namespace Viper.Common
{

    public class Identity : ValueObject<Identity>
    {
        public Guid Id { get; private set; }

        public Identity(Guid id)
        {
            Id = id;
        }

        public Identity(string id)
        {
            Id = new Guid(id);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }

        public override string ToString()
        {
            return Id.ToString();
        }

        public static Identity CreateNew()
        {
            return new Identity(Guid.NewGuid());
        }

    }
}
