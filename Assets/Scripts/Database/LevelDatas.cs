using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="LevelDatas", menuName = "Level/LevelDatas")]
public class LevelDatas : ScriptableObject
{
    public Level[] levels;
}
