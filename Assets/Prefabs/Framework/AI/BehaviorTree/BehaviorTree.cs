using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorTree : MonoBehaviour
{
    BTNode Root;
    Blackboard blackboard = new Blackboard();

    public Blackboard Blackboard
    {
        get { return blackboard; }
    }
    // Start is called before the first frame update
    void Start()
    {
        ConstructTree(out Root);
        SortTree();
    }

    private void SortTree()
    {
        int priortyCounter = 0;
        Root.SortPriority(ref priortyCounter);
       
    }

    protected abstract void ConstructTree(out BTNode rootNode);

    // Update is called once per frame
    void Update()
    {
        Root.UpdateNode();
    }

    public void AbortLowerThan(int priority)
    {
        BTNode currentNode = Root.Get();
        if(currentNode.GetPriority() > priority)
        {
            Root.Abort();
        }
    }
}
