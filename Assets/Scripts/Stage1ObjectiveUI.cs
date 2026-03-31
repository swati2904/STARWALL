using UnityEngine;
using TMPro;

public class Stage1ObjectiveUI : MonoBehaviour
{
    [SerializeField] private Stage1State stage1State;
    [SerializeField] private TextMeshProUGUI objectiveText;

    private string _currentText;

    private void Start()
    {
        UpdateObjectiveText(force: true);
    }

    private void Update()
    {
        UpdateObjectiveText();
    }

    private void UpdateObjectiveText(bool force = false)
    {
        if (stage1State == null || objectiveText == null)
        {
            return;
        }

        string desiredText;

        if (stage1State.StageCompleted)
        {
            desiredText = "Objective: Stage complete";
        }
        else if (stage1State.BeaconActivated)
        {
            desiredText = "Objective: Proceed to extraction";
        }
        else
        {
            desiredText = "Objective: Activate the beacon";
        }

        if (!force && desiredText == _currentText)
        {
            return;
        }

        _currentText = desiredText;
        objectiveText.text = _currentText;
    }
}

