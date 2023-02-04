using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordCreatorLetter : MonoBehaviour
{
    public RectTransform rectTransform { get; private set; }
    public Letter Letter { get; private set; }

    private TMP_Text letterText;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        letterText = GetComponentInChildren<TMP_Text>();
    }

    public void Init(Letter  letter)
    {
        Letter = letter;
        UpdateUI();
    }

    public void UpdateUI()
    {
        letterText.text = Letter.letter.ToString().ToUpper();
    }
}
