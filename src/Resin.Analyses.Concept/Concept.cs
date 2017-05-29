using System;
using System.Collections.Generic;

namespace Resin.Analysis.Concept
{
    public struct Concept : IComparable<Concept>, IEquatable<Concept>
    {
        public string Field { get; }

        public int? Id { get; set; }
        public ConceptTermSpan ConceptTermSpan { get; }
        public ISet<string> Categories { get; set; }
        public ISet<string> Labels { get; set; }

        //public ConceptSource 
        public Concept(int id,string field, ConceptTermSpan conceptTermSpan,ISet<string> categories=null,ISet<string> labels=null)
        {
            Id = id;
            if (field == null) throw new ArgumentNullException("field");

            Field = field;
            ConceptTermSpan = conceptTermSpan;
            Categories=categories??new HashSet<string>();
            Labels = labels ?? new HashSet<string>();
        }

        public int CompareTo(Concept other)
        {
            var fieldComparison = string.Compare(Field, other.Field, StringComparison.Ordinal);
            if (fieldComparison != 0) return fieldComparison;
            return ConceptTermSpan.CompareTo(other.ConceptTermSpan);
        }

        public bool Equals(Concept other)
        {
            return string.Equals(Field, other.Field) && ConceptTermSpan.Equals(other.ConceptTermSpan) && Equals(Id,other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Concept && Equals((Concept) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Field != null ? Field.GetHashCode() : 0) * 397) ^ ConceptTermSpan.GetHashCode();
            }
        }
    }
}