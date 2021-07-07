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

            string[] list3 = { "Lindsey", "Lopez", "Clarke" };
            var results3 = SortNames.GetDistinctNames(list3);
            String[] expected3 = { };
            Assert.AreEqual(expected3, results3);
        }


        [Test]
        public void TestGetSize()
        {
            var list = new List<string> { "London Lindsey", "Mia Banena Lopez", "Hunter Uriah Mathew Clarke" };
            var results = SortNames.GetSize(list);
            var expected = 3;
            Assert.AreEqual(expected, results);
        }

        [Test]
        public void TestSorting()
        {
            string[] list = { "Lindsey", "Banena", "Apple" };
            var results = SortNames.Sorting(list);
            List<String> sorted_names = new List<string> { "Apple", "Banena", "Lindsey" };
            List<int> sorted_index = new List<int> { 2, 1, 0 };
            var expected = Tuple.Create(sorted_names, sorted_index);
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

        [Test]
        public void TestGetFistName()
        {
            var results = SortNames.GetFirstName("Beau Tristan Bentley");
            string expected = "Beau Tristan";
            Assert.AreEqual(expected, results);

            var results2 = SortNames.GetFirstName("Vaughn Lewis");
            string expected2 = "Vaughn";
            Assert.AreEqual(expected2, results2);
        }

        [Test]
        public void TestSortByLastName()
        {
            var unsorted_names = new List<string> { "Adonis Julius Archer", "Marin Alvarez",
            "Janet Parsons","Hunter Uriah Mathew Clarke"};
            string[] last_names = { "Archer", "Alvarez", "Parsons", "Clarke" };
            var results = SortNames.SortByLastName(last_names, unsorted_names);
            string[] expected = { "Marin Alvarez", "Adonis Julius Archer", "Hunter Uriah Mathew Clarke", "Janet Parsons" };
            Assert.AreEqual(expected, results);
        }

        [Test]
        public void TestSortByFirstName()
        {
            var unsorted_names = new List<string> { "B Parsons", "C Parsons", "A Parsons" };
            string[] last_names = SortNames.GetLastNames(unsorted_names);
            string[] sorted_names = SortNames.SortByLastName(last_names, unsorted_names);
            var results = SortNames.SortByFirstName(last_names, unsorted_names, sorted_names);
            string[] expected = { "A Parsons", "B Parsons", "C Parsons" };
            Assert.AreEqual(expected, results);

            var unsorted_names2 = new List<string> { "B X Parsons", "B Parsons", "C Parsons", "A Parsons" };
            string[] last_names2 = SortNames.GetLastNames(unsorted_names2);
            string[] sorted_names2 = SortNames.SortByLastName(last_names2, unsorted_names2);
            var results2 = SortNames.SortByFirstName(last_names2, unsorted_names2, sorted_names2);
            string[] expected2 = { "A Parsons", "B Parsons", "B X Parsons", "C Parsons" };
            Assert.AreEqual(expected2, results2);
        }
    }
}