using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private List<Collider> toBeIgnored;

    const int MASS_DRAG_FIXER = 100;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        foreach (var col in toBeIgnored)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), col);
        }

        rb.AddTorque(transform.forward * rotationSpeed * 1000 * MASS_DRAG_FIXER);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddTorque(transform.forward * rotationSpeed * MASS_DRAG_FIXER);
    }
}
