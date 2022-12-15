using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Compositor : BTNode
{
    LinkedList<BTNode> children = new LinkedList<BTNode>();
    LinkedListNode<BTNode> currentChild = null;

    protected override NodeResult Execute()
    {
        if(children.Count == 0)
        {
            return NodeResult.Success;
        }

        currentChild = children.First;
        return NodeResult.Inprogress;
    }

    protected bool Next()
    {
        if(currentChild != children.Last)
        {
            currentChild = currentChild.Next;
            return true;
        }
        return false;
    }

    protected override void End()
    {
        currentChild = null;
    }
}
