using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection.PortableExecutable;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Newtonsoft.Json;
using NUnit.Framework;
using Snapshooter.Tests.Data;

namespace Snapshooter.NUnit.Tests
{
    public partial class SnapshotTests
    {
        #region Match Snapshot - Simple Snapshot Tests

        [Test]
        public void Match_TestMatchSingleSnapshot_GoodCase()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().WithFirstname("ddddddd").Build();

            TestContext.WriteLine("c:/temp/ServiceBusExplorer-5.0.2/ServiceBusExplorer.exe");

            // act & assert
             Snapshot.Match(testPerson);
        }

        [Test]
        public void Match_TestMatchSingleSnapshot_OneFieldNotEqual()
        {
            TestContext aaf = TestContext.CurrentContext;
            var domain = AppDomain.CurrentDomain;
            var process = Process.GetCurrentProcess(); // Or whatever method you are using
            string fullPath = process.MainModule.FileName;
            Func<bool> da = Debugger.IsLogging;
            bool aa = da();
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().WithAge(5).Build();
            TestContext.AddTestAttachment("c:/temp/test.json");
            TestContext.AddTestAttachment("c:/temp/tmpoldadconnect.json");
            //TestContext.CurrentContext.
            TestContext.WriteLine("{red}This is red The server is running now! You can reach it on file:///c:/temp/test.json file:///c:/temp/test.cmd file:///c:/temp/folder%20with%20space/test.cmd http://google.com ");
            TestContext.WriteLine("{red}This is red The server is running now! You can reach it on file:///c:/temp/test.bat");
            TestContext.WriteLine("{red}This is red The server is running now! You can reach it on file:///c:/temp/test.docx");



            TestContext.WriteLine("file:///c:/Program%20Files/Microsoft%20Visual%20Studio/2022/Enterprise/Common7/IDE/devenv.exe /diff");
            TestContext.WriteLine("file:///c:/Program+Files/Microsoft+Visual+Studio/2022/Enterprise/Common7/IDE/test.json");
            TestContext.WriteLine("file:///C:/Program+Files/Microsoft+Visual+Studio/2022/Enterprise/Common7/IDE/devenv.exe+/diff+%5C%22C%3A%2Ftemp%2Ftest.json%5C%22+%5C%22C%3A%2Ftemp%2Fddd.txt%5C%22%22");


//throw new Exception("{red}This is red The server is running now! You can reach it on file:///c:/temp/test.json file:///c:/temp/test.cmd file:///c:/temp/folder%20with%20space/test.cmd http://google.com ");
            //Assert.Pass(JsonConvert.SerializeObject(testPerson, Formatting.Indented));

            //var dfd = new Exception(JsonConvert.SerializeObject(testPerson, Formatting.Indented));
            //dfd.Source = "c:/temp/test.json";
            //dfd.HelpLink = "c:/temp/test.json";
            //throw dfd;
            //Console.WriteLine(JsonConvert.SerializeObject(testPerson, Formatting.Indented));
            //// act & assert
            //Snapshot.Match(testPerson);
        }

        [Test]
        public void Match_TestMatchSingleSnapshot_FieldNotExistInSnapshot()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Assert.Throws<AssertionException>(() => Snapshot.Match(testPerson));
        }

        [Test]
        public void Match_TestMatchNewSingleSnapshot_ExpectedSnapshotHasBeenCreated()
        {
            // arrange
            var snapshotFullNameResolver = new SnapshotFullNameResolver(
                new NUnitSnapshotFullNameReader());

            SnapshotFullName snapshotFullName =
                snapshotFullNameResolver.ResolveSnapshotFullName();

            string snapshotFileName = Path.Combine(
                snapshotFullName.FolderPath,
                FileNames.SnapshotFolderName,
                snapshotFullName.Filename);

            File.Delete(snapshotFileName);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act
            Snapshot.Match(testPerson);

            // assert
            Assert.True(File.Exists(snapshotFileName));
        }

        [TestCase(36, 189.45)]
        [TestCase(42, 173.16)]
        [TestCase(19, 193.02)]
        public void Match_TestCaseMatchSingleSnapshot_GoodCase(int age, decimal size)
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize(size).Build();

            // act & assert
            Snapshot.Match(testPerson);
        }

        [TestCase(34, 175)]
        [TestCase(36, 177)]
        [TestCase(37, 178)]
        public void Match_TestCaseMatchSingleSnapshot_OneFieldNotEqual(int age, decimal size)
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize(size).Build();

            testPerson.Address.Country.CountryCode = CountryCode.DE;

            // act & assert
            Assert.Throws<AssertionException>(() => Snapshot.Match(testPerson));
        }

        [TestCase(22, 160)]
        [TestCase(23, 164)]
        public void Match_TestCaseMatchSingleSnapshot_FieldNotExistInSnapshot(int age, decimal size)
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize(size).Build();

            // act & assert
            Assert.Throws<AssertionException>(() => Snapshot.Match(testPerson));
        }

        [TestCase(19, 162.3)]
        [TestCase(17, 112.3)]
        public void Match_TestCaseMatchNewSingleSnapshot_ExpectedSnapshotHasBeenCreated(int age, decimal size)
        {
            // arrange
            var snapshotFullNameResolver = new SnapshotFullNameResolver(
                new NUnitSnapshotFullNameReader());

            SnapshotFullName snapshotFullName =
                snapshotFullNameResolver.ResolveSnapshotFullName();

            string snapshotFileName = Path.Combine(
                snapshotFullName.FolderPath,
                FileNames.SnapshotFolderName,
                snapshotFullName.Filename);

            File.Delete(snapshotFileName);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize(size).Build();

            // act
            Snapshot.Match(testPerson);

            // assert
            Assert.True(File.Exists(snapshotFileName));
        }

        #endregion
    }
}
