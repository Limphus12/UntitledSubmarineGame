using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, IMineable
{
    [SerializeField] private int mineAmount;
    [SerializeField] private float miningCooldown;

    [Space, SerializeField] private Vector2 pointMinMax;

    [Space, SerializeField] private AnimationCurve[] miningHitCurves;

    private bool canMine = true;

    private int currentMineAmount;

    private void Awake()
    {
        currentMineAmount = 0;
    }

    public bool CanMine() => canMine;

    public void Mine()
    {
        MiningEffects();

        currentMineAmount++;

        //do particle effects
        //do sound effects
        //add points (randomize amount)

        if (currentMineAmount >= mineAmount) EndMining();

        else StartCoroutine(MiningWaitTimer());
    }

    private void MiningEffects()
    {

    }

    private void EndMining()
    {
        Destroy(gameObject);
    }

    private IEnumerator MiningWaitTimer()
    {
        canMine = false;

        yield return new WaitForSeconds(miningCooldown);

        canMine = true;
    }
}
