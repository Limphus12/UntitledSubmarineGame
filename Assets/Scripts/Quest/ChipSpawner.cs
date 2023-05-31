using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipSpawner : MonoBehaviour
{
    [SerializeField] private int chipsToSpawn;

    [SerializeField] private GameObject chipPrefab;

    [SerializeField] private Transform chipSpawnPoint;

    [SerializeField] private List<QuestData> questData = new List<QuestData>();

    [SerializeField] private List<GameObject> chips = new List<GameObject>();

    private void Start()
    {
        SpawnChips();
    }

    void SpawnChips()
    {
        if (!chipPrefab || chipsToSpawn == 0) return;

        for (int i = 0; i < chipsToSpawn; i++)
        {
            SpawnChip();
        }
    }

    void SpawnChip()
    {
        //spawn and add chip to list
        GameObject chip = Instantiate(chipPrefab, chipSpawnPoint.position, Quaternion.identity); chips.Add(chip);

        QuestChip questChip = chip.GetComponent<QuestChip>();

        if (!questChip) Debug.Log("No Quest Chip Found!");

        else if (questChip)
        {
            questChip.Data = questData[Random.Range(0, questData.Count)];
        }
    }

    public void SpawnChip(QuestData questData)
    {
        GameObject chip = Instantiate(chipPrefab, chipSpawnPoint.position, Quaternion.identity); chips.Add(chip);

        QuestChip questChip = chip.GetComponent<QuestChip>();

        if (!questChip) Debug.Log("No Quest Chip Found!");

        else if (questChip)
        {
            questChip.Data = questData;
        }
    }

    public void DeleteChips()
    {
        foreach (GameObject chip in chips)
        {
            if (chip != null) Destroy(chip);
        }

        chips.Clear();
    }
}
