using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        var createdObject = Instantiate(LevelManager.Instance.horizontalWordCreatorPrefab,
            GetComponent<RectTransform>());

        var wordCreator = createdObject.GetComponent<WordCreator>();
        wordCreator.Init(LevelManager.Instance.levelDatas.levels[0].word);

        SetCenterPoint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetCenterPoint()
    {
        var endPoints = CalculateEndPoints();

        Vector3 topPoint = endPoints.Item1;
        Vector3 bottomPoint = endPoints.Item2;
        Vector3 rightPoint = endPoints.Item3;
        Vector3 leftPoint = endPoints.Item4;

        Vector3 centerPoint = new Vector3((rightPoint.x + leftPoint.x) / 2f, (topPoint.y + bottomPoint.y) / 2f);
        var diff = centerPoint - rectTransform.position;
        rectTransform.position = rectTransform.position - diff;
    }

    private (Vector3, Vector3, Vector3, Vector3) CalculateEndPoints()
    {
        Canvas.ForceUpdateCanvases();

        var allLetters = GetComponentsInChildren<WordCreatorLetter>();

        Vector3 topPoint = allLetters[0].rectTransform.position;
        Vector3 bottomPoint = allLetters[0].rectTransform.position;
        Vector3 rightPoint = allLetters[0].rectTransform.position;
        Vector3 leftPoint = allLetters[0].rectTransform.position;

        for (int i = 1; i < allLetters.Length; i++)
        {
            var currentLetterPosition = allLetters[i].rectTransform.position;

            //Vertical Control
            if (topPoint.y < currentLetterPosition.y)
            {
                topPoint = currentLetterPosition;
            }
            else if (bottomPoint.y > currentLetterPosition.y)
            {
                bottomPoint = currentLetterPosition;
            }

            //Horizontal Control
            if (rightPoint.x < currentLetterPosition.x)
            {
                rightPoint = currentLetterPosition;
            }
            else if (leftPoint.x > currentLetterPosition.x)
            {
                leftPoint = currentLetterPosition;
            }
        }

        return (topPoint, bottomPoint, rightPoint, leftPoint);
    }
}
