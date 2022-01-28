using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CaseProcessor.Utility
{
    public class CourtsList
    {
        [JsonPropertyName("courts")]
        public List<Court> Court { get; set; } = new List<Court>();
    }
    public class Court
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("simple_name")]
        public string SimpleName { get; set; }
        [JsonPropertyName("code")]
        public string Code
        {
            get; set;
        }
    }
    public class CourtsUtil
    {
        private static CourtsUtil courts = new CourtsUtil();
        private List<Court> courtList = null;
        private CourtsUtil()
        {
            LoadData("courts.json");
        }
        public static CourtsUtil GetCourtsUtil()
        {
            return courts;
        }
        public Court GetCourtByName(string name)
        {
            return courtList.Find(court => court.Name == name);
        }
        public Court GetCourtByCode(string code)
        {
            return courtList.Find(court => court.Code == code);
        }
        //讀取courts.json以處理法院代碼
        private void LoadData(string path)
        {
            try
            {
                string json = File.ReadAllText(path);
                courtList = JsonSerializer.Deserialize<CourtsList>(json).Court;
            }catch(FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
