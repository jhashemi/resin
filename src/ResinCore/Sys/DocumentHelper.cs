﻿using System.Collections.Generic;
using Resin.IO;

namespace Resin.Sys
{
    public static class DocumentHelper
    {
        public static IEnumerable<Document> ToDocuments(this IEnumerable<dynamic> dynamicDocuments)
        {
            foreach (var dyn in dynamicDocuments)
            {
                var fields = new List<Field>();

                foreach (var field in Util.ToDictionary(dyn))
                {
                    fields.Add(new Field(field.Key, field.Value));
                }

                yield return new Document(fields);
            }
        }
    }
}