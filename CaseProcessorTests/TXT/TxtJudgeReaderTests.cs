using Microsoft.VisualStudio.TestTools.UnitTesting;
using CaseProcessor.TXT;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace CaseProcessor.TXT.Tests
{
    [TestClass()]
    public class TxtJudgeReaderTests
    {
        [TestMethod()]
        public void TxtJudgeReaderTest()
        {
            string filePath = "D:\\TEST\\109,金訴,24_加重詐欺等.txt";
            //string text = File.ReadAllText(filePath);
            TxtJudgeReader reader = new TxtJudgeReader(filePath);
            
        }
    }
}