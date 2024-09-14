namespace TextAnalysis;

static class FrequencyAnalysisTask
{
    private static void GetBigrammsFrequency(List<string> words, Dictionary<string, Dictionary<string, int>> frequency)
    {
        for (int i = 0; i < words.Count - 1; i++)
        {
            if (!frequency.ContainsKey(words[i]))
            {
                frequency[words[i]] = new Dictionary<string, int>();
                frequency[words[i]][words[i + 1]] = 1; 

            }
            else
            {
                if (!frequency[words[i]].ContainsKey(words[i + 1]))
                    frequency[words[i]][words[i + 1]] = 1;

                else frequency[words[i]][words[i + 1]]++;
            }
        }
    }
    private static void GetTrigrammsFrequency(List<string> words, Dictionary<string, Dictionary<string, int>> frequency)
    {
        for (int i = 0; i < words.Count - 2; i++)
        {
            var startKey = words[i] + " " + words[i+1];
            if (!frequency.ContainsKey(startKey))
            {
                frequency[startKey] = new Dictionary<string, int>();
                frequency[startKey][words[i + 2]] = 1;
            }
            else
            {
                if (!frequency[startKey].ContainsKey(words[i + 2]))
                    frequency[startKey][words[i + 2]] = 1;

                else frequency[startKey][words[i + 2]]++;
            }
        }
    }
    private static string GetValue(Dictionary<string, int> values)
    {
        var sortedValues = from entry in values orderby entry.Value descending select entry;
        var maxValues = new List<string>();
        int previousFrequency = 0;
        foreach (var value in sortedValues)
        {
            previousFrequency = value.Value;
            break;
        }
        foreach (var value in sortedValues)
        {
            if (value.Value == previousFrequency)
            {
                maxValues.Add(value.Key);
            }
            else break;
            previousFrequency = value.Value;
        }
        string result = maxValues[0];
        foreach(var value in maxValues)
        {
            if (string.CompareOrdinal(value, result) < 0) result = value;
        }
        return result;

    }
    private static void FrequencyIntoResult(Dictionary<string, Dictionary<string, int>> frequency, Dictionary<string, string> result)
    {
        foreach (var key in frequency.Keys)
        {
            result.Add(key, GetValue(frequency[key]));
        }
    }
    public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
    {
        var result = new Dictionary<string, string>();
        var frequency = new Dictionary<string, Dictionary<string, int>>();
        foreach (var words in text)
        {
            if (words.Count > 1)
            {
                GetBigrammsFrequency(words, frequency);
            }
            if (words.Count > 2)
            {
                GetTrigrammsFrequency(words, frequency);
            }
        }
        FrequencyIntoResult(frequency, result);

        return result;
    }
}