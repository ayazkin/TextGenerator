using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using System.ComponentModel.Design;
using System.Text;

namespace TextAnalysis;


static class TextGeneratorTask
{
    private static string GetOneWordKey(string phraseBegining)
    {
        var result = new StringBuilder();
        for (int i = phraseBegining.Length - 1; i >= 0; i--)
        {
            if (phraseBegining[i] != ' ') result.Insert(0, phraseBegining[i]);
            else break;
        }
        return result.ToString() ;
    }
    private static string GetTwoWordKey(string phraseBegining)
    {
        var result = new StringBuilder();
        int count = 0;
        for (int i = phraseBegining.Length - 1; i >= 0; i--)
        {
            if (phraseBegining[i] == ' ') count++;
            if (count < 2) result.Insert(0, phraseBegining[i]);
            else break;
        }
        return result.ToString();
    }
    private static string OneKeyContinue(Dictionary<string, string> nextWords, string phraseBeginning)
    {
        string value;
        if (nextWords.TryGetValue(GetOneWordKey(phraseBeginning), out value)) phraseBeginning += " " + value;
        return phraseBeginning;
    }
    private static string TwoKeyContinue(Dictionary<string, string> nextWords, string phraseBeginning)
    {

        string value;
        if (nextWords.TryGetValue(GetTwoWordKey(phraseBeginning), out value)) phraseBeginning += " " + value;
        else if (nextWords.TryGetValue(GetOneWordKey(phraseBeginning), out value)) phraseBeginning += " " + value;

        return phraseBeginning;
    }
    public static string ContinuePhrase(
        Dictionary<string, string> nextWords,
        string phraseBeginning,
        int wordsCount)
    {
        for (int i = 0; i < wordsCount; i++)
        {
            if (phraseBeginning.Contains(' '))
                phraseBeginning = TwoKeyContinue(nextWords, phraseBeginning);
            else
                phraseBeginning = OneKeyContinue(nextWords, phraseBeginning);
        }
        return phraseBeginning;
    }
}