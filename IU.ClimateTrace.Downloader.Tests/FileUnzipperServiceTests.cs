﻿using IU.ClimateTrace.Downloader.Services;
using System.Reflection;

namespace IU.ClimateTrace.Downloader.Tests
{
    [TestClass]
    public class FileUnzipperServiceTests
    {
        public FileUnzipperServiceTests() 
        {

        }

        private void CreateSampleFile(string directoryName, string fileName)
        {
            Directory.CreateDirectory(directoryName);
            FileInfo fileInfo = new FileInfo(fileName);
            using (File.Create(Path.Combine(directoryName, fileName)))
            {

            }
        }
        private void DeleteSampleFile(string directoryName, string fileName) 
        {
            if (File.Exists(Path.Combine(directoryName, fileName)))
            { 
                File.Delete(Path.Combine(directoryName, fileName));
            }
        }



        /// <summary>
        /// Tests that this testClass's DeleteSampleFile method works
        /// </summary>
        [TestMethod]
        public void DeleteSampleFile_DeletesAFile()
        {
            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? 
                throw new NullReferenceException($"Could not get Assembly.GetExecutingAssembly().Location");

            string localDirectory =
                Path.Combine(assemblyPath, "TestFiles");
            string fileName = $"TestFile-{Guid.NewGuid()}.zip";
            CreateSampleFile(Path.Combine(localDirectory), fileName);

            bool fileExisted = File.Exists(Path.Combine(localDirectory, fileName));

            DeleteSampleFile(Path.Combine(localDirectory), fileName);
            bool fileExistedAfterDeletion = File.Exists(Path.Combine(localDirectory, fileName));

            Assert.IsTrue(fileExisted);
            Assert.IsFalse(fileExistedAfterDeletion);
        }

        /// <summary>
        /// Tests that this testClass's CreateSampleFile method works
        /// </summary>
        [TestMethod]
        public void CreateSampleFile_CreatesAFile()
        {
            string assemblyPath = GetAssemblyPath();

            string localDirectory =
                Path.Combine(assemblyPath, "TestFiles");
            string fileName = $"TestFile-{Guid.NewGuid()}.zip";
            CreateSampleFile(Path.Combine(localDirectory), fileName);

            bool fileExists = File.Exists(Path.Combine(localDirectory, fileName));

            Assert.IsTrue(fileExists);
        }

        private static string GetAssemblyPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                throw new NullReferenceException($"Could not get Assembly.GetExecutingAssembly().Location");
        }


        /// <summary>
        /// Tests that the unzip file method actaully unzips files
        /// </summary>
        [TestMethod]
        public void UnzipFile_SuccessfullyUnzipsAFile()
        {
            Guid testRunFileIdId = Guid.NewGuid();
            string assemblyPath = GetAssemblyPath();

            // create the directory to run tests in
            Directory.CreateDirectory(Path.Combine(assemblyPath, "TestFiles"));
            File.Copy(
                Path.Combine(assemblyPath, "TestAssets", "test.zip"),
                Path.Combine(assemblyPath, "TestFiles", $"test-{testRunFileIdId}.zip"));

            bool zipFileExistedAfterCopy = File.Exists(Path.Combine(assemblyPath, "TestFiles", $"test-{testRunFileIdId}.zip"));
            
            
            IFileUnzipperService unzipperService = new FileUnzipperService();

            unzipperService.UnzipFile(
                Path.Combine(assemblyPath, "TestFiles", $"test-{testRunFileIdId}.zip"),
                Path.Combine(assemblyPath, "TestFiles", $"unzipped-{testRunFileIdId}")
                );

            bool unzippedFileExists = File.Exists(Path.Combine(assemblyPath, "TestFiles", $"unzipped-{testRunFileIdId}", $"test.txt"));


            Assert.IsTrue(zipFileExistedAfterCopy);
            Assert.IsTrue(unzippedFileExists);

            // clean up the directory after running these tests
            Directory.Delete(Path.Combine(assemblyPath, "TestFiles"), recursive: true);
        }

        [TestMethod]
        public void UnzipFile_DeletesFileBeforeUnzipping()
        {
            IFileUnzipperService unzipperService = new FileUnzipperService();

            Guid testRunFileIdId = Guid.NewGuid();
            string assemblyPath = GetAssemblyPath();

            // create the directory to run tests in
            Directory.CreateDirectory(Path.Combine(assemblyPath, "TestFiles"));
            File.Copy(
                Path.Combine(assemblyPath, "TestAssets", "test.zip"),
                Path.Combine(assemblyPath, "TestFiles", $"test-{testRunFileIdId}.zip"));

            bool zipFileExistedAfterCopy = File.Exists(Path.Combine(assemblyPath, "TestFiles", $"test-{testRunFileIdId}.zip"));

            unzipperService.UnzipFile(
                Path.Combine(assemblyPath, "TestFiles", $"test-{testRunFileIdId}.zip"),
                Path.Combine(assemblyPath, "TestFiles", $"unzipped-{testRunFileIdId}")
                );

            DateTime originalFileCreationTime = File.GetCreationTimeUtc(Path.Combine(assemblyPath, "TestFiles", $"unzipped-{testRunFileIdId}", $"test.txt"));
            bool unzippedFileExists = File.Exists(Path.Combine(assemblyPath, "TestFiles", $"unzipped-{testRunFileIdId}", $"test.txt"));


            // re-unzip the file
            unzipperService.UnzipFile(
                Path.Combine(assemblyPath, "TestFiles", $"test-{testRunFileIdId}.zip"),
                Path.Combine(assemblyPath, "TestFiles", $"unzipped-{testRunFileIdId}")
                );
            DateTime secondFileCreationTime = File.GetCreationTimeUtc(Path.Combine(assemblyPath, "TestFiles", $"unzipped-{testRunFileIdId}", $"test.txt"));
            bool secondUnzippedFileExists = File.Exists(Path.Combine(assemblyPath, "TestFiles", $"unzipped-{testRunFileIdId}", $"test.txt"));


            Assert.IsTrue(zipFileExistedAfterCopy);
            Assert.IsTrue(unzippedFileExists);
            Assert.IsTrue(secondUnzippedFileExists);
            Assert.AreNotEqual(originalFileCreationTime.ToFileTimeUtc(), secondFileCreationTime.ToFileTimeUtc(), "The created timestamps for the two files should be different");
            
            // clean up the directory after running these tests
            Directory.Delete(Path.Combine(assemblyPath, "TestFiles"), recursive: true);
        }
    }
}
