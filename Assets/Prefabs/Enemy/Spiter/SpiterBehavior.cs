using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiterBehavior : BehaviorTree
{
    protected override void ConstructTree(out BTNode rootNode)
    {
        Selector RootSelector = new Selector();

        RootSelector.AddChild(new BTTaskGroup_AttackTarget(this, 5, 10f));

        RootSelector.AddChild(new BTTaskGroup_MoveToLastSeenLoc(this, 3));

        RootSelector.AddChild(new BTTaskGroup_Patrolling(this, 3));

        rootNode = RootSelector;
    }
}
