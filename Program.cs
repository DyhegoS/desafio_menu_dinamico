
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
                GenerateTree(lines);
                
            }
        }

        static GenericTree<MenuItem> GenerateTree(string[] records)
        {
            GenericTree<MenuItem> myTree = new GenericTree<MenuItem>();
            Dictionary<int, MenuItem> Dict = new Dictionary<int, MenuItem>();
            foreach (string record in records)
            {
                string cleanLine = record.Trim().Trim('[', ']', '"');

                if (string.IsNullOrWhiteSpace(cleanLine))
                    continue;

                string[] parts = cleanLine.Split(',');
                int Id = int.Parse(parts[0]);
                string Text = parts[1];
                string Route = parts[2];
                int IdParent = 0;
                if (parts[3] != '"'.ToString())
                {
                    IdParent = int.Parse(parts[3]);
                }

                Dict.Add(Id, new MenuItem(Text, Route));
                

            }
            return myTree;
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

        public static void PrintBfs<T>(GenericTree<T> tree)
        {
            IPosition<T>? root = tree.Root();
            if (root == null)
            {
                return;
            }
            Queue<IPosition<T>> queue = new Queue<IPosition<T>>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                IPosition<T> position = queue.Dequeue();
                Console.WriteLine(position.Element());
                foreach (IPosition<T> child in tree.Children(position))
                {
                    queue.Enqueue(child);
                }
            }
        }
    }
}
