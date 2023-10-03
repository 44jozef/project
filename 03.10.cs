using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class DictionaryEntry
{
    public string Word { get; set; }
    public List<string> Translations { get; set; }
}

class Dictionary
{
    public string LanguagePair { get; set; }
    public List<DictionaryEntry> Entries { get; set; }

    public Dictionary(string languagePair)
    {
        LanguagePair = languagePair;
        Entries = new List<DictionaryEntry>();
    }
}

class Program
{
    static List<Dictionary> dictionaries = new List<Dictionary>();
    static string currentDictionary = "";

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("1. Создать словарь");
            Console.WriteLine("2. Выбрать словарь");
            Console.WriteLine("3. Добавить слово и перевод");
            Console.WriteLine("4. Заменить слово или перевод");
            Console.WriteLine("5. Удалить слово или перевод");
            Console.WriteLine("6. Искать перевод слова");
            Console.WriteLine("7. Экспортировать в файл");
            Console.WriteLine("8. Выход");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    CreateDictionary();
                    break;
                case 2:
                    SelectDictionary();
                    break;
                case 3:
                    AddWordAndTranslation();
                    break;
                case 4:
                    ReplaceWordOrTranslation();
                    break;
                case 5:
                    DeleteWordOrTranslation();
                    break;
                case 6:
                    SearchTranslation();
                    break;
                case 7:
                    ExportToFile();
                    break;
                case 8:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }

    static void CreateDictionary()
    {
        Console.Write("Введите тип словаря (например, англо-русский): ");
        string languagePair = Console.ReadLine();
        dictionaries.Add(new Dictionary(languagePair));
        Console.WriteLine($"Словарь '{languagePair}' создан.");
    }

    static void SelectDictionary()
    {
        Console.WriteLine("Выберите словарь:");
        for (int i = 0; i < dictionaries.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {dictionaries[i].LanguagePair}");
        }
        int choice = int.Parse(Console.ReadLine());
        if (choice >= 1 && choice <= dictionaries.Count)
        {
            currentDictionary = dictionaries[choice - 1].LanguagePair;
            Console.WriteLine($"Выбран словарь '{currentDictionary}'.");
        }
        else
        {
            Console.WriteLine("Неверный выбор.");
        }
    }

    static void AddWordAndTranslation()
    {
        if (string.IsNullOrEmpty(currentDictionary))
        {
            Console.WriteLine("Сначала выберите словарь.");
            return;
        }

        var dictionary = dictionaries.FirstOrDefault(d => d.LanguagePair == currentDictionary);

        Console.Write("Введите слово: ");
        string word = Console.ReadLine();
        Console.Write("Введите перевод(ы), разделяя их запятыми: ");
        string translationsInput = Console.ReadLine();
        var translations = translationsInput.Split(',').Select(t => t.Trim()).ToList();

        var entry = dictionary.Entries.FirstOrDefault(e => e.Word == word);
        if (entry == null)
        {
            entry = new DictionaryEntry { Word = word, Translations = translations };
            dictionary.Entries.Add(entry);
        }
        else
        {
            entry.Translations.AddRange(translations);
        }

        Console.WriteLine($"Слово '{word}' добавлено в словарь '{currentDictionary}'.");
    }

    static void ReplaceWordOrTranslation()
    {
        if (string.IsNullOrEmpty(currentDictionary))
        {
            Console.WriteLine("Сначала выберите словарь.");
            return;
        }

        var dictionary = dictionaries.FirstOrDefault(d => d.LanguagePair == currentDictionary);

        Console.Write("Введите слово, которое нужно заменить: ");
        string wordToReplace = Console.ReadLine();
        Console.Write("Введите новое слово: ");
        string newWord = Console.ReadLine();
        Console.Write("Введите новый перевод(ы), разделяя их запятыми: ");
        string newTranslationsInput = Console.ReadLine();
        var newTranslations = newTranslationsInput.Split(',').Select(t => t.Trim()).ToList();

        var entry = dictionary.Entries.FirstOrDefault(e => e.Word == wordToReplace);
        if (entry != null)
        {
            entry.Word = newWord;
            entry.Translations = newTranslations;
            Console.WriteLine($"Слово '{wordToReplace}' заменено в словаре '{currentDictionary}'.");
        }
        else
        {
            Console.WriteLine($"Слово '{wordToReplace}' не найдено в словаре '{currentDictionary}'.");
        }
    }

    static void DeleteWordOrTranslation()
    {
        if (string.IsNullOrEmpty(currentDictionary))
        {
            Console.WriteLine("Сначала выберите словарь.");
            return;
        }

        var dictionary = dictionaries.FirstOrDefault(d => d.LanguagePair == currentDictionary);

        Console.Write("Введите слово или перевод, который нужно удалить: ");
        string itemToDelete = Console.ReadLine();

        var entry = dictionary.Entries.FirstOrDefault(e => e.Word == itemToDelete);
        if (entry != null)
        {
            dictionary.Entries.Remove(entry);
            Console.WriteLine($"Слово '{itemToDelete}' удалено из словаря '{currentDictionary}'.");
        }
        else
        {
            foreach (var dictEntry in dictionary.Entries)
            {
                if (dictEntry.Translations.Contains(itemToDelete))
                {
                    dictEntry.Translations.Remove(itemToDelete);
                    Console.WriteLine($"Перевод '{itemToDelete}' удален из словаря '{currentDictionary}'.");
                    break;
                }
            }
            Console.WriteLine($"'{itemToDelete}' не найдено в словаре '{currentDictionary}'.");
        }
    }

    static void SearchTranslation()
    {
        if (string.IsNullOrEmpty(currentDictionary))
        {
            Console.WriteLine("Сначала выберите словарь.");
            return;
        }

        var dictionary = dictionaries.FirstOrDefault(d => d.LanguagePair == currentDictionary);

        Console.Write("Введите слово для поиска перевода: ");
        string wordToSearch = Console.ReadLine();

        var entry = dictionary.Entries.FirstOrDefault(e => e.Word == wordToSearch);
        if (entry != null)
        {
            Console.WriteLine($"Перевод слова '{wordToSearch}' в словаре '{currentDictionary}':");
            foreach (var translation in entry.Translations)
            {
                Console.WriteLine(translation);
            }
        }
        else
        {
            Console.WriteLine($"Слово '{wordToSearch}' не найдено в словаре '{currentDictionary}'.");
        }
    }

    static void ExportToFile()
    {
        if (string.IsNullOrEmpty(currentDictionary))
        {
            Console.WriteLine("Сначала выберите словарь.");
            return;
        }

        var dictionary = dictionaries.FirstOrDefault(d => d.LanguagePair == currentDictionary);

        Console.Write("Введите имя файла для экспорта: ");
        string fileName = Console.ReadLine();

        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (var entry in dictionary.Entries)
            {
                writer.WriteLine($"Слово: {entry.Word}");
                writer.WriteLine("Перевод(ы):");
                foreach (var translation in entry.Translations)
                {
                    writer.WriteLine(translation);
                }
                writer.WriteLine();
            }
        }

        Console.WriteLine($"Словарь '{currentDictionary}' экспортирован в файл '{fileName}'.");
    }
}
