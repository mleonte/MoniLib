using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Xunit;
using MoniLib.Data;

namespace MoniLib.Tests.Data
{
    public class FileDataTestFixture : IDisposable
    {
        public string TempFilePath;
        public FileDataTestFixture()
        {
            TempFilePath = Path.GetTempFileName();
        }

        public void Dispose()
        {
            File.Delete(TempFilePath);
        }
    }

    public class JsonDataCollectionTest : IClassFixture<FileDataTestFixture>
    {
        FileDataTestFixture fixture;
        public JsonDataCollectionTest(FileDataTestFixture fixture)
        {
            this.fixture = fixture;
        }

        private string TempFilePath => fixture.TempFilePath;

        private struct TestDataStruct
        {
            public int Id;
            public bool B;
            public string C;
        }

        [Fact]
        public void StoreOne()
        {
            var collection = new JsonDataCollection<int, TestDataStruct>(TempFilePath);
            var obj = new TestDataStruct { Id = 1, B = true, C = "Test" };
            collection[obj.Id] = obj;
            collection.Commit();

            collection.Clear();
            Assert.Empty(collection.All);
            collection.Refresh();
            
            Assert.Single(collection.All);
            var obj2 = collection[obj.Id];
            Assert.Equal(obj.B, obj2.B);
            Assert.Equal(obj.C, obj2.C);
        }

        [Fact]
        public void StoreMany()
        {
            var collection = new JsonDataCollection<int, TestDataStruct>(TempFilePath);
            collection.Add(
                s => s.Id,
                new List<TestDataStruct>
                {
                    new TestDataStruct { Id = 1, B = true, C = "Test1" },
                    new TestDataStruct { Id = 2, B = false, C = "Test2" }
                }
            );
            collection.Commit();

            collection.Clear();
            Assert.Empty(collection.All);
            collection.Refresh();

            Assert.Equal("Test1", collection[1].C);
            Assert.Equal("Test2", collection[2].C);
        }
    }
}
