using SourceDownloader;
using System;
using System.Collections.Generic;
using System.Text;
using JSONVerdict = FileDownloader.Model.JSON.Verdict;
using Verdict = FileDownloader.Model.Verdict;
using System.Reflection;
using System.Linq;
using System.IO;

namespace FileDownloader.Utility
{
    interface IConverter
    {
        void Process(JSONVerdict verdict);
    }
    public class Util
    {
        public struct Content
        {
            public string BeforeMainContent;
            public string MainContent;
            public string FactReason;
        }
        public static Content SplitContent(string text)
        {
            var content = new Content();
            content.FactReason = ExtractFactReason(ref text);
            content.MainContent = ExtractMainContent(ref text);
            content.BeforeMainContent = text;
            return content;
        }

        public static Verdict Converter(JSONVerdict jsonVerdict)
        {
            var verdict = new Verdict();
            verdict.CourtCode = jsonVerdict.Court.Code;
            verdict.DetailUrl = jsonVerdict.Body.ContentUrl;
            verdict.Reason = jsonVerdict.Story.Reason;
            verdict.JudgeName = jsonVerdict.Story.JudgeName;
            var result = SplitContent(jsonVerdict.Body.Content.Content);
            verdict.BeforeMain = result.BeforeMainContent;
            verdict.MainContent = result.MainContent;
            verdict.FactReason = result.FactReason;
            return verdict;
        }
        private static string ExtractBeforeMainContent(ref string text)
        {
            StringReader sr = new StringReader(text);
            string s; bool start = false; string output = "";
            while ((s = sr.ReadLine()) != null)
            {
                if (StringUtil.RemoveSpace(s) == "主文")
                {
                    break;
                }
                else
                {
                    output += s + "\n";

                }
            }
            return output;
        }
        private static string ExtractFactReason(ref string text)
        {
            string[] terms = { "犯罪事實及理由", "理由", "事實及理由", "犯罪事實", "事實" };
            StringReader sr = new StringReader(text);
            string s; string output = ""; bool cont = true; string outText = "";
            while ((s = sr.ReadLine()) != null)
            {
                var line = StringUtil.RemoveSpace(s);
                foreach (var term in terms)
                {
                    if (line == term)
                    {
                        cont = false;
                        break;
                    }
                }
                if (cont)
                {
                    outText += s + "\n";
                }
                else
                {
                    output += s + "\n";
                }
            }
            text = outText;
            return output;
        }
        private static string ExtractMainContent(ref string text)
        {
            string[] terms = { "主文" };
            StringReader sr = new StringReader(text);
            string s; string output = ""; bool cont = true; string outText = "";
            while ((s = sr.ReadLine()) != null)
            {
                var line = StringUtil.RemoveSpace(s);
                foreach (var term in terms)
                {
                    if (line == term)
                    {
                        cont = false;
                        break;
                    }
                }
                if (cont)
                {
                    outText += s + "\n";
                }
                else
                {
                    output += s + "\n";
                }
            }
            text = outText;
            return output;
        }

    }
}
