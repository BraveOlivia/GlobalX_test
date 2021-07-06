using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        System.Console.WriteLine(args[0]);
        System.Console.WriteLine(args[1]);

        StreamReader r = new StreamReader("unsorted-names-list.txt");

        string[] unsorted_names = new string[20];

        int i = 0;
        String line = r.ReadLine();
        while (line != null)
        {
            unsorted_names[i] = line;
            i++;
            line = r.ReadLine();
        }

        string[] last_names = new string[i];

        string[] sorted_names = new string[i];

        Console.WriteLine(i);
        for (int j = 0; j < i; j++)
        {
            String name = unsorted_names[j];
            if (name != null)
            {
                // Console.WriteLine(name);
                string[] words = name.Split(" ");
                last_names[j] = words[words.Length - 1];
                // first_names[j] = words[0];
                // Console.WriteLine(last_names[j]);
            }
        }

        var sorted = last_names
            .Select((x, i) => new KeyValuePair<string, int>(x, i))
            .OrderBy(x => x.Key)
            .ToList();

        // List<String> B = sorted.Select(x => x.Key).ToList();
        List<int> sorted_index = sorted.Select(x => x.Value).ToList();


        // get the orignal indexes of sorted last names, add the value to sorted list
        for (int y = 0; y < i; y++)
        {
            int original_index = sorted_index[y];
            sorted_names[y] = unsorted_names[original_index];
        }

        // print the sorted result and write to a file
        foreach (String name in sorted_names)
        {
            Console.WriteLine(name);
            File.WriteAllLines("sorted-names-list.txt", sorted_names);
        }

        // find duplication indexes of last names
        var duplicateIndexes = last_names
            .Select((t, i) => new { Index = i, Text = t })
            .GroupBy(g => g.Text)
            .Where(g => g.Count() > 1)
            .SelectMany(g => g, (g, x) => x.Index);
        Console.WriteLine("Duplicate indexes: " + String.Join(",", duplicateIndexes));

        var distinctNames = last_names.GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .Select(y => y.Key)
              .ToList();
        Console.WriteLine("Distinct Names: " + String.Join(",", distinctNames));

        System.Console.WriteLine("****************************************************************");

        var indices = last_names
                .Select((f, i) => new { f, i })
                .Where(x => x.f == "Lopez")
                .Select(x => x.i);
        Console.WriteLine("Duplicate names: " + String.Join(",", indices));

        // duplicateIndexes.ToList().ForEach(i => System.Console.WriteLine(i));

        List<String> first_names = new List<String>();
        duplicateIndexes.ToList().ForEach(i => first_names.Add(unsorted_names[i]));
        string[] unsorted_first_names = first_names.ToArray();

        System.Console.WriteLine("unsorted first names: ");
        foreach (string name in unsorted_first_names)
        {
            System.Console.WriteLine(name);
        }

        var sorted2 = unsorted_first_names
            .Select((x, i) => new KeyValuePair<string, int>(x, i))
            .OrderBy(x => x.Key)
            .ToList();

        List<String> B = sorted2.Select(x => x.Key).ToList();
        List<int> sorted_index2 = sorted2.Select(x => x.Value).ToList();

        // get the orignal indexes of sorted last names, add the value to sorted list
        // for (int y = 0; y < i; y++)
        // {
        //     int original_index = sorted_index2[y];
        //     sorted_names[original_index] = sorted_names[original_index];
        // }
        System.Console.WriteLine("****************************************************************");
        foreach (String s in B)
        {
            System.Console.WriteLine(s);
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

    public String[] sorting(String filename)
    {
        String[] sorted = new String[0];
        return sorted;
    }

}

