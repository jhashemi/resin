using System.Collections.Generic;

namespace Resin.Analysis.Concept.Entities
{
    public interface IEntityRecognizer
    {
        ICollection<Entity> GetEntities(string token, EntityFormatConfiguration config);
    }
}