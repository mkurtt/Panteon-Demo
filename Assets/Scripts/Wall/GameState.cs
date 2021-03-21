using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public CanvasGroup panel;
    public Text qualified;
    public Text gameOver;
    public Button Restart;
    public Text countdown;
    public bool isWin;

    [SerializeField] Player player;
    private Camera cam;

    private List<Transform> obstacles;
    private List<GameObject> bots;

    [HideInInspector] public int counter;
    private float timer;
    private bool isGameStarted;

    private bool isGameOver;

    /// <summary>
    /// Must be set to true after Everything!
    /// </summary>
    public bool IsGameOver
    {
        get { return isGameOver; }

        set
        {
            if (value == true)
            {
                player.isActive = false;
                if(player.physics.wall)
                    player.physics.wall.isActive = false;

                StopBots();

                panel.gameObject.SetActive(true);

                if (isWin)
                {
                    qualified.gameObject.SetActive(true);
                }
                else
                {
                    gameOver.gameObject.SetActive(true);
                }
                Restart.gameObject.SetActive(true);
            }

            else
			{
                player.transform.parent = null;

                if (player.physics.wall)
				{
                    player.physics.wall.isActive = false;
                    player.physics.wall.ResetWall();
                    player.physics.paintCam.enabled = false;
                    cam.GetComponent<Camera>().enabled = true;
                }

                panel.gameObject.SetActive(false);

                qualified.gameObject.SetActive(false);
                Restart.gameObject.SetActive(false);
                gameOver.gameObject.SetActive(false);

                player.transform.position = player.physics.spawnPoint.position;
                player.transform.localRotation = Quaternion.Euler(0, 180, 0);

                foreach (var bot in bots)
                {
                    var botScript = bot.GetComponent<Bot>();
                    bot.transform.position = botScript.state.spawnPoint;
                    bot.transform.localRotation = Quaternion.Euler(0, 180, 0);
                    botScript.state.botPathState = 0;
                    botScript.transform.parent = botScript.physics.originalParent;
                }
            }
            isGameOver = value;
        }
    }

	private void Start()
	{
        cam = Camera.main;
        isGameStarted = false;
        timer = 3;
        counter = 0;

        bots = GameObject.FindGameObjectsWithTag("Bot").ToList();
        obstacles = GameObject.FindGameObjectWithTag("Obstacles").GetComponentsInChildren<Transform>().ToList();
    }

	private void Update()
	{
		if (!isGameStarted)
		{
            timer -= Time.deltaTime;
            countdown.text = Mathf.Round(timer).ToString();
        }
        if(timer <= 0 && !isGameStarted)
		{
            countdown.gameObject.SetActive(false);
            StartGame();
		}
        

		if(counter >= 5)
		{
            IsGameOver = true;
		}
	}

	private void StartGame()
	{
        isGameStarted = true;

        player.isActive = true;


        foreach (var bot in bots)
        {
            bot.GetComponent<Bot>().isActive = true;
        }
    }

	public void ActivateGameArea(bool value)
	{
		foreach (var item in obstacles)
		{
            item.gameObject.SetActive(value);
		}

        foreach (var bot in bots)
        {
            bot.SetActive(value);
        }
    }

    public void StopBots()
	{
        foreach (var bot in bots)
        {
            bot.GetComponent<Bot>().isActive = false;
        }
    }

    public void RestartGame()
	{
        timer = 3;
        countdown.gameObject.SetActive(true);
        isWin = false;
        counter = 0;
        ActivateGameArea(true);
        IsGameOver = false;
        isGameStarted = false;
        


    }
}
