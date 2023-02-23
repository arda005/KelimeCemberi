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

    public void Init()
    {
        InputLetters = CreateInputLetters();
        ShuffleLetters();
    }

    private InputLetter[] CreateInputLetters()
    {
        var differenLetters = LevelManager.Instance.differentLetters;
        int totalDifferentLetterCount = LevelManager.Instance.TotalDifferentLetterCount();
        var letters = new InputLetter[totalDifferentLetterCount];
        var degreePerLetter = 360 / (totalDifferentLetterCount);

        int currentIndex = 0;
        for (int i = 0; i < differenLetters.Count; i++)
        {
            var differentLetter = differenLetters[i];
            for (int j = 0; j < differentLetter.count; j++)
            {
                letters[currentIndex] = Instantiate(InputLetterPrefab, rotationHandler);
                rotationHandler.Rotate(Vector3.forward * degreePerLetter);
                letters[currentIndex].rectTransform.SetParent(rectTransform);
                letters[currentIndex].rectTransform.localEulerAngles = Vector3.zero;

                letters[currentIndex].Init(differentLetter.letter);

                currentIndex++;
            }
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

    private void ShuffleLetters()
    {
        for (int i = 0; i < InputLetters.Length; i++)
        {
            var currentLetterPos = InputLetters[i].rectTransform.position;

            var randomLetter = InputLetters[Random.Range(0, InputLetters.Length)];

            InputLetters[i].rectTransform.position = randomLetter.rectTransform.position;
            randomLetter.rectTransform.position = currentLetterPos;
        }
    }
}
