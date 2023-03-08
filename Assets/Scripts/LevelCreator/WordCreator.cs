using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordCreator : MonoBehaviour
{
    protected RectTransform rectTransform;

    protected Word currentWord;

    protected GameObject nestedWordPrefab;

    public WordCreatorLetter[] wordCreatorLetters;

    public bool IsAnwered { get; private set; } = false;

    #region UNITY_INSPECTOR
    [SerializeField] private GameObject letterPrefab;
    [SerializeField] private GameObject letterSpacePrefab;
    #endregion

    protected virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public virtual void Init(Word word)
    {
        Init(word, null);
    }

    public virtual void Init(Word word, Letter parentLetter)
    {
        currentWord = word;
        CreateLetters();
        SetOffsetWithParent(parentLetter);
    }

    private void SetOffsetWithParent(Letter parentLetter)
    {
        if (parentLetter == null) return;

        var parentWordCreatorLetter = FindLetter(parentLetter);
        if (parentWordCreatorLetter == null) return;

        Canvas.ForceUpdateCanvases();

        var letterRect = parentWordCreatorLetter.GetComponent<RectTransform>();

        rectTransform.position += rectTransform.position - letterRect.position;
    }

    private WordCreatorLetter FindLetter(Letter letter)
    {
        return Array.Find(wordCreatorLetters, element => element.Letter.IsEqual(letter));
    }

    protected virtual void CreateLetters()
    {
        wordCreatorLetters = new WordCreatorLetter[currentWord.letters.Length];

        for (int i = 0; i < currentWord.letters.Length; i++)
        {
            var letter = currentWord.letters[i];
            bool isNested = letter.IsNested();

            if (isNested)
                wordCreatorLetters[i] = CreateEmptyLetter(letter);
            else
                wordCreatorLetters[i] = CreateLetter(letter);

            LevelManager.Instance.TryAddDifferentLetter(letter.letter, currentWord.TotalCount(letter.letter));
        }
    }

    private WordCreatorLetter CreateLetter(Letter letter)
    {
        var createdObject = Instantiate(letterPrefab, rectTransform);
        var createdRect = createdObject.GetComponent<RectTransform>();
        var wordCreatorLetter = createdObject.GetComponent<WordCreatorLetter>();
        wordCreatorLetter.Init(letter);

        return wordCreatorLetter;
    }

    protected virtual WordCreatorLetter CreateEmptyLetter(Letter letter)
    {
        var emptyLetter = Instantiate(letterSpacePrefab, rectTransform);
        var emptyLetterRect = emptyLetter.GetComponent<RectTransform>();

        var wordCreatorGameObj = Instantiate(nestedWordPrefab, emptyLetterRect);
        var wordCreatorRect = wordCreatorGameObj.GetComponent<RectTransform>();

        wordCreatorRect.anchoredPosition = Vector2.zero;

        var wordCreator = wordCreatorGameObj.GetComponent<WordCreator>();
        wordCreator.Init(letter.nestedWord, letter);

        return wordCreator.FindLetter(letter);
    }

    public void SetAnswered()
    {
        IsAnwered = true;

        foreach(var letter in wordCreatorLetters)
        {
            letter.SetVisibla(true);
        }
    }

    public string GetWord()
    {
        return currentWord.ToString();
    }
}
