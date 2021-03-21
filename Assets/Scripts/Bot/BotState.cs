using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BotState : MonoBehaviour
{
	private Bot bot;
	private Rigidbody rb;
	private List<PathHolder> paths;
	[HideInInspector] public Animator anim;

	[SerializeField] private float speed;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private float gravity;

	[HideInInspector] public Vector3 spawnPoint;
	[HideInInspector] public int botPathState = 0;

	// Start is called before the first frame update
	void Start()
	{
		speed = Random.Range(15, 20);
		rotationSpeed = Random.Range(100, 180);

		rb = GetComponent<Rigidbody>();
		bot = GetComponent<Bot>();
		anim = GetComponentInChildren<Animator>();
		paths = transform.parent.GetComponentsInChildren<PathHolder>().ToList();
		spawnPoint = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		if (bot.isActive)
		{
			if (transform.parent.tag == ("RotatingPlatform"))
			{
				transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
			}
			if (!isGrounded()) rb.AddForce(-transform.up * gravity, ForceMode.Impulse);

			if (botPathState > 10) botPathState = 10;

			var path = GetClosestPath(paths[botPathState]);
			var targetPoint = RandomPointInBounds(path.GetComponent<Collider>().bounds);

			var target = new Vector3(targetPoint.x - rb.transform.position.x,
							0,
							targetPoint.z - rb.transform.position.z);
			var targetAngle = Quaternion.LookRotation(target);

			rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetAngle, rotationSpeed * Time.deltaTime));

			rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);

			anim.SetFloat("Vertical", 1, .1f, Time.deltaTime);
		}
		else anim.SetFloat("Vertical", 0, 0f, Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (bot.isActive)
		{
			if (other.CompareTag("BotPath"))
			{
				botPathState++;
			}
			if (other.CompareTag("FinishArea"))
			{
				bot.isActive = false;
				var gameState = GameObject.FindGameObjectWithTag("GamePlay").GetComponent<GameState>();
				gameState.counter++;
			}
			if (other.CompareTag("DeathZone"))
			{
				botPathState = 0;
				transform.position = spawnPoint;
			}
		}
	}

	public Vector3 RandomPointInBounds(Bounds bounds)
	{
		return new Vector3(
			Random.Range(bounds.min.x, bounds.max.x),
			Random.Range(bounds.min.y, bounds.max.y),
			Random.Range(bounds.min.z, bounds.max.z)
		);
	}

	Path GetClosestPath(PathHolder holder)
	{
		float bestDistance = Mathf.Infinity;
		Path bestPath = null;

		foreach (var path in holder.paths)
		{
			float distance = Vector3.Distance(transform.position, path.transform.position);

			if (distance < bestDistance)
			{
				bestDistance = distance;
				bestPath = path;
			}
		}

		return bestPath;
	}

	bool isGrounded()
	{
		var hit = Physics.Raycast(GetComponent<Collider>().bounds.center, Vector3.down, GetComponent<Collider>().bounds.extents.y + 0.5f, LayerMask.GetMask("Ground"));

		return hit;
	}
}
