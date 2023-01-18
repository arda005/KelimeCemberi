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

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
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

        Debug.Log(CurrentWord);

        inputLetter.CreateLine();
        previousLetter = inputLetter;
    }

    public void ResetInput()
    {
        IsInputActive = false;
        CurrentWord = string.Empty;
        previousLetter = null;

        InputLetter.SetUsedAll(false);
        InputLetterLine.DeleteAllLines();
    }
}
