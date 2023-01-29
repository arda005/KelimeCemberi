using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordCreator : MonoBehaviour
{
    protected RectTransform rectTransform;

    #region UNITY_INSPECTOR
    [SerializeField] private GameObject letterPrefab;
    #endregion

    protected virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    protected virtual void Start()
    {
        Create();
    }

    protected virtual void Create()
    {
        Word currentWord = LevelManager.Instance.levelDatas.levels[0].word;

        foreach (Letter letter in currentWord.letters)
        {
            var createdObject = Instantiate(letterPrefab, rectTransform);
            createdObject.GetComponentInChildren<TMP_Text>().text = letter.letter.ToString().ToUpper();
        }
    }
}
