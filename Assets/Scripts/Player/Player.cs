using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerPhysics physics;
    [HideInInspector] public Movement movement;

    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        physics = GetComponent<PlayerPhysics>();
        movement = GetComponent<Movement>();
    }
}
