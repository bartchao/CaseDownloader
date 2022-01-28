using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace SourceDownloader.Sunshine.Search
{
    public class SearchKeywords
    {
        private string base_url = "https://api.jrf.org.tw/search/stories?";
        public int? page { get; set; } = 1;
        private string _story_type;
        public string story_type
        {
            get
            {
                return _story_type;
            }
            set
            {
                _story_type = HttpUtility.UrlEncode(value, System.Text.Encoding.UTF8);
            }
        }
        public int? year { get; set; }
        public string? word { get; set; }
        public int? number { get; set; }
        public string? adjudged_on_gteq { get; set; }
        public string? adjudged_on_lteq { get; set; } = string.Format("{0}-{1}-{2}", DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
        public string MakeUrl()
        {
            check();
            string append = "";
            Type t = this.GetType();
            List<PropertyInfo> props = t.GetProperties().ToList();
            props.Remove(t.GetProperty("base_url"));
            props.Remove(t.GetProperty("page"));
            append += string.Format("page={0}", page);
            foreach(var prop in props)
            {
                string propName = prop.Name;
                object propValue = prop.GetValue(this);
                if (propValue != null)
                {
                    append += string.Format("&q%5B{0}%5D={1}", propName, propValue);
                }
            }
            return base_url + append;
        }
        private void check()
        {
            if (string.IsNullOrEmpty(story_type))
            {
                throw new Exception("案件分類須設定");
            }
        }
    }
}
