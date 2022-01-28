
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using System.IO;
using CaseProcessor.Utility;
using CaseProcessor.Model;

namespace CaseProcessor.TXT
{

    public class ContentAnalyzer
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
        //目前棄用，處理另2個後所剩資料即為BeforeMainContent
        private static string ExtractBeforeMainContent(ref string text)
        {
            StringReader sr = new StringReader(text);
            string s; string output = "";
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
                if (cont)
                {
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
                if (cont)
                {
                    foreach (var term in terms)
                    {
                        if (line != term)
                        {
                            outText += s + "\n";
                        }
                        else
                        {
                            cont = false;
                            break;
                        }
                    }
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
