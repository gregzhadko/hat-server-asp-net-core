using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Utilities;

namespace UtilitiesTests
{
    [TestFixture]
    public class StringUtilitiesTests
    {
        [Test]
        [TestCase("Лос-Анджелес", ExpectedResult = 1)]
        [TestCase("Нью-Йорк", ExpectedResult = 1)]
        [TestCase("Лос-Анджелес Нью-Йорк", ExpectedResult = 2)]
        [TestCase("Лос-Анджелес Нью-Йорк Нью-Йорк", ExpectedResult = 3)]
        public int GetWordsFromString_WordsWithHypens_CorrectNumberOfWords(string original)
        {
            var words = StringUtilities.GetWordsFromString(original);
            return words.Count();
        }
        
        [Test]
        [TestCase("Тест", ExpectedResult = 1)]
        [TestCase("Тест тест", ExpectedResult = 2)]
        [TestCase("Тест     Тест    Тест", ExpectedResult = 3)]
        public int GetWordsFromString_Words_CorrectNumberOfWords(string original)
        {
            var words = StringUtilities.GetWordsFromString(original);
            return words.Count();
        }

        [Test]
        [TestCaseSource(typeof(GetWordsFromStringTestCaseSources), nameof(GetWordsFromStringTestCaseSources.SimpleWordsTest))]
        public void GetWordsFromString_SimpleWords_CorrectResults(string original, List<string> expectedWords)
        {
            var words = StringUtilities.GetWordsFromString(original);
            CollectionAssert.AreEquivalent(expectedWords, words);
        }
        
        [Test]
        [TestCaseSource(typeof(GetWordsFromStringTestCaseSources), nameof(GetWordsFromStringTestCaseSources.WordsWithPunctuation))]
        public void GetWordsFromString_WordWithIsPunctuation_CorrectResults(string original, List<string> expectedWords)
        {
            var words = StringUtilities.GetWordsFromString(original);
            CollectionAssert.AreEquivalent(expectedWords, words);
        }
    }

    internal class GetWordsFromStringTestCaseSources
    {
        internal static object[] SimpleWordsTest =
        {
            new object[] {"Test", new List<string> {"Test"}},
            new object[] {"Test    ", new List<string> {"Test"}},
            new object[] {"    Test", new List<string> {"Test"}},
            new object[] {"Test Test ", new List<string> {"Test", "Test"}},
            new object[] {"Test   Test  s Test", new List<string> {"Test", "Test", "s", "Test"}},
        };

        internal static object[] WordsWithPunctuation =
        {
            new object[]
            {
                "'Oh, you can't help that,' said the Cat: 'we're all mad here. I'm mad. You're mad.'",
                new List<string>
                {
                    "Oh",
                    "you",
                    "can't",
                    "help",
                    "that",
                    "said",
                    "the",
                    "Cat",
                    "we're",
                    "all",
                    "mad",
                    "here",
                    "I'm",
                    "mad",
                    "You're",
                    "mad"
                },
            }
        };
    }
}