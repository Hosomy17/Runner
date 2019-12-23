using System.Collections.Generic;
using UnityEngine;

public class CustomRandom
{
    private Dictionary<string, int> indexes;
    private int totalFactor;

    public CustomRandom()
    {
        indexes = new Dictionary<string, int>();
        totalFactor = 0;
    }

    //Add and edit index
    public void NewIndex(string index, int factor)
    {
        if (factor < 0)
            factor = 0;

        if(!indexes.ContainsKey(index))
        {
            totalFactor += factor;
            indexes.Add(index, factor);
        }
        else
        {
            totalFactor -= indexes[index];
            totalFactor += factor;
            indexes[index] = factor;
        }
    }

    public float IndexChance(string index)
    {
        return indexes[index] / (float) totalFactor;
    }

    public int IndexFactor(string index)
    {
        return indexes[index];
    }

    public int GetTotalFactor()
    {
        return totalFactor;
    }

    public int GetTotalIndex()
    {
        return indexes.Count;
    }

    public string Generate()
    {
        if(totalFactor == 0)
            return null;

        var n = Random.Range(1, totalFactor);
        var index = indexes.GetEnumerator();
        do
        {
            index.MoveNext();
            n -= index.Current.Value;
        } while (n > 0);

        return index.Current.Key;
    }
}
