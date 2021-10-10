using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Film.Infra
{
    [Serializable]
    public abstract class EntityBase : IValidatableObject
    {
        [Key]
        public virtual long Id { get; set; }

        public EntityBase()
        {
            Id = 0;
        }

        public override bool Equals(object obj)
        {
            var other = obj as EntityBase;
            if (other == null) return false;
            if (Id == 0 && other.Id == 0)
                return this == other;
            else
                return Id == other.Id;
        }

        public override int GetHashCode()
        {
            if (Id == 0) return base.GetHashCode();
            string stringRepresentation =
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName
                + "#" + Id.ToString();
            return stringRepresentation.GetHashCode();
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return Enumerable.Empty<ValidationResult>();
        }
    }
}