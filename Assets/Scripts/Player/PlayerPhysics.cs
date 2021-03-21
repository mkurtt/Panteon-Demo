using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    private Player player;
    private Rigidbody rb;

    [HideInInspector] public PaintableWall wall;
    [HideInInspector] public Camera paintCam;

    [HideInInspector] public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        var go = GameObject.FindGameObjectWithTag("PaintableWall");

        if (go)
        {
            wall = go.GetComponent<PaintableWall>();
            paintCam = wall.GetComponentInChildren<Camera>();
        }
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody>();
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (player.isActive)
        {
            if (other.CompareTag("FinishArea"))
            {
                var gameState = GameObject.FindGameObjectWithTag("GamePlay").GetComponent<GameState>();
                gameState.ActivateGameArea(false);
                gameState.StopBots();

                if (wall)
                {
                    wall.isActive = true;
                    paintCam.enabled = true;
                    Camera.main.enabled = false;
                }
                else
                {
                    gameState.isWin = true;
                    gameState.IsGameOver = true;
                }
                player.isActive = false;
            }
            if (other.CompareTag("DeathZone"))
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                transform.position = spawnPoint.position;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (player.isActive)
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
        if (player.isActive)
        {
            if (collision.gameObject.CompareTag("RotatingPlatform"))
            {
                transform.parent = null;
            }
        }
    }
}
