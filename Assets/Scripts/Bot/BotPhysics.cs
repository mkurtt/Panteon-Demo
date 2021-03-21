using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotPhysics : MonoBehaviour
{
    private Bot bot;
    private Rigidbody rb;

    [HideInInspector] public Transform originalParent;

    // Start is called before the first frame update
    void Start()
    {
        bot = GetComponent<Bot>();
        rb = GetComponent<Rigidbody>();
        originalParent = transform.parent;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (bot.isActive)
        {
            if (collision.gameObject.CompareTag("RotatingPlatform"))
            {
                transform.parent = collision.transform;
            }

            if (collision.gameObject.CompareTag("RotatingStick"))
            {
                rb.AddExplosionForce(collision.gameObject.GetComponent<RotatingStick>().force,
                    collision.GetContact(0).point + Vector3.up,
                    collision.gameObject.GetComponent<RotatingStick>().radius,
                    collision.gameObject.GetComponent<RotatingStick>().upforce,
                    ForceMode.Impulse);
            }

            if (collision.gameObject.CompareTag("MovingObstacle"))
            {
                rb.AddExplosionForce(collision.gameObject.GetComponent<MovingObstacle>().force,
                    collision.GetContact(0).point + Vector3.up,
                    collision.gameObject.GetComponent<MovingObstacle>().radius,
                    collision.gameObject.GetComponent<MovingObstacle>().upforce,
                    ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (bot.isActive)
        {
            if (collision.gameObject.CompareTag("RotatingPlatform"))
            {
                transform.parent = originalParent;
            }
        }
    }
}
