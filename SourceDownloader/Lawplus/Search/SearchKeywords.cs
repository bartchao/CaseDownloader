using SourceDownloader.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SourceDownloader.Lawplus.Search
{
    public class SearchKeywordsList :IEnumerable
    {
        List<SearchKeywords> list = new List<SearchKeywords>();
        int records;int allPages;int rowsPerPage;
        public SearchKeywordsList(SearchKeywords keywords)
        {
            records = keywords.GetResult().Records;
            rowsPerPage = keywords.rows;
            allPages = (records / rowsPerPage) + 1;
            Console.WriteLine("All pages:" + allPages);
            Console.WriteLine("All records:" + records);

            ExpandList();
        }
        public SearchKeywordsList()
        {
            SearchKeywords keywords = new SearchKeywords();
            var records = keywords.GetResult().Records;
            rowsPerPage = keywords.rows;
            allPages = (records / rowsPerPage) + 1;
            ExpandList();
        }
        private void ExpandList()
        {
            for(int i = 1; i <= allPages; i++)
            {
                list.Add(new SearchKeywords(i, rowsPerPage));
            }
        }
        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)list).GetEnumerator();
        }
    }
    public class SearchKeywords
    {
        private SearchResult Result;
        public SearchKeywords()
        {
        }

        public SearchKeywords(int _page,int _rows)
        {
            page = _page;
            rows = _rows;
        }
        public string querySentence { get; set; }
        public string keyword { get; set; }
        public string prevKeyword { get; set; }
        public string date { get; set; }
        public string money { get; set; }
        public string sentence { get; set; }
        public string caseNum { get; set; }
        public string caseTypes { get; set; } = "M";//刑事
        public string courts { get; set; } = "CYD";//嘉義法院
        public string levels { get; set; }
        public string jtypes { get; set; } = "J";
        public string tags { get; set; }
        public string issue { get; set; }
        public string main { get; set; }
        public string judge { get; set; }
        public string judgeTypes { get; set; }
        public string lawyer { get; set; }
        public string litigant { get; set; }
        public string prosecutor { get; set; }
        public string clerk { get; set; }
        public int rows { get; set; } = 10;
        public int page { get; set; } = 1;
        public string sortField { get; set; } = "judgeDate";


        public string GetFullUrl()
        {
            string url = "https://www.lawplus.com.tw/rest/search/report?";
            string append = "";
            List<PropertyInfo> info = GetType().GetProperties().ToList();
            info.ForEach((info) =>
            {
                var name = info.Name;
                var result = info.GetValue(this);
                append += name + "=" + result + "&";
            }
            );
            return url + append;
        }
        public SearchResult GetResult()
        {
            if (Result == null)
            {
                Result = Downloader.GetJsonByUrl<SearchResult>(GetFullUrl());
            }
            return Result;
        }
        



    }
}
