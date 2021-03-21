using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private List<RotatingStick> sticks;

    [SerializeField] private float angularVelocity= 150;

    [Header("Push Explosion Variables")]
    [SerializeField] private float force = 150;
    [SerializeField] private float radius = 10;
    [SerializeField] private float upforce = -1;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var stick in sticks)
        {
            var motor = stick.GetComponent<HingeJoint>().motor;
            motor.targetVelocity = angularVelocity;
            stick.GetComponent<HingeJoint>().motor = motor;

            stick.force = force;
            stick.radius = radius;
            stick.upforce = upforce;
        }
    }

    private void Reset()
    {
        sticks = GetComponentsInChildren<RotatingStick>().ToList();

        Debug.Log(sticks.Count);
        for (int i = 0; i < sticks.Count; i++)
        {
            sticks[i].transform.rotation = Quaternion.identity;
            sticks[i].transform.Rotate(transform.up, 360 / sticks.Count * i);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
