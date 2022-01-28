using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaseProcessor.Utility
{
    public static class StringUtil
    {
        public static string RemoveSpace(string text)
        {
            return string.Concat(text.Where(c => !Char.IsWhiteSpace(c)));
        }
    }
}
