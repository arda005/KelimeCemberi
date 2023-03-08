using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Word
{
    public Letter[] letters;

    public int TotalCount(char letter)
    {
        int totalCount = 0;

        foreach (Letter element in letters)
        {
            if(element.letter == letter) totalCount++;
        }

        return totalCount;
    }

    public override string ToString()
    {
        var word = string.Empty;
        foreach (Letter element in letters)
        {
            word += element.letter;
        }

        return word;
    }
}
