using CaseProcessor.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace CaseProcessor.JSON
{
    public class CaseJSON
    {

        public static void Save(Verdict verdict,string path)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(verdict,options);
            File.WriteAllText(path, json);
        }
    }
}
