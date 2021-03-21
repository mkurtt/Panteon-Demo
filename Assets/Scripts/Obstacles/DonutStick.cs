using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DonutStates
{
    Waiting,
    Pushing,
    Pulling,
    Pushed
}

public class DonutStick : MonoBehaviour
{
    private Rigidbody rb;

    [Tooltip("x per second, starts once the donut is at start position")]
    [SerializeField] private float frequency;
    [Tooltip("x per second, starts once the donus is at target position")]
    [SerializeField] private float waitTime;
    [Tooltip("Stick's movement speed")]
    [SerializeField] private float movementSpeed;
    [Tooltip("Optimal max range is 30 due to stick size")]
    [SerializeField] private float range;

    private DonutStates state;
    private Vector3 startPos;
    private Vector3 targetPos;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        state = DonutStates.Waiting;

        startPos = transform.position;
        targetPos = transform.position + -transform.right * range;

        timer = frequency;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (state == DonutStates.Waiting && timer <= 0)
        {
            state = DonutStates.Pushing;
        }
        else if (state == DonutStates.Pushed && timer <= 0)
        {
            state = DonutStates.Pulling;
        }

        if (state == DonutStates.Pushing && (transform.position - targetPos).sqrMagnitude <= 0.3f)
        {
            state = DonutStates.Pushed;
            timer = waitTime;
        }
        else if (state == DonutStates.Pulling && (transform.position - startPos).sqrMagnitude <= 0.3f)
        {
            state = DonutStates.Waiting;
            timer = frequency;
        }
    }

    private void FixedUpdate()
    {
        if (state == DonutStates.Pushing)
        {
            PushDonut();
        }
        if(state == DonutStates.Pulling)
        {
            PullDonut();
        }
    }

    void PushDonut()
    {
        rb.MovePosition(transform.position + -transform.right * movementSpeed * Time.fixedDeltaTime);
    }

    void PullDonut()
    {
        rb.MovePosition(transform.position + transform.right * movementSpeed * Time.fixedDeltaTime);
    }
}
