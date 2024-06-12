using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerScore
{
    public string playerName;
    public int score;
}

[System.Serializable]
public class ScoreList
{
    public List<PlayerScore> scores;
}
