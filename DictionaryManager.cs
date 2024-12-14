using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1ExamCSharpp
{
    public class DictionaryManager
    {
        public string DictionaryType;
        private List<WordTranslation> WordTranslations = new List<WordTranslation>();
        private IOManager io = new IOManager();
        private string path = Directory.GetCurrentDirectory() + @"\Dictionaries";
        private string exportPath = Directory.GetCurrentDirectory() + @"\Export";

        public DictionaryManager() { }
        public DictionaryManager(string dictionaryType)
        {
            this.DictionaryType = dictionaryType;
        }

        public void ViewAllWords(string dictionaryType)
        {
            string fullPath = Path.Combine(path, dictionaryType + ".json");
            if (File.Exists(fullPath))
            {
                WordTranslations = io.ReadJson<List<WordTranslation>>(path, dictionaryType);
            }

            Console.WriteLine();
            foreach (WordTranslation wordTranslation in WordTranslations)
            {
                wordTranslation.Display();
            }
        }

        public int DisplayWordsToSelect(string dictionaryType)
        {
            string fullPath = Path.Combine(path, dictionaryType + ".json");
            if (File.Exists(fullPath))
            {
                WordTranslations = io.ReadJson<List<WordTranslation>>(path, dictionaryType);
            }

            Console.WriteLine();
            int index = 1;
            foreach (WordTranslation wordTranslation in WordTranslations)
            {
                Console.WriteLine($"{index}. {wordTranslation.Word}");
                index++;
            }
            Console.Write("Select word: ");
            int selected = int.Parse(Console.ReadLine());

            return selected;
        }

        public WordTranslation FindWord(string word, string dictionaryType)
        {
            string fullPath = Path.Combine(path, dictionaryType + ".json");
            if (File.Exists(fullPath))
            {
                WordTranslations = io.ReadJson<List<WordTranslation>>(path, dictionaryType);
            }

            WordTranslation wordTranslation = WordTranslations.Find(w => w.Word == word);

            if (wordTranslation != null)
            {
                return wordTranslation;
            }
            else
            {
                return null;
            }
        }
        public void AddWord(string dictionaryType)
        {
            string key;
            while (true)
            {
                Console.Write("\nAdd word: ");
                key = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(key))
                {
                    Console.WriteLine("Word cannot be empty. Please enter a valid word.");
                }
                else if (FindWord(key, dictionaryType) != null)
                {
                    Console.WriteLine("Word already exists. Please enter a different word.");
                    break;
                }
                else
                {
                    break;
                }
            }

            var values = new List<string>();
            string input;

            while (true)
            {
                Console.Write("Enter translation (e to exit): ");
                input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Translation cannot be empty. Please enter a valid translation.");
                    continue;
                }

                if (input.ToLower() == "e")
                {
                    if (values.Count == 0)
                    {
                        Console.WriteLine("You must enter at least one translation.");
                        continue;
                    }
                    break;
                }

                if (!values.Contains(input))
                {
                    values.Add(input);
                }
                else
                {
                    Console.WriteLine("Translation already exists.");
                }
            }

            string fullPath = Path.Combine(path, dictionaryType + ".json");
            WordTranslations = File.Exists(fullPath) ? io.ReadJson<List<WordTranslation>>(path, dictionaryType) : new List<WordTranslation>();

            WordTranslations.Add(new WordTranslation(key, values));
            io.WriteJson(path, dictionaryType, WordTranslations);

            Console.WriteLine("Word added successfully");
        }


        public void AddTranslation(string dictionaryType)
        {
            int selected = DisplayWordsToSelect(dictionaryType);

            string fullPath = Path.Combine(path, dictionaryType + ".json");

            if (File.Exists(fullPath))
            {
                WordTranslations = io.ReadJson<List<WordTranslation>>(path, dictionaryType);
            }

            if (selected > 0 && selected <= WordTranslations.Count)
            {
                WordTranslation wordTranslation = WordTranslations[selected - 1];
                string input;

                while (true)
                {
                    Console.Write("Enter translation (e to exit): ");
                    input = Console.ReadLine()?.Trim();

                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("Translation cannot be empty. Please enter a valid translation.");
                        continue;
                    }

                    if (input.ToLower() == "e")
                    {
                        break;
                    }


                    if (!wordTranslation.Translations.Contains(input))
                    {
                        wordTranslation.Translations.Add(input);
                        Console.WriteLine($"Translation '{input}' added successfully.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("This translation already exists.");
                    }
                }

                io.WriteJson(path, dictionaryType, WordTranslations);
            }
            else
            {
                Console.WriteLine("Invalid selection. Please choose a valid word.");
            }
        }

        public void RemoveWord(string dictionaryType)
        {
            int selected = DisplayWordsToSelect(dictionaryType);

            string fullPath = Path.Combine(path, dictionaryType + ".json");
            if (File.Exists(fullPath))
            {
                WordTranslations = io.ReadJson<List<WordTranslation>>(path, dictionaryType);
            }

            if (selected > 0 && selected <= WordTranslations.Count)
            {
                WordTranslations.RemoveAt(selected - 1);

                io.WriteJson(path, dictionaryType, WordTranslations);

                Console.WriteLine("word removed success");
            }
            else
            {
                Console.WriteLine("invalid selection");
            }
        }

        public void RemoveTranslation(string dictionaryType)
        {
            int selected = DisplayWordsToSelect(dictionaryType);

            string fullPath = Path.Combine(path, dictionaryType + ".json");
            if (File.Exists(fullPath))
            {
                WordTranslations = io.ReadJson<List<WordTranslation>>(path, dictionaryType);
            }

            if (selected > 0 && selected <= WordTranslations.Count)
            {
                WordTranslation wordTranslation = WordTranslations[selected - 1];
                wordTranslation.removeTranslation();

                io.WriteJson(path, dictionaryType, WordTranslations);
            }
            else
            {
                Console.WriteLine("invalid selection");
            }
        }

        public void ReplaceWord(string dictionaryType)
        {
            int selection = DisplayWordsToSelect(dictionaryType);
            string fullPath = Path.Combine(path, dictionaryType + ".json");

            if (File.Exists(fullPath))
            {
                WordTranslations = io.ReadJson<List<WordTranslation>>(path, dictionaryType);
            }
            if (selection > 0 && selection <= WordTranslations.Count)
            {
                WordTranslation wordTranslation = WordTranslations[selection - 1];
                Console.Write("Enter new word: ");
                string newWord = Console.ReadLine();

                if (WordTranslations.Any(w => w.Word.Equals(newWord, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("Word already exists.");
                }
                else
                {
                    wordTranslation.Word = newWord;
                    io.WriteJson(path, dictionaryType, WordTranslations);
                }
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }
        }


        public void ReplaceTranslation(string dictionaryType)
        {
            int selection = DisplayWordsToSelect(dictionaryType);

            string fullPath = Path.Combine(path, dictionaryType + ".json");
            if (!File.Exists(fullPath))
            {
                Console.WriteLine("Dictionary file not found.");
                return;
            }
            WordTranslations = io.ReadJson<List<WordTranslation>>(path, dictionaryType);

            if (selection <= 0 || selection > WordTranslations.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            WordTranslation wordTranslation = WordTranslations[selection - 1];

            wordTranslation.replaceTranslation();

            io.WriteJson(path, dictionaryType, WordTranslations);

        }



        public void SearchWord(string word, string dictionaryType)
        {
            WordTranslation wordTranslation = FindWord(word, dictionaryType);
            if (wordTranslation != null)
            {
                wordTranslation.Display();
            }
            else
            {
                Console.WriteLine("word doesn't exist");
            }
        }

        public void exportWord(string dictionaryType)
        {
            Console.Write("Enter the word to export: ");
            string userInput = Console.ReadLine();
            WordTranslations = io.ReadJson<List<WordTranslation>>(path, dictionaryType);

            WordTranslation wordTranslation = WordTranslations.FirstOrDefault(w => string.Equals(w.Word, userInput, StringComparison.OrdinalIgnoreCase));
            if (wordTranslation != null)
            {
                string content = $"Word: {wordTranslation.Word}\nTranslations:\n";
                foreach (string translation in wordTranslation.Translations)
                {
                    content += $"- {translation}\n";
                }
                io.EnsureDirectoryExists(exportPath);
                string filePath = Path.Combine(exportPath, wordTranslation.Word + ".txt");
                try
                {
                    File.WriteAllText(filePath, content);
                    Console.WriteLine("Word exported successfully to text file.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error exporting word: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Word not found.");
            }
        }


    }
}
