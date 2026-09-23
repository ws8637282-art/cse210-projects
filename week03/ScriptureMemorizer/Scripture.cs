using System;
using System.Collections.Generic;

namespace ScriptureMemorizer
{
    public class Scripture
    {
        private Reference _reference;
        private List<Word> _words;

        public Scripture(Reference reference, string text)
        {
            _reference = reference;
            _words = new List<Word>();

            string[] words = text.Split(' ');

            foreach (string word in words)
            {
                _words.Add(new Word(word));
            }
        }

        public string GetDisplayText()
        {
            string result = _reference.GetDisplayText() + "\n";

            foreach (Word word in _words)
            {
                result += word.GetDisplayText() + " ";
            }

            return result.Trim();
        }

        public void HideRandomWords(int numberOfWords)
        {
            Random random = new Random();

            List<Word> availableWords = new List<Word>();

            foreach (Word word in _words)
            {
                if (!word.IsHidden())
                {
                    availableWords.Add(word);
                }
            }

            int wordsToHide = Math.Min(numberOfWords, availableWords.Count);

            for (int i = 0; i < wordsToHide; i++)
            {
                int index = random.Next(availableWords.Count);

                availableWords[index].Hide();
                availableWords.RemoveAt(index);
            }
        }

        public bool AllWordsHidden()
        {
            foreach (Word word in _words)
            {
                if (!word.IsHidden())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
