using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Resin.Analysis.Concept.Vocabulary;
using Resin.Querying;

namespace Resin.Analysis.Concept.Entities
{
    public class EntityRecognizer:IEntityRecognizer
    {

        private readonly IVocabulary _vocabulary;
        

        internal EntityRecognizer(IVocabulary vocabulary)
        {
            this._vocabulary = vocabulary;
            
        }


        protected virtual bool ShouldAnnotate(Concept concept, EntityFormatConfiguration config)
        {
            var conceptCategories = new HashSet<string>(concept.Categories);

            //if (!disjoint(config.ExcludeCategories, conceptCategories))
            if(config.ExcludeCategories.IsSupersetOf(conceptCategories))
            {
                
                return false;
            }
            if (!config.IncludeNumbers && concept.Labels.Any(x=>Regex.IsMatch(x, "(\\d|\\-|_)+")))
            {
                return false;
            }

            if (config.IncludeCategories.Any() && config.IncludeCategories.IsSupersetOf(conceptCategories))
            {
                
                return false;
            }

            return true;
        }

        public virtual ICollection<Entity> GetEntities(string token, EntityFormatConfiguration config)
        {
            var query = Resin.Analysis.Concept.Vocabulary.VocabularyQuery.Builder.Build(token);
            IList<Concept> terms = _vocabulary.GetConceptsFromTerm(query);

            HashSet<Entity> entities = new HashSet<Entity>();
            foreach (Concept term in terms.Where(term=>ShouldAnnotate(term,config)))
            {
                
                //    var id = term.Id.GetValueOrDefault();
                    entities.Add(new Entity(term.Labels, term.Id, term.Categories));
                
            }

            return entities;
        }

    }
}
