using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public LevelDatas levelDatas;

    public readonly List<LetterData> differentLetters = new List<LetterData>();

    #region UNITY_INSPECTOR
    public GameObject horizontalWordCreatorPrefab;
    public GameObject verticalWordCreatorPrefab;
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TryAddDifferentLetter(char letter, int totalCount)
    {
        var currentData = differentLetters.Find(x => x.letter.Equals(letter));
        bool isContained = currentData != null;

        if (!isContained)
        {
            differentLetters.Add(new LetterData(letter));
        }
        else
        {
            if (currentData.count < totalCount) currentData.count++;
        }

        return isContained;
    }

    public int TotalDifferentLetterCount()
    {
        int totalCount = 0;

        foreach(var differentLetter in differentLetters)
            totalCount += differentLetter.count;

        return totalCount;
    }

    [System.Serializable]
    public class LetterData
    {
        public char letter;
        public int count;
        
        public LetterData(char letter, int count)
        {
            this.letter = letter;
            this.count = count;
        }

        public LetterData(char letter)
        {
            this.letter = letter;
            this.count = 1;
        }

        public LetterData()
        {
            this.count = 1;
        }
    }
}
