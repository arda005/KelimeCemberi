using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputLetterLine : MonoBehaviour
{
    private RectTransform rectTransform;
    private Image image;

    private InputLetter firstLetter;
    private InputLetter secondLetter;

    private bool IsStatic { get { return secondLetter != null; } }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    public void Init(InputLetter firstLetter)
    {
        this.firstLetter = firstLetter;
        secondLetter = null;

        rectTransform.SetAsFirstSibling();

        UpdateUI();
    }

    public void SetStatic(InputLetter secondLetter)
    {
        this.secondLetter = secondLetter;
        UpdateUI();
    }

    void Update()
    {
        if (!IsStatic)
        {
            SetRotation();
            SetPosition();
            SetSize();
        }
    }

    public void UpdateUI()
    {
        SetRotation();
        SetPosition();
        SetSize();
        SetColor();
    }

    private void SetSize()
    {
        rectTransform.sizeDelta = new Vector2(CalculateDistance(), rectTransform.sizeDelta.y);
    }

    private void SetPosition()
    {
        rectTransform.position = firstLetter.rectTransform.position;
    }

    private void SetRotation()
    {
        rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, CalculateAngle()));
    }

    private float CalculateDistance()
    {
        return Vector3.Distance(firstLetter.rectTransform.position,
            GetSecondPosition()) / rectTransform.lossyScale.x;
    }

    private float CalculateAngle()
    {
        Vector3 dir = GetSecondPosition() - firstLetter.rectTransform.position;
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    public static void DeleteAllLines()
    {
        var lines = FindObjectsOfType<InputLetterLine>();
        
        foreach (var line in lines)
        {
            Destroy(line.gameObject);
        }
    }
    
    private void SetColor()
    {
        image.color = firstLetter.GetColor();
    }

    private Vector3 GetSecondPosition()
    {
        if (IsStatic)
        {
            return secondLetter.rectTransform.position;
        }
        else
        {
            if (Input.touches.Length <= 0)
            {
                return Input.mousePosition;
            }
            else
            {
                return Input.touches[0].position;
            }
        }
    }
}
