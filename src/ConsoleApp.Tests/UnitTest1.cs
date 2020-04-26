using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace ConsoleApp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var processInfo = new ProcessStartInfo();
            processInfo.CreateNoWindow = true;
            processInfo.FileName = "ConsoleApp.exe";

            var process = Process.Start(processInfo);
        }
    }
}
