using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{

    static void FindAllDuplicateIndexes(String[] names)
    {
        var duplicateIndexes = names
            .Select((t, i) => new { Index = i, Text = t })
            .GroupBy(g => g.Text)
            .Where(g => g.Count() > 1)
            .SelectMany(g => g, (g, x) => x.Index);
        Console.WriteLine("Duplicate indexes: " + String.Join(",", duplicateIndexes));
    }

    // Find the original index of specific name in the given array 
    static int[] FindIndexes(String[] names, String name)
    {
        var indexes = names
            .Select((f, i) => new { f, i })
            .Where(x => x.f == name)
            .Select(x => x.i);
        Console.WriteLine("Indices of specific name: " + String.Join(",", indexes));
        return indexes.ToArray();
    }


    static String[] GetDistinctNames(String[] names)
    {
        var distinctNames = names.GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .Select(y => y.Key)
              .ToList();
        Console.WriteLine("Distinct Names: " + String.Join(",", distinctNames));
        return distinctNames.ToArray();
    }

    // print the sorted result and write to a file
    static void PrintResults(String[] sorted_names)
    {
        foreach (String name in sorted_names)
        {
            Console.WriteLine(name);
            File.WriteAllLines("sorted-names-list.txt", sorted_names);
        }
    }

    static void Main(string[] args)
    {
        System.Console.WriteLine(args[0]);
        System.Console.WriteLine(args[1]);

        StreamReader r = new StreamReader("unsorted-names-list.txt");
        List<String> unsorted_names = new List<String>();

        int count_names = 0;
        String line = r.ReadLine();
        while (line != null)
        {
            unsorted_names.Add(line);
            count_names++;
            line = r.ReadLine();
        }

        string[] last_names = new string[count_names];
        string[] sorted_names = new string[count_names];

        Console.WriteLine(count_names);
        for (int j = 0; j < count_names; j++)
        {
            String name = unsorted_names[j];
            string[] words = name.Split(" ");
            last_names[j] = words[words.Length - 1];
        }

        var sorted = last_names
            .Select((x, i) => new KeyValuePair<string, int>(x, i))
            .OrderBy(x => x.Key)
            .ToList();

        List<String> sorted_last_names = sorted.Select(x => x.Key).ToList();
        List<int> sorted_index = sorted.Select(x => x.Value).ToList();


        // get the orignal indexes of sorted last names, add the value to sorted list
        for (int i = 0; i < count_names; i++)
        {
            int original_index = sorted_index[i];
            String orignal_name = unsorted_names[original_index];
            sorted_names[i] = orignal_name;
        }


        // find duplication indexes of last names
        FindAllDuplicateIndexes(last_names);

        String[] distinct_names = GetDistinctNames(last_names);
        int[] raw_indexes;
        int[] new_indexes;
        List<String> first_names;

        foreach (String name in distinct_names)
        {
            System.Console.WriteLine("****************************************************************");

            raw_indexes = FindIndexes(last_names, name);
            System.Console.WriteLine("To sort name: " + name);
            first_names = new List<String>();
            foreach (int index in raw_indexes)
            {
                System.Console.WriteLine(unsorted_names[index]);
                first_names.Add(unsorted_names[index]);
            }
            System.Console.WriteLine("new_indexes ->>");
            new_indexes = FindIndexes(sorted_last_names.ToArray(), name);

            string[] unsorted_first_names = first_names.ToArray();
            var sorted2 = unsorted_first_names
            .Select((x, i) => new KeyValuePair<string, int>(x, i))
            .OrderBy(x => x.Key)
            .ToList();

            List<String> B = sorted2.Select(x => x.Key).ToList();
            List<int> sorted_index2 = sorted2.Select(x => x.Value).ToList();

            
            System.Console.WriteLine("================================");
            PrintResults(sorted_names);

            for (int i = 0; i < raw_indexes.Length; i++)
            {
                
                sorted_names[new_indexes[i]] = B[i];
            }

            // System.Console.WriteLine("////////////////////////////////");
            // foreach (String s in B)
            // {
            //     System.Console.WriteLine(s);
            // }
            // foreach (int s in sorted_index2)
            // {
            //     System.Console.WriteLine(s);
            //     System.Console.WriteLine(B[sorted_index2[s]]);
            // }
        }


        // read file

        // sorting
        // Step 1: Sort all the last name
        // if all the last names different with each other, done

        // if repetitation exist, compare the first name
        // if first names equal, compare second name
        // if second names equal, compare third name

        // write file
    }
}

