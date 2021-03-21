using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [HideInInspector] public BotPhysics physics;
    [HideInInspector] public BotState state;

    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        physics = GetComponent<BotPhysics>();
        state = GetComponent<BotState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
