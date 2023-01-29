using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Letter
{
    public char letter;
    public Word nestedWord;

    public bool IsNested()
    {
        if (nestedWord == null)
            return false;

        if(nestedWord.letters == null)
            return false;

        return nestedWord.letters.Length > 0;
    }

    public bool IsEqual(Letter letter)
    {
        return this.letter == letter.letter;
    }
}
