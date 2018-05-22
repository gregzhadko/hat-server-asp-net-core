using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DictionaryService;
using HatServer.Models;
using JetBrains.Annotations;
using NHunspell;
using Utilities;
using Yandex.Speller.Api;
using Yandex.Speller.Api.DataContract;

namespace SpellChecker
{
    public sealed class SpellChecker
    {
        private readonly List<Pack> _packs;
        private readonly List<string> _skippedPhrases;
        private const char SpecialSymbol = '♆';
        private readonly OxfordService _oxfordService;

        public SpellChecker(List<Pack> packs, OxfordService service)
        {
            _oxfordService = service;
            _packs = packs;
            _skippedPhrases = File.ReadAllLines("SkipDictionary.txt").ToList();
        }

        public void Run()
        {
            var yandexSpellCheck = new YandexSpeller();

            using (var hunSpellEng = new Hunspell(@"Dictionaries\English (American).aff", @"Dictionaries\English (American).dic"))
            using (var hunSpell = new Hunspell(@"Dictionaries\Russian.aff", @"Dictionaries\Russian.dic"))
            {
                InitCustomDictionary(hunSpell);

                //Skip test pack with id 20
                foreach (var pack in _packs.Where(p => p.Id != 20))
                {
                    Console.WriteLine($"Checking pack {pack.Name}");
                    foreach (var phrase in pack.Phrases)
                    {
                        Console.WriteLine($"Working with phrase {phrase.Phrase}");
                        SpellPhrase(pack, phrase.Phrase, hunSpell, hunSpellEng, yandexSpellCheck);
                        SpellPhrase(pack, phrase.Description, hunSpell, hunSpellEng, yandexSpellCheck);
                    }
                }
            }

            Console.WriteLine("Spell check is finished");
        }

        private static void InitCustomDictionary(Hunspell hunSpell)
        {
            var customWords = File.ReadAllLines("CustomDictionary.txt");
            foreach (var line in customWords)
            {
                hunSpell.Add(line);
            }
        }

        private void SpellPhrase(Pack pack, string phrase, Hunspell hunSpell, Hunspell hunSpellEng, IYandexSpeller speller)
        {
            var words = StringUtilities.GetWordsFromString(phrase);
            foreach (var word in words.Select(w => w.ToLowerInvariant().Replace('ё', 'е')).Where(word =>
                !hunSpell.Spell(word) && !hunSpellEng.Spell(word) && !ExistsInSkipped(word, phrase, pack.Id)))
            {
                if (SeveralLanguages(word))
                {
                    ShowSpellErrorMessages(pack, phrase, word, "Несколько языков в слове");
                    HandleErrorWord(pack, word, phrase, hunSpell);
                }
                else if (YandexSpellCheckPass(speller, word) || OxfordSpellCheck(pack, word))
                {
                    SaveNewCustomWord(hunSpell, word);
                }
                else
                {
                    ShowSpellErrorMessages(pack, phrase, word);
                    HandleErrorWord(pack, word, phrase, hunSpell);
                }
            }
        }

        private bool OxfordSpellCheck([NotNull] Pack pack, string word) =>
            pack.Language == "en" && _oxfordService.DoesWordExist(word).GetAwaiter().GetResult();

        private static void HandleErrorWord(Pack pack, string word, string phrase, Hunspell hunSpell)
        {
            var key = Console.ReadKey();
            switch (key.KeyChar)
            {
                case 'y':
                case 'Y':
                case 'd':
                case 'D':
                    SaveNewCustomWord(hunSpell, word);
                    break;
                case 's':
                case 'S':
                    SaveNewSkipWord(word, phrase, pack.Id);
                    break;
            }

            Console.WriteLine("Работаем Дальше!");
        }

        private static bool SeveralLanguages([NotNull] string word)
        {
            var engMatches = Regex.Matches(word, @"[a-zA-Z]");
            if (engMatches.Count <= 0)
            {
                return false;
            }

            var rusMatches = Regex.Matches(word, @"[а-яА-Я]");
            return rusMatches.Count > 0;
        }

        private static bool YandexSpellCheckPass([NotNull] IYandexSpeller speller, string word) =>
            !speller.CheckText(word, Lang.Ru | Lang.En, Options.IgnoreCapitalization, TextFormat.Plain).Errors.Any();

        private static void ShowSpellErrorMessages([NotNull] Pack pack, string phrase, string word, string error = "Ошибка в слове")
        {
            var color = Console.ForegroundColor;
            Console.Write($"{DateTime.Now:hh:mm:ss}: {error} ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(word);
            Console.ForegroundColor = color;
            Console.Write(" из пака ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(pack.Name);
            Console.ForegroundColor = color;
            Console.Write(" Полная фраза: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(phrase);
            Console.ForegroundColor = color;
            Console.WriteLine(" Добавим слово в словарь или пропустим в конкретном случае? (d - dictionary, s - skip)");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(word);
            Console.WriteLine();
            Console.ForegroundColor = color;
        }

        private bool ExistsInSkipped(string word, [NotNull] string wholeWord, int id)
        {
            var formattedWholeWord = wholeWord.Replace('\n', SpecialSymbol);
            return _skippedPhrases.Select(s => s.Split('|')).Any(line => string.Compare(line[0], word, StringComparison.OrdinalIgnoreCase) == 0 &&
                                                                         string.Compare(line[1], id.ToString(), StringComparison.OrdinalIgnoreCase) == 0 &&
                                                                         string.Compare(line[2], formattedWholeWord, StringComparison.OrdinalIgnoreCase) == 0);
        }

        private static void SaveNewCustomWord([NotNull] Hunspell hunSpell, string word)
        {
            hunSpell.Add(word);
#if DEBUG
            File.AppendAllLines(@"..\..\CustomDictionary.txt", new[] {word});
#endif
            File.AppendAllLines(@"CustomDictionary.txt", new[] {word});
            ConsoleUtilities.WriteGreenLine($"\nСлово {word} было добавлено в персональный словарь");
        }

        private static void SaveNewSkipWord(string word, [NotNull] string wholeWord, int packId)
        {
            var formattedWholeWord = wholeWord.Replace('\n', SpecialSymbol);
            var stringToSave = $"{word}|{packId}|{formattedWholeWord}";
#if DEBUG
            File.AppendAllLines(@"..\..\SkipDictionary.txt", new[] {stringToSave});
#endif
            File.AppendAllLines(@"SkipDictionary.txt", new[] {stringToSave});
            ConsoleUtilities.WriteGreenLine($"\nСлово {word} было добавлено в словарь пропущенных слов");
        }
    }
}