using System;
using Resin.IO;

namespace Resin.Analysis.Concept
{
    public interface ILongIdSortable : IComparable<ILongIdSortable>
    {
        long Id { get; }
    }
    public interface IIntIdSortable : IComparable<IIntIdSortable>
    {
        int Id { get; }
    }
    public interface INameSortable : IComparable<INameSortable>
    {
        string Name { get; }
    }

   public abstract class NameSortable : INameSortable
    {
     

        public int CompareTo(INameSortable other)
        {
            return StringComparer.Ordinal.Compare(Name, other.Name);
        }


        public  string Name { get; protected set; }
    }

    public class ConceptSource: NameSortable
    {
  
        public int Id { get; set; }
        public bool SupportsStemming { get; set; }
        public bool SupportsLemma { get; set; }
        public bool IsPositionDependent { get; set; }
    }
  
}