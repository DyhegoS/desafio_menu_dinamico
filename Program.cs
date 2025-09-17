
﻿using MenuDinamico;

namespace Course
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string CsvFolder = "./resources";
            string[] CsvFiles = Directory.GetFiles(CsvFolder, "*.csv");

            foreach (string file in CsvFiles)
            {
                string[] lines = File.ReadAllLines(file);
                Print(GenerateTree(lines));
                Console.WriteLine("========================================================");
            }           
        }

        static GenericTree<MenuItem> GenerateTree(string[] records)
        {
            GenericTree<MenuItem> tree = new GenericTree<MenuItem>();
            var Dict = new Dictionary<int, MenuItem>();
            foreach (string record in records)
            {
                string[] parts = record.Split(',');
                int Id = int.Parse(parts[0]);
                string Text = parts[1];
                string Route = "null";
                if (!string.IsNullOrWhiteSpace(parts[2]))
                {
                    Route = parts[2];
                } 
                int IdParent = 0;
                if (!string.IsNullOrWhiteSpace(parts[3]))
                {
                    IdParent = int.Parse(parts[3]);
                }

                Dict.Add(Id, new MenuItem(Text, Route));
        
                if (Dict.ContainsKey(IdParent))
                {
                    IPosition<MenuItem>? parent = tree.Find(Dict[IdParent]);
                    tree.Add(Dict[Id], parent);
                }
                else
                {
                    tree.Add(Dict[Id], null);
                }
                
            }        
            
            return tree;
        }

        public static void Print<T>(GenericTree<T> tree)
        {
            PrintRecursive(tree.Root(), tree, 0);
        }

        public static void PrintRecursive<T>(IPosition<T>? position, GenericTree<T> tree, int depth)
        {
            if (position == null) {
                return;
            }
            string spaces = new String(' ', depth * 4);
            Console.WriteLine(spaces + position.Element());
            foreach (IPosition<T> child in tree.Children(position))
            {
                PrintRecursive(child, tree, depth + 1);
            }
        }
    }
}
