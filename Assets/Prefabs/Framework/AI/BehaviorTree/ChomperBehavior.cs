using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperBehavior : BehaviorTree
{
    protected override void ConstructTree(out BTNode rootNode)
    {
        BTTask_MoveToTarget moveToTarget = new BTTask_MoveToTarget(this, "Target", 2f);
        rootNode = moveToTarget;
    }
}
