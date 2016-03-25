﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Resin;

namespace Tests
{
    [TestFixture]
    public class IndexReaderTests
    {
        [Test]
        public void Can_read()
        {
            const string dir = "c:\\temp\\resin_tests\\Can_find";
            if (Directory.Exists(dir)) Directory.Delete(dir, true);
            var analyzer = new Analyzer();
            var parser = new QueryParser(analyzer);

            using (var w = new IndexWriter(dir, analyzer))
            {
                w.Write(new Document
                {
                    Id = 0,
                    Fields = new Dictionary<string, List<string>>
                    {
                        {"title", new[]{"a"}.ToList()}
                    }
                });
                w.Write(new Document
                {
                    Id = 1,
                    Fields = new Dictionary<string, List<string>>
                    {
                        {"title", new[]{"a b"}.ToList()}
                    }
                });
                w.Write(new Document
                {
                    Id = 2,
                    Fields = new Dictionary<string, List<string>>
                    {
                        {"title", new[]{"a b c"}.ToList()}
                    }
                });
            }
            using (var reader = new IndexReader(new Scanner(dir)))
            {
                var docs = reader.GetScoredResult(parser.Parse("title:a")).ToList();

                Assert.AreEqual(3, docs.Count);

                docs = reader.GetScoredResult(parser.Parse("title:b")).ToList();

                Assert.AreEqual(2, docs.Count);

                docs = reader.GetScoredResult(parser.Parse("title:c")).ToList();

                Assert.AreEqual(1, docs.Count);

                docs = reader.GetScoredResult(parser.Parse("title:a +title:b")).ToList();

                Assert.AreEqual(2, docs.Count);

                docs = reader.GetScoredResult(parser.Parse("title:a +title:b +title:c")).ToList();

                Assert.AreEqual(1, docs.Count);
            }
        }
    }

    [TestFixture]
    public class TermTests
    {
        [Test]
        public void Can_calculate_distance_from_similarity()
        {
            Assert.AreEqual(6, new Term { Token = "jrambo", Similarity = 0.009f }.Edits);
            Assert.AreEqual(5, new Term { Token = "jrambo", Similarity = 0.2f }.Edits);
            Assert.AreEqual(4, new Term { Token = "jrambo", Similarity = 0.3f }.Edits);
            Assert.AreEqual(3, new Term { Token = "jrambo", Similarity = 0.5f }.Edits);
            Assert.AreEqual(2, new Term { Token = "jrambo", Similarity = 0.7f }.Edits);
            Assert.AreEqual(1, new Term { Token = "jrambo", Similarity = 0.8f }.Edits);
            Assert.AreEqual(1, new Term { Token = "jrambo", Similarity = 0.9f }.Edits);
            Assert.AreEqual(0, new Term { Token = "jrambo", Similarity = 0.999999f }.Edits);


            Assert.AreEqual(1, new Term { Token = "abcde", Similarity = 0.8f }.Edits);
            Assert.AreEqual(1, new Term { Token = "abcdef", Similarity = 0.8f }.Edits);
            Assert.AreEqual(1, new Term { Token = "abcdefg", Similarity = 0.8f }.Edits);
            Assert.AreEqual(2, new Term { Token = "abcdefgh", Similarity = 0.8f }.Edits);

            Assert.AreEqual(2, new Term { Token = "abcde", Similarity = 0.7f }.Edits);
            Assert.AreEqual(2, new Term { Token = "abcdef", Similarity = 0.7f }.Edits);
            Assert.AreEqual(2, new Term { Token = "abcdefg", Similarity = 0.7f }.Edits);
            Assert.AreEqual(2, new Term { Token = "abcdefgh", Similarity = 0.7f }.Edits);
        }

    }
}