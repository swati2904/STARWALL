using UnityEngine;

public class Stage1State : MonoBehaviour
{
    public bool BeaconActivated { get; private set; }
    public bool StageCompleted { get; private set; }

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

