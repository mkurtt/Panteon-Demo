using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{
    private PaintableWall wall;

    [HideInInspector] public GameObject paint;
    [HideInInspector] public float speed;
    [HideInInspector] public float angularSpeed;

    private float x;
    private float z;

    

    // Start is called before the first frame update
    void Start()
    {
        wall = GetComponentInParent<PaintableWall>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wall.isActive)
        {
            x = Input.GetAxis("Vertical");
            z = Input.GetAxis("Horizontal");

            //if (x < 0) x = 0;

            transform.Translate(-transform.up * x * speed * Time.deltaTime);
            transform.Rotate(new Vector3(0, 1, 0) * z * angularSpeed * Time.deltaTime);

            if (x != 0 || z != 0)
            {
                var go = Instantiate(paint, transform.position, Quaternion.identity, wall.transform);

                go.transform.localRotation = Quaternion.Euler(new Vector3(0, transform.localEulerAngles.y, 0));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);

        if (other.CompareTag("BrushFrame"))
            wall.ResetBrush();
    }
}
