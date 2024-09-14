using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TextAnalysis;

static class SentencesParserTask
{
    private static List<string> GetWordsFromSentence(string sentence)
    {

        List<string> words = new List<string>();
        bool isEnd;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < sentence.Length - 1; i++)
        {
            isEnd = false;
            if ((char.IsLetter(sentence[i]) || sentence[i] == '\'') && (char.IsLetter(sentence[i + 1]) || sentence[i + 1] == '\''))
            {
                sb.Append(sentence[i]);
            }
            else if ((char.IsLetter(sentence[i]) || sentence[i] == '\''))
            {
                sb.Append(sentence[i]);
                isEnd = true;
            }
            if (isEnd)
            {
                words.Add(sb.ToString().ToLower());
                sb.Clear();
            }

        }
        if (char.IsLetter(sentence[sentence.Length - 1]) || sentence[sentence.Length - 1] == '\'')
        {
            sb.Append(sentence[sentence.Length - 1]);
            words.Add(sb.ToString().ToLower());
        }

        return words;
    }
    private static bool HasWords(string sentence)
    {
        bool hasWords = false;

        for (int i = 0; i < sentence.Length; i++)
        {
            if (char.IsLetter(sentence[i]) || sentence[i] == '\'') hasWords = true;
        }
        return hasWords;
    }
    public static List<List<string>> ParseSentences(string text)
    {
        var sentencesList = new List<List<string>>();

        char[] delimiters = { '.', '!', '?', ';', ':', '(', ')' };
        var roughSentences = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < roughSentences.Length; i++)
        {
            if (HasWords(roughSentences[i]))
                sentencesList.Add(GetWordsFromSentence(roughSentences[i]));
        }

        return sentencesList;
    }
}