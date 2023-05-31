using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestData CurrentQuest;

    public static void SetQuest(QuestData data)
    {
        CurrentQuest = data;
        Debug.Log("Started Quest: " + CurrentQuest.title + "!" + "     Potential Reward: £" + CurrentQuest.rewardAmount);
    }

    public static void CompleteQuest()
    {
        //go to player manager or smth, set their current money
        //CurrentQuest.rewardAmount;

        Debug.Log("Completed Quest: " + CurrentQuest.title + "!" + "     Earned £" + CurrentQuest.rewardAmount);
    }

    public static void FailQuest()
    {
        Debug.Log("Failed Quest: " + CurrentQuest.title + "!" + "     Didn't Earn £" + CurrentQuest.rewardAmount);
    }
}
