using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordCreator : MonoBehaviour
{
    protected RectTransform rectTransform;

    protected Word currentWord;

    protected GameObject nestedWordPrefab;

    private WordCreatorLetter[] wordCreatorLetters;

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
        foreach (Letter letter in currentWord.letters)
        {
            bool isNested = letter.IsNested();

            if (isNested)
                CreateEmptyLetter(letter);
            else
                CreateLetter(letter);
        }

        wordCreatorLetters = GetComponentsInChildren<WordCreatorLetter>();
    }

    private void CreateLetter(Letter letter)
    {
        var createdObject = Instantiate(letterPrefab, rectTransform);
        var wordCreatorLetter = createdObject.GetComponent<WordCreatorLetter>();
        wordCreatorLetter.Init(letter);
    }

    protected virtual void CreateEmptyLetter(Letter letter)
    {
        var emptyLetter = Instantiate(letterSpacePrefab, rectTransform);
        var emptyLetterRect = emptyLetter.GetComponent<RectTransform>();

        var wordCreatorGameObj = Instantiate(nestedWordPrefab, emptyLetterRect);
        var wordCreatorRect = wordCreatorGameObj.GetComponent<RectTransform>();

        wordCreatorRect.anchoredPosition = Vector2.zero;

        var wordCreator = wordCreatorGameObj.GetComponent<WordCreator>();
        wordCreator.Init(letter.nestedWord, letter);
    }
}
