using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeResult
{
    Success,
    Failure,
    InProgress
}

public class BTNode
{
    public NodeResult UpdatedNode()
    {
        if(!started)
        {
            started = true;
            NodeResult executeResult = Execute();
            if(executeResult != NodeResult.InProgress)
            {
                EndNode();
                return executeResult;
            }
        }

        //update
        NodeResult updateResult = Update();
        if(updateResult != NodeResult.InProgress)
        {
            EndNode();
        }
        return updateResult;
    }

    //child implemented:
    protected virtual NodeResult Execute() {return NodeResult.Success;}
    protected virtual NodeResult Update() {return NodeResult.Success;}
    protected virtual void End() { }

    private void EndNode()
    {
        started = false;
        End();
    }

    bool started;
}
