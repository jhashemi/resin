﻿using System;
using Resin.Querying;

namespace Resin.Analysis
{
    public class Tfidf : IScoringScheme
    {
        private readonly double _idf;

        public double Idf
        {
            get { return _idf; }
        }

        /// <summary>
        /// Create scoring scheme
        /// </summary>
        public Tfidf()
        {
        }

        /// <summary>
        /// Create scorer. 
        /// On tf-idf: 
        /// https://lucene.apache.org/core/6_4_1/core/org/apache/lucene/search/similarities/TFIDFSimilarity.html 
        /// https://en.wikipedia.org/wiki/Tf%E2%80%93idf
        /// </summary>
        /// <param name="docsInCorpus"></param>
        /// <param name="docsWithTerm"></param>
        public Tfidf(int docsInCorpus, int docsWithTerm)
        {
            //_idf = Math.Log10(docsInCorpus / (double)docsWithTerm);
            _idf = Math.Log10(docsInCorpus - docsWithTerm / (double)docsWithTerm);
        }

        public void Score(DocumentScore doc)
        {
            var tf = 1 + Math.Log10(Math.Pow(doc.TermCount, 1/2));
            doc.Score = tf * _idf;
        }

        public IScoringScheme CreateScorer(int docsInCorpus, int docsWithTerm)
        {
            return new Tfidf(docsInCorpus, docsWithTerm);
        }
    }
}