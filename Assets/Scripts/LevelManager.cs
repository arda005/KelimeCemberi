using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public LevelDatas levelDatas;

    private List<char> differentLetters = new List<char>();

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

    public bool TryAddDifferentLetter(char letter)
    {
        bool isContained = differentLetters.Contains(letter);

        if(!isContained) differentLetters.Add(letter);

        return isContained;
    }
}
