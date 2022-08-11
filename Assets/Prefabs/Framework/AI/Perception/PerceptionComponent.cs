using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionComponent : MonoBehaviour
{
    [SerializeField] SenseComp[] senses;
    LinkedList<PerceptionStimuli> currentlyPerceivedStimulis = new LinkedList<PerceptionStimuli>();

    PerceptionStimuli targetStimuli;

    public delegate void OnPerceptionTagetChanged(GameObject target, bool sensed);

    public event OnPerceptionTagetChanged onPerceptionTargetChanged;

    // Start is called before the first frame update
    void Start()
    {
        foreach(SenseComp sense in senses)
        {
            sense.onPerceptionUpdated += SenseUpdated;
        }
    }

    private void SenseUpdated(PerceptionStimuli stimuli, bool succsessfulySensed)
    {
        var nodeFound = currentlyPerceivedStimulis.Find(stimuli);
        if (succsessfulySensed)
        {
            if (nodeFound != null)
            {
                currentlyPerceivedStimulis.AddAfter(nodeFound, stimuli);
            }
            else
            {
                currentlyPerceivedStimulis.AddLast(stimuli);
            }
        }
        else
        {
            currentlyPerceivedStimulis.Remove(nodeFound);
        }

        if (currentlyPerceivedStimulis.Count != 0)
        {
            PerceptionStimuli highestStimuli = currentlyPerceivedStimulis.First.Value;
            if (targetStimuli == null || targetStimuli!=highestStimuli)
            {
                targetStimuli = highestStimuli;
                onPerceptionTargetChanged?.Invoke(targetStimuli.gameObject, true);
            }
        }
        else
        {
            if(targetStimuli!=null)
            {

                onPerceptionTargetChanged?.Invoke(targetStimuli.gameObject, false);
                targetStimuli = null;
            }
        }
    }
}
