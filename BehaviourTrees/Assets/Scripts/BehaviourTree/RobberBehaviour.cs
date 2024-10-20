using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberBehaviour : MonoBehaviour
{
    BehaviourTree tree;

    // Start is called before the first frame update
    void Start()
    {
        tree = new BehaviourTree();

        Node steal = new Node("Steal Something");

        Node goToDiamond = new Node("Go To Diamond");

        Node goToVan = new Node("Go To Van");

        steal.AddChild(goToDiamond);

        steal.AddChild(goToVan);

        tree.AddChild(steal);

        tree.PrintTree();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
