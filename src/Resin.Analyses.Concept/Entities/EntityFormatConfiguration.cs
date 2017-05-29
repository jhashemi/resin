using System.Collections.Generic;

namespace Resin.Analysis.Concept.Entities
{
    public class EntityFormatConfiguration
    {
        public bool IncludeNumbers { get; set; }
        public ISet<string> IncludeCategories { get; set; }
        public ISet<string> ExcludeCategories { get; set; }
    }
}