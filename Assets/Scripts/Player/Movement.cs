using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Player player;
    private Rigidbody rb;
    [HideInInspector] public Animator anim;

    [SerializeField] private float speed = 20;
    [SerializeField] private float rotationSpeed = 150;
    [SerializeField] private float gravity = 2;

    [SerializeField] private bool cheat;

    private float hor;
    private float ver;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isActive)
        {
            //Fixed rotation on RotatingPlatform
            if (transform.parent)
            {
                transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
            }

            hor = Input.GetAxis("Horizontal");
            ver = Input.GetAxis("Vertical");

            anim.SetFloat("Vertical", ver, .1f, Time.deltaTime);

            if (ver <= -.5f) ver = -.5f;
            if (ver < 0) hor = -hor;
		}
		else anim.SetFloat("Vertical", 0, 0f, Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (player.isActive)
        {
            //apply custom gravity
            if (!isGrounded()) rb.AddForce(-transform.up * gravity, ForceMode.Impulse);

            //Cheat for testing;
            if (cheat)
            {
                
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    rb.AddForce(transform.up * gravity, ForceMode.Impulse);
                    if (Input.GetKey(KeyCode.Space)) transform.Translate(transform.up);
                    if (Input.GetKey(KeyCode.W)) transform.position += transform.forward * ver * speed * Time.fixedDeltaTime * 10;
                }
            }

            rb.MovePosition(transform.position + transform.forward * ver * speed * Time.fixedDeltaTime);
            transform.Rotate((transform.up * hor) * rotationSpeed * Time.fixedDeltaTime);
        }
    }

    bool isGrounded()
    {
        var hit = Physics.Raycast(GetComponent<Collider>().bounds.center, Vector3.down, GetComponent<Collider>().bounds.extents.y + 0.5f, LayerMask.GetMask("Ground"));

        return hit;
    }
}
