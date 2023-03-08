using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private LevelDatas levelDatas;

    public Level CurrentLevel { get { return levelDatas.levels[0]; } }

    public readonly List<LetterData> differentLetters = new List<LetterData>();

    public bool IsGameEnd { get; private set; } = false;

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

    public bool TryWord(string word)
    {
        var wordCreators = FindObjectsOfType<WordCreator>();
        foreach(var wordCreator in wordCreators)
        {
            if (wordCreator.IsAnwered) continue;

            if (wordCreator.GetWord().ToLower() == word.ToLower())
            {
                wordCreator.SetAnswered();

                CheckGameEnd(wordCreators);
                return true;
            }
        }

        return false;
    }

    private void CheckGameEnd(WordCreator[] wordCreators)
    {
        int totalAnsweredWords = 0;

        foreach (var wordCreator in wordCreators)
        {
            if (wordCreator.IsAnwered)
                totalAnsweredWords++;
        }

        if (totalAnsweredWords < wordCreators.Length)
            return;

        IsGameEnd = true;
        Debug.Log("<color=green><b>You WON!</b></color>");
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
