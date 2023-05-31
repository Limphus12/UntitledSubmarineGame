using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest")]
public class QuestData : ScriptableObject
{
    public string title;
    public string description;

    public int rewardAmount;
}