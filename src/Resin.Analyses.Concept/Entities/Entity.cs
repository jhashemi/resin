using System.Collections.Generic;

namespace Resin.Analysis.Concept.Entities
{
    public struct Entity
    {
        public ISet<string> Labels { get; private set; }
        private object p;
        public ISet<string> Categories { get; private set; }

        public Entity(IEnumerable<string> labels, object p, IEnumerable<string> categories)
        {
            this.Labels = new HashSet<string>(labels);
            this.p = p;
            this.Categories = new HashSet<string>(categories);
        }
    }
}