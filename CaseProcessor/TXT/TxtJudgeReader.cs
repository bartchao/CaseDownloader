using CaseProcessor.Model;
using CaseProcessor.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CaseProcessor.TXT
{
    public class TxtJudgeReader
    {
        StringReader sr;
        string fileName;
        Verdict verdict = new Verdict();
        string line;

        public TxtJudgeReader(string filepath)
        {
            this.sr = new StringReader(File.ReadAllText(filepath));
            this.fileName = Path.GetFileNameWithoutExtension(filepath);
        }
        public Verdict GetVerdict()
        {
            try
            {
                GetIdentity(fileName);
                JumpToPeople();
            }
            catch (Exception e)
            {
                Console.WriteLine(fileName);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            return verdict;
        }
        private void JumpToPeople()
        {
            int matchCount = 0;
            Regex regex = new Regex(@"\d{2,}年度[\u4E00-\u9FFF]*字第\d*號");
            while ((line = sr.ReadLine()) != null)
            {
                if (regex.IsMatch(line) == true)
                {
                    matchCount++;
                }
                else if (regex.IsMatch(line) == false && matchCount > 0)
                {
                    AnalyzePeople(line);
                    break;
                }
            }
        }
        
        //取得法院名稱 目前未啟用功能
        private void GetCourtName()
        {
            if ((line = sr.ReadLine()) != null)
            {
                //Case type 
                //string[] firstLine = System.Text.RegularExpressions.Regex.Split(line, @"\s{2,}");
                //string[] firstLine = s.Split(" ",StringSplitOptions.RemoveEmptyEntries);             
                verdict.GetIdentity.Type = "刑事";
                string courtName = line.Remove(line.Length - 4);
                //Get Court Code 
                //CourtsUtil courtsUtil = CourtsUtil.GetCourtsUtil();
                //verdict.CourtCode = courtsUtil.GetCourtByName(courtName).Code;

            }
        }
        //取得案件字號
        /*private void GetIdentity()
        {
            string[] term = { "年度", "字", "第", "號" };
            line = StringUtil.RemoveSpace(sr.ReadLine());
            var identity = line.Split(term, StringSplitOptions.RemoveEmptyEntries);
            verdict.GetIdentity.Year = int.Parse(identity[0]);
            verdict.GetIdentity.Word = identity[1];
            verdict.GetIdentity.Number = int.Parse(identity[2]);
        }*/

        //取得案件字號，改用檔名來取
        private void GetIdentity(String fileName)
        {
            string[] term = { ",", "_" };
            var identity = fileName.Split(term, StringSplitOptions.RemoveEmptyEntries);
            verdict.GetIdentity.Year = int.Parse(identity[0]);
            verdict.GetIdentity.Word = identity[1];
            verdict.GetIdentity.Number = int.Parse(identity[2]);
            verdict.GetIdentity.Classification = identity[3];
        }


        //抓出案件人物
        private void AnalyzePeople(string firstLine)
        {
            string rawText = firstLine + "\n"; 
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Length == 0)
                {
                    continue;
                }
                else if (line.StartsWith("上列") == false)
                {
                    rawText += line + "\n";
                }
                else if (line.StartsWith("上列") == true)
                {
                    AnalyzePeopleData(rawText);
                    AnalyzeContent(line);
                    break;
                }
            }
        }
        private void AnalyzePeopleData(string rawText)
        {
            using (StringReader reader = new StringReader(rawText))
            {
                string type = ""; string name = "";
                Persons persons = verdict.persons;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line) == false)
                    {
                        string[] splitLine = line.TrimEnd().Split(" ");
                        int typeLength = splitLine.Length;
                        if (splitLine.Length > 1 && splitLine[splitLine.Length - 1].Length > 1)
                        {
                            //發現含有人名
                            name = splitLine[splitLine.Length - 1];
                            typeLength--;
                        }
                        //加入該行type資料
                        for (int i = 0; i <= typeLength - 1; i++)
                        {
                            if (string.IsNullOrWhiteSpace(splitLine[i]) == false)
                            {
                                type += splitLine[i];
                            }
                        }
                        if (string.IsNullOrWhiteSpace(type) == false && string.IsNullOrWhiteSpace(name) == false)
                        {
                            persons.AddPerson(type, name);
                            type = "";
                            name = "";
                        }
                        else if (string.IsNullOrWhiteSpace(name) == false && string.IsNullOrWhiteSpace(type) == true)
                        {
                            persons.AddPerson(name);
                            name = "";
                        }

                    }

                }
            }
        }
            

        
        private void AnalyzeContent(string firstline)
        {

            string content = firstline + "\n";
            string[] dateDelimeter = { "中華民國", "年", "月", "日" };
            while ((line = sr.ReadLine()) != null)
            {
                if (StringUtil.RemoveSpace(line).StartsWith("中華民國") == false)
                {
                    content += line + "\n";
                }
                else
                {
                    var dateString = StringUtil.RemoveSpace(line).Split(dateDelimeter, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        verdict.JudgeDate = new DateTime(int.Parse(dateString[0]) + 1911, int.Parse(dateString[1]), int.Parse(dateString[2]));
                        break;
                    }
                    catch (FormatException e)
                    {
                        //Not a correct date string 
                        content += line + "\n";
                    }
                }
            }
            //對內文進行分類處理
            var contentList = ContentAnalyzer.SplitContent(content);
            verdict.BeforeMain = contentList.BeforeMainContent;
            verdict.MainContent = contentList.MainContent;
            verdict.FactReason = contentList.FactReason;
        }
        







    }
}
