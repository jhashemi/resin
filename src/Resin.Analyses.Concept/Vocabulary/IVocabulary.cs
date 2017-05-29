using System.Collections.Generic;
using Resin.Querying;

namespace Resin.Analysis.Concept.Vocabulary
{
    /// <summary>
    ///     *
    ///     A vocabulary allows interaction with a backing store of terms, ids, categories, and ontologies.
    ///     <para>
    ///         It is designed to be used for concept retrieval from IDs or terms and for auto-completion.
    ///     </para>
    /// </summary>
    /// <param name="N"> The <seealso cref="NodeProperties" /> to return </param>
    public interface IVocabulary
    {
        /// <summary>
        ///     *
        /// </summary>
        /// <returns> a set of categories in the vocabulary </returns>
        ISet<string> AllCategories { get; }

        /// <summary>
        ///     *
        /// </summary>
        /// <returns> a collection of all known CURIE prefixes </returns>
        ISet<string> AllCuriePrefixes { get; }

        /// <summary>
        ///     *
        ///     Get concepts that match either a complete IRI or a CURIE.
        ///     <para>
        ///         CURIE prefixes may be specified at runtime.
        ///         Given the following mapping: <i>http://example.org/CUR_ -> CUR</i> a concept with URI http://example.org/CUR_1
        ///         would be retrievable as CUR:1.
        ///     </para>
        /// </summary>
        /// <param name="query">  a <seealso cref="Query" /> with the IRI or CURIE as input </param>
        /// <returns> an optional concept </returns>
        Concept? GetConceptFromId(VocabularyQuery query);

        /// <summary>
        ///     *
        ///     Gets concepts from a prefix string - useful for auto-complete
        /// </summary>
        /// <param name="query">  a <seealso cref="Query" /> with the prefix as input </param>
        /// <returns> a list of matching concepts </returns>
        IList<Concept> GetConceptsFromPrefix(VocabularyQuery query);

        /// <summary>
        ///     *
        ///     Search concepts as free text.
        ///     <para>
        ///         The label of the resulting concept may not be prefixed with the search term
        ///         (ie: "foo bar" could be returned by a search for "bar").
        ///     </para>
        /// </summary>
        /// <param name="query">  a <seealso cref="Query" /> with the term as input </param>
        /// <returns> a list of matching concepts </returns>
        IList<Concept> SearchConcepts(VocabularyQuery query);

        /// <summary>
        ///     *
        ///     Attempts to match the label of a concept as closely as possible ("exact-ish" match).
        ///     <para>
        ///         The extent of "exact-ish" depends on the implementing class. It may include:
        ///     </para>
        ///     <para>
        ///         <ul>
        ///             <li>stemming</li>
        ///             <li>lowercasing</li>
        ///             <li>lemmatization</li>
        ///         </ul>
        ///         A best attempt to match the complete label should be made
        ///         (ie: "foo bar" would not be returned by a search for "bar").
        ///     </para>
        /// </summary>
        /// <param name="query">  a <seealso cref="Query" /> with the term as input </param>
        /// <returns> a list of matching concepts </returns>
        IList<Concept> GetConceptsFromTerm(VocabularyQuery query);

        /// <summary>
        ///     *
        ///     Provides "did you mean" functionality based on the labels of concepts in the vocabulary.
        /// </summary>
        /// <param name="query">  a query string </param>
        /// <returns> a list of suggestions </returns>
        IList<string> GetSuggestions(string query);
    }
}