using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1ExamCSharpp
{
    public class Menu
    {
        private List<DictionaryManager> Dictionaries = new List<DictionaryManager>();
        private DictionaryManager dictionary;
        private IOManager io = new IOManager();
        private string path = Directory.GetCurrentDirectory() + @"\Dictionaries";

        public void StartMenu()
        {
            try
            {
                Console.WriteLine("============== MENU ==============\n");
                Console.WriteLine("1 ) Create Dictionary\n");
                Console.WriteLine("2 ) Select Dictionaries\n");
                Console.WriteLine("3 ) Delete Dictionaries\n");
                Console.WriteLine("3 ) Exit\n");
                Console.WriteLine("==================================\n");
                Console.Write("Enter Opt : ");
                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1: CreateDictionary(); break;
                    case 2: SelectDictionary(); break;
                    case 3: DeleteDictionary(); break;
                    case 4: break;
                    default: StartMenu(); break;
                }
            }
            catch (Exception ex) { Console.Clear(); Console.WriteLine(ex.Message); StartMenu(); }
        }

        public void CreateDictionary()
        {
            Console.Write("Enter dictionary type (e.g. English-Khmer): ");
            string type = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(type))
            {
                Console.Clear();
                Console.WriteLine("Dictionary type cannot be empty.");
                StartMenu();
            }

            var fileExists = io.LoadFiles(path)
                               .Any(file => io.GetFileName(file) == type);

            if (fileExists)
            {
                Console.Clear();
                Console.WriteLine("Dictionary type already exists.");
                StartMenu();
            }
            else
            {
                Dictionaries.Add(new DictionaryManager(type));
                io.WriteJson(path, type, new List<WordTranslation>());
                Console.WriteLine("Dictionary added successfully.");
            }

            Console.WriteLine();
            StartMenu();
        }
        public void DeleteDictionary()
        {
            Console.Clear();
            Console.WriteLine("============== DELETE DICTIONARY ==============\n");

            ShowDictionary();
            Console.WriteLine("===============================================");

            List<FileInfo> dictionaries = io.LoadFiles(path);

            if (dictionaries.Count == 0)
            {
                Console.WriteLine("No dictionaries found to delete.");
                StartMenu();
                return;
            }

            Console.Write("\nEnter the name of the dictionary to delete: ");
            string selectedName = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(selectedName))
            {
                Console.WriteLine("You must enter a dictionary name.");
                StartMenu();
                return;
            }

            var selectedDictionary = dictionaries.FirstOrDefault(d => io.GetFileName(d).Equals(selectedName, StringComparison.OrdinalIgnoreCase));

            if (selectedDictionary != null)
            {
                Console.Write($"Are you sure you want to delete the dictionary '{selectedName}'? (y/n): ");
                string confirmation = Console.ReadLine()?.Trim().ToLower();

                if (confirmation == "y" || confirmation == "yes")
                {
                    try
                    {
                        File.Delete(selectedDictionary.FullName); 
                        Console.WriteLine($"Dictionary '{selectedName}' has been deleted successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting dictionary: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Deletion canceled.");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"No dictionary found with the name '{selectedName}'. Please try again.");
            }

            StartMenu();
        }


        public void ShowDictionary()
        {
            List<FileInfo> dictionaries = io.LoadFiles(path);
            int index = 1;
            if (dictionaries.Count > 0)
            {
                foreach (FileInfo dictionary in dictionaries)
                {
                    Console.WriteLine($"{index}. {io.GetFileName(dictionary)}");
                    Console.WriteLine("\n");
                    index++;
                }
            }
        }

        public void SelectDictionary()
        {
            Console.Clear();
            Console.WriteLine("============== SELECT DICTIONARY ==============\n");
            ShowDictionary();
            Console.WriteLine("===============================================");
            List<FileInfo> dictionaries = io.LoadFiles(path);

            if (dictionaries.Count == 0)
            {
                Console.WriteLine("No dictionaries found.");
                StartMenu();
                return;
            }

            Console.Write("\nEnter the name of the dictionary to select: ");
            string selectedName = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(selectedName))
            {
                Console.WriteLine("You must enter a dictionary name.");
                StartMenu();
                return;
            }

            var selectedDictionary = dictionaries.FirstOrDefault(d => io.GetFileName(d).Equals(selectedName, StringComparison.OrdinalIgnoreCase));

            if (selectedDictionary != null)
            {
                Console.WriteLine($"Dictionary '{selectedName}' is selected.");

                var customDictionary = new DictionaryManager(selectedName);
                var dictionaryMenu = new DictionaryMenu(customDictionary);
                dictionaryMenu.StartDictionaryMenu(selectedName);
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"No dictionary found with the name '{selectedName}'. Please try again.");
            }

            StartMenu();
        }

    }
}
