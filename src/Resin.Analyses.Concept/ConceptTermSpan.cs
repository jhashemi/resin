using System;
using Resin.IO;

namespace Resin.Analysis.Concept
{
    public struct ConceptTermSpan : IComparable<ConceptTermSpan>, IEquatable<ConceptTermSpan>
    {
        public Term FirstTerm { get; set; }
        public Term LastTerm { get; set; }
        public int CompareTo(ConceptTermSpan other)
        {
            if (Object.Equals(FirstTerm, other.FirstTerm))
            {
                
                return LastTerm.CompareTo(other.LastTerm);
            }
            return FirstTerm.CompareTo(LastTerm);
        }

        public bool Equals(ConceptTermSpan other)
        {
            return Object.Equals(FirstTerm, other.FirstTerm) && Object.Equals(LastTerm, other.LastTerm);
            
        }
    }
}