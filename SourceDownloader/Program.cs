using SourceDownloader.Lawplus.Search;
using SourceDownloader.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SourceDownloader
{
    public class CaseDownloader
    {
        string _folderPath;
        public CaseDownloader(string folderPath = "")
        {
            _folderPath = folderPath;
            if (Directory.Exists(folderPath) == false)
            {
                try
                {
                    Directory.CreateDirectory(folderPath);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        public async Task Start()
        {
            var keywords = new SearchKeywords()
            {
                date = "2021%2F01%2F01~2021%2F12%2F31"
            };
            SearchKeywordsList list = new SearchKeywordsList(keywords);
            Queue<Task> tasks = new Queue<Task>();
            foreach(SearchKeywords item in list)
            {
                await Task.Delay(100);
                tasks.Enqueue(Process(item.GetResult()));
            }
            await Task.WhenAll(tasks);
            Console.WriteLine("Finished all");
        }
        private async Task Process(SearchResult result)
        {
            var rows = result.SearchResultRows;
            List<Task> tasks = new List<Task>();
            foreach (var row in rows)
            {
                tasks.Add(Downloader.DownloadTxtAsync(row.Identifier, _folderPath + row.CaseNum + "_" + row.Issue + ".txt"));
            }
            await Task.WhenAll(tasks);
            Console.WriteLine(DateTime.Now+" - Task:" + result.Page + " Finished");
        }
    }
}
