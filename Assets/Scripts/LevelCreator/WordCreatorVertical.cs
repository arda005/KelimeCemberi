using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordCreatorVertical : WordCreator
{
    public override void Init(Word word, Letter parentLetter)
    {
        nestedWordPrefab = LevelManager.Instance.horizontalWordCreatorPrefab;
        base.Init(word, parentLetter);
    }
}