﻿using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace DutWrapper.Tester
{
    [TestClass]
    public class UtilsTest
    {
        public Formatting JSON_FORMATTING = Formatting.Indented;

        [TestMethod]
        public async Task GetCurrentSchoolWeek()
        {
            Debug.WriteLine($"Processing Utils.GetCurrentSchoolWeek...");
            var resultCurrentWeek = await Utils.GetCurrentSchoolWeek();
            var data = JsonConvert.SerializeObject(resultCurrentWeek, JSON_FORMATTING);
            Debug.WriteLine($"Result: {data}");
            Debug.WriteLine("");
        }
    }
}