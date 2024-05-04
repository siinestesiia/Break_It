using System;
using UnityEngine;


[CreateAssetMenu (fileName = "Score System", menuName = "ScriptableObjects/ScoreSystem")]
public class ScoreSystem : ScriptableObject
{
    public int score = 0;
    public int lives = 3;
    public bool hasWon = false;
}
