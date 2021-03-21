using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private bool isReal3rdPerson;

    private Vector3 camOffSet;

    // Start is called before the first frame update
    void Start()
    {
        if (isReal3rdPerson)
        {
            transform.parent = player.transform;
            this.enabled = false;
        }

        camOffSet = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + camOffSet.y, player.transform.position.z + camOffSet.z);
    }
}
