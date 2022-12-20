using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperBehavior : BehaviorTree
{
    protected override void ConstructTree(out BTNode rootNode)
    {
        Selector RootSelector = new Selector();

        #region attackTarget
        Sequencer attackTargetSeq = new Sequencer();
        BTTask_MoveToTarget moveToTarget = new BTTask_MoveToTarget(this, "Target", 2);

        BTTask_RotateTowardsTarget rotateTowardsTarget = new BTTask_RotateTowardsTarget(this, "Target", 10f);
        //attack

        attackTargetSeq.AddChild(moveToTarget);
        attackTargetSeq.AddChild(rotateTowardsTarget);

        BlackboardDecorator attackTargetDecorator = new BlackboardDecorator(this,
                                                                            attackTargetSeq, 
                                                                            "Target",
                                                                            BlackboardDecorator.RunCondition.KeyExists,
                                                                            BlackboardDecorator.NotifyRule.RunConditionChange,
                                                                            BlackboardDecorator.NotifyAbort.both
                                                                            );

        RootSelector.AddChild(attackTargetDecorator);
        #endregion attackTarget

        #region CheckLastSeenLoc
        Sequencer CheckLastSeenLocSeq = new Sequencer();
        BTTask_MoveToLoc MoveToLastSeenLoc = new BTTask_MoveToLoc(this, "LastSeenLoc", 3);
        BTTask_Wait WaitAtLastSeenLoc = new BTTask_Wait(2f);
        BTTask_RemoveBlackboardData removeLastSeenLoc = new BTTask_RemoveBlackboardData(this, "LastSeenLoc");
        CheckLastSeenLocSeq.AddChild(MoveToLastSeenLoc);
        CheckLastSeenLocSeq.AddChild(WaitAtLastSeenLoc);
        CheckLastSeenLocSeq.AddChild(removeLastSeenLoc);

        BlackboardDecorator CheckLastSeenLocDeocorator = new BlackboardDecorator(this,
                                                                                 CheckLastSeenLocSeq,
                                                                                 "LastSeenLoc",
                                                                                 BlackboardDecorator.RunCondition.KeyExists,
                                                                                 BlackboardDecorator.NotifyRule.RunConditionChange,
                                                                                 BlackboardDecorator.NotifyAbort.none
                                                                                 );

        RootSelector.AddChild(CheckLastSeenLocDeocorator);

        #endregion CheckLastSeenLoc

        #region Patrolling
        Sequencer patrollingSeq = new Sequencer();

        BTTask_GetNextPatrolPoint getNextPatrolPoint = new BTTask_GetNextPatrolPoint(this, "PatrolPoint");
        BTTask_MoveToLoc moveToPatrolPoint = new BTTask_MoveToLoc(this, "PatrolPoint", 3);
        BTTask_Wait waitAtPatrolPoint = new BTTask_Wait(2f);

        patrollingSeq.AddChild(getNextPatrolPoint);
        patrollingSeq.AddChild(moveToPatrolPoint);
        patrollingSeq.AddChild(waitAtPatrolPoint);
        
        RootSelector.AddChild(patrollingSeq);

        #endregion Patrolling

        rootNode = RootSelector;
    }
}
