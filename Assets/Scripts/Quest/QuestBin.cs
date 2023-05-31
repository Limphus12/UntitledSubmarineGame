using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBin : MonoBehaviour
{
    [SerializeField] private ChipSpawner chipSpawner;

    public GameObject currentQuestChip;
    public QuestData currentQuestData;

    private void OnTriggerEnter(Collider other)
    {
        if (currentQuestChip && currentQuestData && chipSpawner) DenyQuest();

        QuestChip questChip = other.GetComponent<QuestChip>();

        if (!questChip) return;

        else if (questChip)
        {
            currentQuestData = questChip.Data;
            currentQuestChip = questChip.gameObject;
        }
    }

    public void AcceptQuest()
    {
        Debug.Log("Accepting Quest");
        QuestManager.SetQuest(currentQuestData);
        if (currentQuestChip) Destroy(currentQuestChip);
    }

    public void DenyQuest()
    {
        Debug.Log("Denying Quest");
        
        if (!chipSpawner || !currentQuestChip || !currentQuestData) return;
        if (currentQuestChip) Destroy(currentQuestChip);
        if (currentQuestData) chipSpawner.SpawnChip(currentQuestData);
    }
}
