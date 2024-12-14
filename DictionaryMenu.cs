using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1ExamCSharpp
{
    public class DictionaryMenu
    {
        DictionaryManager dictionary = new DictionaryManager();

        public DictionaryMenu(DictionaryManager customDictionary)
        {
            this.dictionary = customDictionary;
        }

        public void StartDictionaryMenu(string dictionaryType)
        {
            Console.Clear();
            Console.WriteLine("================ Dictionary MENU ================\n");
            Console.WriteLine("1 ) Add Word / Translation\n");
            Console.WriteLine("2 ) Remove Word / Translation\n");
            Console.WriteLine("3 ) Replace Word / Translation\n");
            Console.WriteLine("4 ) Search Word\n");
            Console.WriteLine("5 ) Show All Words\n");
            Console.WriteLine("6 ) Export Word\n");
            Console.WriteLine("0 ) Back \n");
            Console.WriteLine("==================================================\n");


            Console.Write("Enter Opt : ");
            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1: AddWordTranslation(dictionaryType); break;
                case 2: RemoveWordTranslation(dictionaryType); break;
                case 3: ReplaceWordTranslation(dictionaryType); break;
                case 4: SearchWord(dictionaryType); break;
                case 5: ShowAllWords(dictionaryType); break;
                case 6: ExportWord(dictionaryType); break;
                case 0: Console.Clear(); Console.WriteLine(); ; break;
                default: StartDictionaryMenu(dictionaryType); break;
            }
        }

        public void AddWordTranslation(string dictionaryType)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("================ Add Word / Translation ================\n");
                Console.WriteLine("1 ) Add Word With Translations\n");
                Console.WriteLine("2 ) Add Translations\n");
                Console.WriteLine("0 ) Back\n");
                Console.WriteLine("========================================================\n");

                Console.Write("Enter Opt : ");
                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1: AddWordAndTranslations(dictionaryType); break;
                    case 2: AddTranslations(dictionaryType); break;
                    case 0: StartDictionaryMenu(dictionaryType); break;
                    default: AddWordTranslation(dictionaryType); break;
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); StartDictionaryMenu(dictionaryType); }
        }

        public void AddWordAndTranslations(string dictionaryType)
        {
            dictionary.AddWord(dictionaryType);
            Console.ReadLine();
            AddWordTranslation(dictionaryType);
        }

        public void AddTranslations(string dictionaryType)
        {
            dictionary.AddTranslation(dictionaryType);
            Console.ReadLine();
            AddWordTranslation(dictionaryType);
        }

        public void RemoveWordTranslation(string dictionaryType)
        {
            Console.Clear();
            Console.WriteLine("================ Remove Word / Translation ================\n");
            Console.WriteLine("1 ) Remove Word With Translations\n");
            Console.WriteLine("2 ) Remove Translations\n");
            Console.WriteLine("0 ) Back\n");
            Console.WriteLine("===========================================================\n");

            Console.Write("Enter Opt: ");
            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1: RemoveWord(dictionaryType); break;
                case 2: RemoveTranslation(dictionaryType); break;
                case 0: StartDictionaryMenu(dictionaryType); break;
                default: RemoveWordTranslation(dictionaryType); break;
            }
        }

        public void RemoveWord(string dictionaryType)
        {
            dictionary.RemoveWord(dictionaryType);
            Console.ReadLine();
            RemoveWordTranslation(dictionaryType);
        }

        public void RemoveTranslation(string dictionaryType)
        {
            dictionary.RemoveTranslation(dictionaryType);
            Console.ReadLine();
            RemoveWordTranslation(dictionaryType);
        }

        public void ReplaceWordTranslation(string dictionaryType)
        {
            Console.Clear();
            Console.WriteLine("================ Replace Word / Translation ================\n");
            Console.WriteLine("1 ) Replace Word\n");
            Console.WriteLine("2 ) Replace Translations\n");
            Console.WriteLine("0 ) Back\n");
            Console.Write("Enter Opt: ");
            Console.WriteLine("============================================================\n");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1": ReplaceWord(dictionaryType); break;
                case "2": ReplaceTranslation(dictionaryType); break;
                case "0": StartDictionaryMenu(dictionaryType); break;
                default: ReplaceWordTranslation(dictionaryType); break;
            }
        }

        public void ReplaceWord(string dictionaryType)
        {
            dictionary.ReplaceWord(dictionaryType);
            Console.ReadLine();
            ReplaceWordTranslation(dictionaryType);
        }

        public void ReplaceTranslation(string dictionaryType)
        {
            dictionary.ReplaceTranslation(dictionaryType);
            Console.ReadLine();
            ReplaceWordTranslation(dictionaryType);
        }

        public void SearchWord(string dictionaryType)
        {
            Console.Write("\nSearch word: ");
            string key = Console.ReadLine();

            dictionary.SearchWord(key, dictionaryType);
            Console.ReadLine();
            StartDictionaryMenu(dictionaryType);
        }

        public void ShowAllWords(string dictionaryType)
        {
            Console.Clear();
            Console.WriteLine("================  All Words In [ {0} ] ================ ", dictionary.DictionaryType);
            dictionary.ViewAllWords(dictionaryType);
            Console.WriteLine("=======================================================\n");

            Console.ReadLine();
            StartDictionaryMenu(dictionaryType);
        }

        public void ExportWord(string dictionaryType)
        {
            dictionary.exportWord(dictionaryType);
            Console.ReadLine();
            StartDictionaryMenu(dictionaryType);
        }

    }
}
