using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperBehavior : BehaviorTree
{
    protected override void ConstructTree(out BTNode rootNode)
    {
        Selector RootSelector = new Selector();
        Sequencer attackTargetSeq = new Sequencer();
        BTTask_MoveToTarget moveToTarget = new BTTask_MoveToTarget(this, "Target", 2);
        attackTargetSeq.AddChild(moveToTarget);

        BlackboardDecorator attackTargetDecorator = new BlackboardDecorator(this,
                                                                            attackTargetSeq, "Target",
                                                                            BlackboardDecorator.RunCondition.KeyExists,
                                                                            BlackboardDecorator.NotifyRule.RunConditionChange,
                                                                            BlackboardDecorator.NotifyAbort.both
                                                                            );

        RootSelector.AddChild(attackTargetDecorator);



        Sequencer patrollingSeq = new Sequencer();

        BTTask_GetNextPatrolPoint getNextPatrolPoint = new BTTask_GetNextPatrolPoint(this, "PatrolPoint");
        BTTask_MoveToLoc moveToPatrolPoint = new BTTask_MoveToLoc(this, "PatrolPoint", 3);
        BTTask_Wait waitAtPatrolPoint = new BTTask_Wait(2f);

        patrollingSeq.AddChild(getNextPatrolPoint);
        patrollingSeq.AddChild(moveToPatrolPoint);
        patrollingSeq.AddChild(waitAtPatrolPoint);
        
        RootSelector.AddChild(patrollingSeq);

        rootNode = RootSelector;
    }
}
