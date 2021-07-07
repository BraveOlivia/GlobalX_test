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

        // Get all the duplicated names in the given name array
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

        // Invoke the program when command line input is correct and the file exists
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

        // Load the data from given file, store all the data into a list, return the list
        public static List<String> LoadData(StreamReader file)
        {
            List<String> unsorted_names = new List<String>();

            String line = file.ReadLine();
            while (line != null)
            {
                unsorted_names.Add(line);
                line = file.ReadLine();
            }
            return unsorted_names;
        }

        // Get the size of the given list
        public static int GetSize(List<String> names)
        {
            return names.Count;
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

        // Get all the last names from given name list, return the result as an array
        public static string[] GetLastNames(List<String> unsorted_names)
        {
            int size = GetSize(unsorted_names);
            string[] last_names = new string[size];

            for (int i = 0; i < size; i++)
            {
                String name = unsorted_names[i];
                string[] words = name.Split(" ");
                last_names[i] = words[words.Length - 1];
            }
            return last_names;
        }

        public static string[] GetFirstNames(List<String> unsorted_names)
        {
            int size = GetSize(unsorted_names);
            string[] first_names = new string[size];


            for (int i = 0; i < size; i++)
            {
                string full_name = unsorted_names[i];
                int indexOfLastEmptySpace = full_name.LastIndexOf(" ");
                string first_name = full_name.Substring(0, indexOfLastEmptySpace);
                // System.Console.WriteLine(first_name);
                first_names[i] = first_name;
            }
            System.Console.WriteLine("****************************************************************s");
            foreach (String name in first_names)
            {
                System.Console.WriteLine(name);
            }
            return first_names;
        }


        public static string GetFirstName(string full_name)
        {
            int indexOfLastEmptySpace = full_name.LastIndexOf(" ");
            return full_name.Substring(0, indexOfLastEmptySpace);
        }

        public static string GetFullName(string first_name)
        {
            string full_name = "";
            return full_name;
        }


        public static string[] GetSortedNames(string[] last_names, List<String> unsorted_names)
        {
            (List<String> sorted_last_names, List<int> sorted__last_index) = Sorting(last_names);

            int size = GetSize(unsorted_names);
            string[] sorted_names = new string[size];

            for (int i = 0; i < size; i++)
            {
                int original_index = sorted__last_index[i];
                String orignal_name = unsorted_names[original_index];
                sorted_names[i] = orignal_name;
            }
            return sorted_names;
        }

        static void Main(string[] args)
        {
            StreamReader file = ValidateInput(args[0], args[1]);

            List<String> unsorted_names = LoadData(file);
            int size = GetSize(unsorted_names);

            // Get all last names as an array, sort them
            string[] last_names = GetLastNames(unsorted_names);

            (List<String> sorted_last_names, List<int> sorted__last_index) = Sorting(last_names);

            string[] sorted_names = GetSortedNames(last_names, unsorted_names);




            String[] distinct_last_names = GetDistinctNames(last_names);

            foreach (String name in distinct_last_names)
            {
                // Find the nex indexes and old indexes of duplicated last names in arraies.
                int[] new_indexes = FindIndexes(sorted_last_names.ToArray(), name);
                int[] old_indexes = FindIndexes(last_names, name);

                // List<String> full_names = new List<String>();
                // foreach (int index in old_indexes)
                // {
                //     full_names.Add(unsorted_names[index]);
                // }
                // string[] unsorted_first_names = full_names.ToArray();

                List<String> first_names = new List<String>();
                Dictionary<string, string> name_dict = new Dictionary<string, string>();

                foreach (int index in old_indexes)
                {
                    string first_name = GetFirstName(unsorted_names[index]);
                    string full_name = unsorted_names[index];
                    first_names.Add(first_name);
                    name_dict.Add(first_name, full_name);
                    // full_names.Add(full_name);
                    // System.Console.WriteLine(full_name);
                }
                string[] unsorted_first_names = first_names.ToArray();
                // string[] unsorted_full_names = full_names.ToArray();

                System.Console.WriteLine("****************************************************************");
                foreach (String n in unsorted_first_names)
                {
                    System.Console.WriteLine(n);
                }

                System.Console.WriteLine("@@@@@@@@@@@@@@@@");
                // string[] unsorted_first_names = GetFirstNames(unsorted_names);
                (List<String> sorted_first_names, List<int> sorted_first_index) = Sorting(unsorted_first_names);
                foreach (string n in sorted_first_names)
                {
                    System.Console.WriteLine(n);
                }

                foreach (int n in sorted_first_index)
                {
                    System.Console.WriteLine(n);
                }

                for (int i = 0; i < old_indexes.Length; i++)
                {
                    // sorted_names[new_indexes[i]] = sorted_first_names[i];
                    string full_name = name_dict[sorted_first_names[i]];
                    sorted_names[new_indexes[i]] = full_name;
                }

                // String[] new_names = GetFirstNames(unsorted_names);
            }


            System.Console.WriteLine("================================================================");
            PrintResults(sorted_names);


        }
    }
}