using System;
using Xunit;
using MoniLib.Collections;

namespace MoniLib.Tests.Collections
{
    public class NestedDictionaryTests
    {
        [Fact]
        public void Level2()
        {
            var dict = new NestedDictionary<int, bool, string>();
            dict[1, true] = "Test";

            Assert.True(dict.ContainsKey(1, true));
            Assert.Equal("Test", dict[1, true]);
        }

        [Fact]
        public void Level3_2Keys()
        {
            var dict = new NestedDictionary<int, bool, int, string>();
            dict[1, true, 2] = "Test";

            Assert.True(dict.ContainsKey(1, true));
            Assert.Single(dict[1, true]);
        }

        [Fact]
        public void Level3_3Keys()
        {
            var dict = new NestedDictionary<int, bool, int, string>();
            dict[1, true, 2] = "Test";

            Assert.True(dict.ContainsKey(1, true, 2));
            Assert.Equal("Test", dict[1, true, 2]);
        }

        [Fact]
        public void Level4_2Keys()
        {
            var dict = new NestedDictionary<int, bool, int, bool, string>();
            dict[1, true, 2, false] = "Test";

            Assert.True(dict.ContainsKey(1, true));
            Assert.Single(dict[1, true]);
        }

        [Fact]
        public void Level4_3Keys()
        {
            var dict = new NestedDictionary<int, bool, int, bool, string>();
            dict[1, true, 2, false] = "Test";

            Assert.True(dict.ContainsKey(1, true, 2));
            Assert.Single(dict[1, true, 2]);
        }

        [Fact]
        public void Level4_4Keys()
        {
            var dict = new NestedDictionary<int, bool, int, bool, string>();
            dict[1, true, 2, false] = "Test";

            Assert.True(dict.ContainsKey(1, true, 2, false));
            Assert.Equal("Test", dict[1, true, 2, false]);
        }
    }
}
