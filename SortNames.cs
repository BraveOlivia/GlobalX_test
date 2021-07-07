using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


/* 
Sort all the last names. If all the last names different with each other, the sorting is done.
If repetitations exist, treat all the first name(s) as a string. Compare the first_name strings.
*/
namespace GlobalX
{
    public class SortNames
    {
        // Find the original index of specific name in the given array 
        public static int[] FindIndexes(String[] names, String name)
        {
            var indexes = names
                .Select((f, i) => new { f, i })
                .Where(x => x.f == name)
                .Select(x => x.i);
            // Console.WriteLine(String.Join(",", indexes));
            return indexes.ToArray();
        }


        public static String[] GetDistinctNames(String[] names)
        {
            var distinctNames = names.GroupBy(x => x)
                  .Where(g => g.Count() > 1)
                  .Select(y => y.Key)
                  .ToList();
            // Console.WriteLine("Distinct Names: " + String.Join(",", distinctNames));
            return distinctNames.ToArray();
        }

        // print the sorted result and write to a file
        public static void PrintResults(String[] sorted_names)
        {
            foreach (String name in sorted_names)
            {
                Console.WriteLine(name);
                File.WriteAllLines("sorted-names-list.txt", sorted_names);
            }
        }

        public static StreamReader ValidateInput(String arg1, String arg2)
        {
            if (arg1 == "name-sorter" && arg2.Substring(0, 2) == "./")
            {
                try
                {
                    StreamReader file = new StreamReader(arg2.Substring(2));
                    return file;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"The error is found: '{e}'");
                }
            }
            return null;
        }

        public static Tuple<List<String>, int> LoadData(StreamReader file)
        {
            List<String> unsorted_names = new List<String>();

            int count_names = 0;
            String line = file.ReadLine();
            while (line != null)
            {
                unsorted_names.Add(line);
                count_names++;
                line = file.ReadLine();
            }
            return Tuple.Create(unsorted_names, count_names);
        }

        // Sort the given array, return the sorted array and the sorted indexes array
        public static Tuple<List<String>, List<int>> Sorting(string[] names)
        {
            var sorted = names
                .Select((x, i) => new KeyValuePair<string, int>(x, i))
                .OrderBy(x => x.Key)
                .ToList();

            List<String> sorted_names = sorted.Select(x => x.Key).ToList();
            List<int> sorted_index = sorted.Select(x => x.Value).ToList();

            return Tuple.Create(sorted_names, sorted_index);
        }

        public static string[] GetLastNames(int count_names, List<String> unsorted_names)
        {
            string[] last_names = new string[count_names];

            for (int i = 0; i < count_names; i++)
            {
                String name = unsorted_names[i];
                string[] words = name.Split(" ");
                last_names[i] = words[words.Length - 1];
            }
            return last_names;
        }
        public static string[] GetFirstNames(int count_names, List<String> unsorted_names)
        {
            string[] first_names = new string[count_names];

            for (int i = 0; i < count_names; i++)
            {
                String name = unsorted_names[i];
                string[] words = name.Split(" ");
                first_names[i] = words[words.Length - 1];
            }
            return first_names;
        }

        static void Main(string[] args)
        {
            StreamReader file = ValidateInput(args[0], args[1]);
            (List<String> unsorted_names, int count_names) = LoadData(file);

            // Get all last names as an array, sort them
            string[] last_names = GetLastNames(count_names, unsorted_names);
            (List<String> sorted_last_names, List<int> sorted__last_index) = Sorting(last_names);

            // save the sorting results into a new array
            string[] sorted_names = new string[count_names];
            for (int i = 0; i < count_names; i++)
            {
                int original_index = sorted__last_index[i];
                String orignal_name = unsorted_names[original_index];
                sorted_names[i] = orignal_name;
            }

            String[] distinct_last_names = GetDistinctNames(last_names);

            foreach (String name in distinct_last_names)
            {
                // Find the nex indexes and old indexes of duplicated last names in arraies.
                int[] new_indexes = FindIndexes(sorted_last_names.ToArray(), name);
                int[] old_indexes = FindIndexes(last_names, name);

                List<String> full_names = new List<String>();
                foreach (int index in old_indexes)
                {
                    full_names.Add(unsorted_names[index]);
                }
                string[] unsorted_first_names = full_names.ToArray();

                (List<String> sorted_first_names, List<int> sorted_first_index) = Sorting(unsorted_first_names);

                for (int i = 0; i < old_indexes.Length; i++)
                {
                    sorted_names[new_indexes[i]] = sorted_first_names[i];
                }
            }

            PrintResults(sorted_names);
        }
    }
}