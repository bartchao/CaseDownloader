using CaseProcessor.Model;
using CaseProcessor.TXT;
using SourceDownloader;
using SourceDownloader.Lawplus.Search;

using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static string folderPath = "";
        static async Task Main(string[] args)
        {   
            //Configure folder path
            Console.WriteLine("Configure Folder Path:");
            folderPath = Console.ReadLine();
            if (string.IsNullOrEmpty(folderPath) ==false)
            {
                await DownloadData(folderPath);
                await ProcessTxt(folderPath);

            }
            else
            {
                folderPath = "D:\\TEST\\";
                Console.WriteLine("No path is set, process existing data.");
                await ProcessTxt(folderPath);
            }
            
            Console.ReadKey();
        }
        static async Task DownloadData(string folderPath)
        {
            CaseDownloader downloader = new CaseDownloader(folderPath);
            await downloader.Start();
        }
        static async Task ProcessTxt(string folderPath)
        {
            FileInfo[] info = new DirectoryInfo(folderPath).GetFiles();
            foreach (var text in info)
            {
                var textinfo = text.FullName;
                TxtJudgeReader txtReader = new TxtJudgeReader(textinfo);
                SaveJson(txtReader.GetVerdict(),textinfo);
            }
        }
        static void  SaveJson(Verdict verdict,string textinfo)
        {
            //記得先建Json資料夾，不然會丟Exception
            string folderPath = Program.folderPath + "Json\\";
            string newfilename = new FileInfo(textinfo).Name+".json";
            string path = folderPath + newfilename;
            CaseProcessor.JSON.CaseJSON.Save(verdict,path);
        }
    }
}
