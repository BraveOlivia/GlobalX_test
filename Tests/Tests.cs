using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Tests
{
    public class SortingTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGetLastNames()
        {
            List<String> unsorted_names = new List<String>();
            unsorted_names.Add("London Lindsey");
            unsorted_names.Add("Mia Banena Lopez");
            unsorted_names.Add("Hunter Uriah Mathew Clarke");
            var results = GlobalX.SortNames.GetLastNames(3, unsorted_names);

            string[] expected = { "Lindsey", "Lopez", "Clarke" };
            Assert.AreEqual(expected, results);
        }

        [Test]
        public void TestGetIndexes()
        {
            // string[] expected = { "Lindsey", "Lopez", "Clarke" };
            Assert.AreEqual(1, 1);
        }
    }
}