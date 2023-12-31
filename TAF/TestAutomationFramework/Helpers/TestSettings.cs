﻿using TAF.Core.Utilities;
using TAF.Core.Enums;
using NUnit.Framework;
using System.Reflection;

namespace TAF.Helper
{
    public static partial class TestSettings
    {
        public static TestContext TestContext { get; set; }

        public static BrowserType Browser => EnumUtilities.ParseEnum<BrowserType>(TestContext.Parameters.Get("Browser").ToString());

        public static string LogsPath => Path.Combine(TestContext.TestDirectory, @TestContext.Parameters.Get("LogsPath").ToString());

        public static TimeSpan WebDriverTimeOut => TimeSpan.FromSeconds(int.Parse(TestContext.Parameters.Get("WebDriverTimeOut").ToString()));

        public static string DefaultTimeOut => TestContext.Parameters.Get("WaitElementTimeOut").ToString();

        public static string ApplicationUrl => TestContext.Parameters.Get("ApplicationUrl").ToString();

        public static string DataDirPath => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, @"..\..\..\..\TestData\TestDataFiles\");
    }
}