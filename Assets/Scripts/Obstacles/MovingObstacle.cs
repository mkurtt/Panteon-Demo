using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MovingObstacle : MonoBehaviour
{
    private Rigidbody rb;

    [Tooltip("List paths in order to create a designated path, or Reset")]
    [SerializeField] private List<PathCorner> corners;
    [SerializeField] private float movementSpeed=10;

    private Vector3 cornerPos;
    private int cornerIndex;

    [Header("Push Explosion Variables")]
    public float force=100;
    public float radius=10;
    public float upforce= 0;

    private void Reset()
    {
        corners = transform.parent.GetComponentsInChildren<PathCorner>().ToList();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();

        cornerIndex = 0;
        cornerPos = corners[cornerIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        PushTowards();

        if ((transform.position - cornerPos).sqrMagnitude <= 0.3f)
        {
            cornerIndex++;
            if (cornerIndex >= corners.Count) cornerIndex = 0;

            cornerPos = corners[cornerIndex].transform.position;
        }
    }

    void PushTowards()
    {
        var dir = (cornerPos - transform.position).normalized;

        rb.MovePosition(transform.position + dir * movementSpeed * Time.fixedDeltaTime);
    }
}
