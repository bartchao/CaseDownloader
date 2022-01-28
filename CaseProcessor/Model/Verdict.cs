using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CaseProcessor.Model
{
    
    public class Verdict
    {
        public Identity GetIdentity { get; set; } = new Identity();
        public string CourtCode { get; set; } //法院代號
        public string[] Judge { get; set; } //法官
        public string[] Defendant { get; set; } //被告
        public string[] Agent { get; set; } //代理人
        public string[] Plaintiff { get; set; } //上訴人
        public string[] Lawyer { get; set; } //律師
        public string[] Assistant { get; set; } //輔佐人
        public string[] Clerk { get; set; } //書記官

        public Persons persons { get; set; } = new Persons();
        
        public string BeforeMain { get; set; }
        public string MainContent { get; set;} //主文
        public string FactReason { get; set;}
        public string DetailUrl { get; set; } //資料來源網址
        public string Reason { get; set; }
        public DateTime JudgeDate { get; set; } = new DateTime(); //判決日期
        public class Identity
        {
            public string Type { get; set; } //案件類型 ex 刑法、民法...
            public int Year { get; set; } //裁判年分
            public string Word { get; set; } //裁判貫字
            public int Number { get; set; } //裁判編號
            public string Classification { get; set; } //案件分類 ex 加重詐欺、過失傷害等等
        }
        public string GetJSON()
        {
            return JsonSerializer.Serialize(this);
        }

        public void PrintPersons()
        {
            
        }
        
        
    }
}
