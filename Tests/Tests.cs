using System;
using System.Collections.Generic;
using NUnit.Framework;
using GlobalX;

namespace Tests
{
    public class SortingTests
    {
        [Test]
        public void TestFindIndexes()
        {
            string[] list = { "Lindsey", "Lopez", "Clarke", "Lindsey" };
            var results = SortNames.FindIndexes(list, "Lindsey");
            int[] expected = { 0, 3 };
            Assert.AreEqual(expected, results);
        }

        [Test]
        public void TestGetDistinctNames()
        {
            string[] list = { "Lindsey", "Lopez", "Clarke", "Lindsey" };
            var results = SortNames.GetDistinctNames(list);
            String[] expected = { "Lindsey" };
            Assert.AreEqual(expected, results);

            string[] list2 = { "Lindsey", "Lopez", "Clarke", "Lindsey", "Lopez", "Lopez" };
            var results2 = SortNames.GetDistinctNames(list2);
            String[] expected2 = { "Lindsey", "Lopez" };
            Assert.AreEqual(expected2, results2);
        }

        [Test]
        public void TestGetSize()
        {
            var list = new List<string> { "London Lindsey", "Mia Banena Lopez", "Hunter Uriah Mathew Clarke" };
            var results = SortNames.GetSize(list);
            int expected = 3;
            Assert.AreEqual(expected, results);
        }

        [Test]
        public void TestGetLastNames()
        {
            var list = new List<string> { "London Lindsey", "Mia Banena Lopez", "Hunter Uriah Mathew Clarke" };
            var results = SortNames.GetLastNames(list);

            string[] expected = { "Lindsey", "Lopez", "Clarke" };
            Assert.AreEqual(expected, results);
        }

    }
}