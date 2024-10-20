using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : Node
{
    public BehaviourTree()
    {
        name = "Tree";
    }

    public BehaviourTree(string n)
    {
        name = n;
    }

    public void PrintTree()
    {
        Debug.Log(this.name);
        for(int i = 0; i < children.Count; ++i)
        {
            Debug.Log(children[i].name);
            for(int y = 0; y < children[i].children.Count; ++y)
            {
                Debug.Log(children[i].children[y].name);
            }
        }
    }
}
