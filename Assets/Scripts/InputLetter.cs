using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputLetter : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    public RectTransform rectTransform { get; private set; }

    public char Letter { get; private set; } = 'A';

    private bool isUsed;

    private Image image;

    private InputCircle inputCircle;

    #region UNITY_INSPECTOR
    [SerializeField] private GameObject linePrefab;
    #endregion

    private static InputLetterLine currentLine;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    void Start()
    {
        inputCircle = InputCircle.Instance;

        SetUsed(false);
    }

    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        inputCircle.StartNewInput(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputCircle.ResetInput();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!inputCircle.IsInputActive || isUsed)
            return;

        inputCircle.AddLetter(this);
    }

    public void SetUsed(bool isUsed)
    {
        this.isUsed = isUsed;
        UpdateUI();
    }

    public static void SetUsedAll(bool isUsed)
    {
        var allLetters = InputCircle.Instance.InputLetters;

        foreach(var letter in allLetters)
        {
            letter.SetUsed(isUsed);
        }
    }

    public void UpdateUI()
    {
        SetColor();
    }

    public void CreateLine()
    {
        var createdObject = Instantiate(linePrefab, rectTransform.parent);
        var createdLine = createdObject.GetComponent<InputLetterLine>();
        createdLine.Init(this);

        if (currentLine != null)
        {
            currentLine.SetStatic(this);
        }
        currentLine = createdLine;
    }

    private void SetColor()
    {
        if (isUsed)
        {
            image.color = inputCircle.activeLetterColor;
        }
        else
        {
            image.color = inputCircle.deactiveLetterColor;
        }
    }

    public Color GetColor()
    {
        return image.color;
    }
}
