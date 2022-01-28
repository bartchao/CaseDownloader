using FileDownloader.JSON;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
namespace FileDownloader.Utility
{
    class CourtsUtil
    {
        private static CourtsUtil courts = new CourtsUtil();
        private List<Court> courtList = null;
        private CourtsUtil()
        {
            courtList = Model.JSON.Downloader.DownloadByUrl<CourtsList>("https://api.jrf.org.tw/courts").Court;
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
    }
}
