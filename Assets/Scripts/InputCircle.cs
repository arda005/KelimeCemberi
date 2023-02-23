using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCircle : MonoBehaviour
{
    public static InputCircle Instance { get; private set; }

    private RectTransform rectTransform;
    public bool IsInputActive { get; private set; }

    public string CurrentWord { get; private set; }

    private InputLetter previousLetter;
    public InputLetter[] InputLetters { get; private set; }

    #region UNITY_INSPECTOR
    [SerializeField] private RectTransform rotationHandler;

    public Color activeLetterColor;
    public Color deactiveLetterColor;

    [SerializeField] private InputLetter InputLetterPrefab;
    #endregion

    private void Awake()
    {
        Instance = this;
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        //Init();
    }

    void Update()
    {
        
    }

    public void Init()
    {
        InputLetters = CreateInputLetters();
    }

    private InputLetter[] CreateInputLetters()
    {
        var differenLetters = LevelManager.Instance.differentLetters;
        var letters = new InputLetter[differenLetters.Count];
        var degreePerLetter = 360 / (differenLetters.Count);

        for (int i = 0; i < letters.Length; i++)
        {
            letters[i] = Instantiate(InputLetterPrefab, rotationHandler);
            rotationHandler.Rotate(Vector3.forward * degreePerLetter);
            letters[i].rectTransform.SetParent(rectTransform);
            letters[i].rectTransform.localEulerAngles = Vector3.zero;

            letters[i].Init(differenLetters[i]);
        }

        return letters;
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
