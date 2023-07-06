using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, IMineable
{
    [Header("Attributes")]
    [SerializeField] private int mineAmount;
    [SerializeField] private float miningCooldown;

    [Space, SerializeField] private Vector2 pointMinMax;

    [Header("VFX")]
    [SerializeField] private GameObject gfx;

    [SerializeField] private GameObject hitParticles;
    [Space, SerializeField] private AnimationCurve[] miningHitCurves;

    [Header("SFX")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] hitClips;

    private bool canMine = true;

    private int currentMineAmount;

    private void Awake()
    {
        currentMineAmount = 0;
    }

    private void Update()
    {
        if (!canMine) SizeLerp();
        else EndSizeLerp();
    }

    public bool CanMine() => canMine;

    public void Mine()
    {
        MiningEffects();

        if (currentMineAmount >= mineAmount) EndMining();

        else StartCoroutine(MiningWaitTimer());
    }

    private IEnumerator MiningWaitTimer()
    {
        canMine = false;

        yield return new WaitForSeconds(miningCooldown);

        currentMineAmount++;

        canMine = true;
    }

    private void EndMining()
    {
        GameManager.ModifyPoints((int)Random.Range(pointMinMax.x, pointMinMax.y));

        Destroy(gameObject);
    }

    private void MiningEffects()
    {
        if (hitParticles)
        {
            GameObject particle = Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(particle, 1f);
        }

        if (audioSource && hitClips.Length > 0)
        {
            audioSource.PlayOneShot(hitClips[Random.Range(0, hitClips.Length)]);
        }
    }

    float sizeI = 0;

    private void SizeLerp()
    {
        sizeI += Time.deltaTime;

        float i = miningHitCurves[currentMineAmount].Evaluate(sizeI);

        gfx.transform.localScale = new Vector3(i, i, i);
    }

    private void EndSizeLerp()
    {
        sizeI = 0;
    }
}