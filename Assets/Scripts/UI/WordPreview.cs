using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordPreview : MonoBehaviour
{
    public static WordPreview Instance { get; private set; }

    private TMP_Text text;

    private void Awake()
    {
        Instance = this;
        text = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        Clear();
    }

    public void Add(char letter)
    {
        text.text += letter;
    }

    public void Clear()
    {
        text.text = string.Empty;
    }
}
