using System.Collections.Generic;

namespace Resin.Analysis.Concept.Vocabulary
{
    public class VocabularyQuery
    {
        private VocabularyQuery(Builder builder)
        {
            Input = builder._input;
            Limit = builder._limit;
            IncludeDeprecated = builder._includeDeprecated;
            IncludeSynonyms = builder._includeSynonyms;
            IncludeAcronyms = builder._includeAcronyms;
            IncludeAbbreviations = builder._includeAbbreviations;
            Prefixes = new SortedSet<string>(builder._prefixes);

            Categories = new SortedSet<string>(builder._categories);
        }

        public virtual string Input { get; }

        public virtual int Limit { get; }

        public virtual bool IncludeDeprecated { get; }

        public virtual bool IncludeSynonyms { get; }

        public virtual bool IncludeAcronyms { get; }

        public virtual bool IncludeAbbreviations { get; }

        public virtual ISet<string> Prefixes { get; }

        public virtual ISet<string> Categories { get; }

        public sealed class Builder
        {
            internal readonly ISet<string> _categories = new SortedSet<string>();
            internal readonly string _input;
            internal readonly ISet<string> _prefixes = new SortedSet<string>();
            internal bool _includeAbbreviations;
            internal bool _includeAcronyms;
            internal bool _includeDeprecated;
            internal bool _includeSynonyms;
            internal int _limit;

            /// <summary>
            ///     *
            ///     The input could be an IRI, a CURIE, or a term.
            /// </summary>
            /// <param name="input">  the relevant input for the query. </param>
            public Builder(string input)
            {
                _input = input;
            }

            public static VocabularyQuery Build(string input, IEnumerable<string> prefixes = null,
                IEnumerable<string> categories = null, int limit = 1000, bool includeDeprecated = true,
                bool includeSynonyms = true, bool includeAcronyms = false, bool includeAbbreviations = false)
            {
                return new Builder(input).Prefixes(prefixes ?? new List<string>())
                    .Categories(categories ?? new List<string>())
                    .Limit(limit)
                    .IncludeAcronyms(includeAcronyms)
                    .IncludeAbbreviations(includeAbbreviations)
                    .IncludeSynonyms(includeSynonyms)
                    .IncludeDeprecated(includeDeprecated)
                    .Build();
            }

            /// <summary>
            ///     *
            /// </summary>
            /// <param name="limit">  the maximum number results to return </param>
            /// <returns> the builder </returns>
            public Builder Limit(int limit)
            {
                _limit = limit;
                return this;
            }

            public Builder IncludeDeprecated(bool include)
            {
                _includeDeprecated = include;
                return this;
            }

            public Builder IncludeSynonyms(bool include)
            {
                _includeSynonyms = include;
                return this;
            }

            public Builder IncludeAcronyms(bool include)
            {
                _includeAcronyms = include;
                return this;
            }

            public Builder IncludeAbbreviations(bool include)
            {
                _includeAbbreviations = include;
                return this;
            }

            /// <summary>
            ///     *
            /// </summary>
            /// <param name="prefixes"> a set of required CURIE prefixes </param>
            /// <returns> the builder </returns>
            public Builder Prefixes(IEnumerable<string> prefixes)
            {
                _prefixes.UnionWith(prefixes);

                return this;
            }

            /// <summary>
            ///     *
            /// </summary>
            /// <param name="categories">  a set of required categories </param>
            /// <returns> the builder </returns>
            public Builder Categories(IEnumerable<string> categories)
            {
                _categories.UnionWith(categories);

                return this;
            }

            /// <summary>
            ///     *
            /// </summary>
            /// <returns> the built query </returns>
            public VocabularyQuery Build()
            {
                return new VocabularyQuery(this);
            }
        }
    }
}