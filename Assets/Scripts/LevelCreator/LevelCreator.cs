using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var createdObject = Instantiate(LevelManager.Instance.horizontalWordCreatorPrefab,
            GetComponent<RectTransform>());

        var wordCreator = createdObject.GetComponent<WordCreator>();
        wordCreator.Init(LevelManager.Instance.levelDatas.levels[0].word);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
