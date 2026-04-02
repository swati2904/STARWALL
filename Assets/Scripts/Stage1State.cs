using UnityEngine;

public class Stage1State : MonoBehaviour
{
    public bool BeaconActivated { get; private set; }
    public bool StageCompleted { get; private set; }

    [Header("Optional Objectives")]
    [SerializeField] private int scrapCacheTarget = 3;

    public int ScrapCachesCollected { get; private set; }
    public int ScrapCacheTarget => scrapCacheTarget;

    public bool ScrapCachesCompleted => ScrapCachesCollected >= scrapCacheTarget;

    public void CollectScrapCache(int amount = 1)
    {
        if (StageCompleted)
        {
            // Still allow collection for debugging, but we don't need to spam.
            return;
        }

        ScrapCachesCollected = Mathf.Clamp(ScrapCachesCollected + amount, 0, scrapCacheTarget);
        Debug.Log($"Scrap caches: {ScrapCachesCollected}/{scrapCacheTarget}");
    }

    public void ActivateBeacon()
    {
        if (BeaconActivated)
            return;

        BeaconActivated = true;
        Debug.Log("Primary Objective Updated: Beacon activated. Proceed to extraction.");
    }

    public void CompleteStage()
    {
        if (StageCompleted)
            return;

        StageCompleted = true;
        Debug.Log("Stage Complete! Extraction successful.");
    }
}

