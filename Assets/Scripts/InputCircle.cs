using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCircle : MonoBehaviour
{
    public static InputCircle Instance { get; private set; }

    public bool IsInputActive { get; private set; }

    public string CurrentWord { get; private set; }

    private InputLetter previousLetter;
    public InputLetter[] InputLetters { get; private set; }

    #region UNITY_INSPECTOR
    public Color activeLetterColor;
    public Color deactiveLetterColor;
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }

    public void Init()
    {
        InputLetters = FindObjectsOfType<InputLetter>();
    }

    public void StartNewInput(InputLetter inputLetter)
    {
        IsInputActive = true;
        AddLetter(inputLetter);
    }

    public void AddLetter(InputLetter inputLetter)
    {
        CurrentWord += inputLetter.Letter;
        inputLetter.SetUsed(true);

        inputLetter.CreateLine();
        previousLetter = inputLetter;

        WordPreview.Instance.Add(inputLetter.Letter);
    }

    public void ResetInput()
    {
        IsInputActive = false;
        CurrentWord = string.Empty;
        previousLetter = null;

        InputLetter.SetUsedAll(false);
        InputLetterLine.DeleteAllLines();
        WordPreview.Instance.Clear();
    }
}
