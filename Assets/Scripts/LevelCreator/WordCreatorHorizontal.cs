using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordCreatorHorizontal : WordCreator
{
    public override void Init(Word word, Letter parentLetter)
    {
        nestedWordPrefab = LevelManager.Instance.verticalWordCreatorPrefab;
        base.Init(word, parentLetter);
    }
}