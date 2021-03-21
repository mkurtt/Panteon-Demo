using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PaintableWall : MonoBehaviour
{
    
    [SerializeField] private RenderTexture rendTex;
    [SerializeField] private Slider slider;
    [SerializeField] private Text text;
    public Button resetBrush;

    [HideInInspector] public bool isActive;

    [Header("Brush")]
    [SerializeField] private Brush brush;
    [SerializeField] private float speed = 10;
    [SerializeField] private float angularSpeed = 150;
    [SerializeField] private GameObject paint;

    private Vector3 brushPos;
    private Quaternion brushRot;

    [SerializeField] private float timerStart= .4f;
    private float timer;

    private int whiteAmount = 0;

    void Start()
    {
        timer = timerStart;

        brush = GetComponentInChildren<Brush>();
        brushPos = brush.transform.position;
        brushRot = brush.transform.rotation;

        brush.speed = speed;
        brush.angularSpeed = angularSpeed;
        brush.paint = paint;
    }

    void Update()
    {
		if (isActive)
		{
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timer = timerStart;
                CalculatePercentage();
            }
        }
    }

    async private void CalculatePercentage()
    {
        RenderTexture.active = rendTex;
        var texture2D = new Texture2D(rendTex.width, rendTex.height);
        texture2D.ReadPixels(new Rect(0, 0, rendTex.width, rendTex.height), 0, 0);
        var totalPixels = texture2D.width * texture2D.height;
       
        var pixels = texture2D.GetPixels(0, 0, texture2D.width, texture2D.height);

        await GetWhiteAmount(pixels);

        float percentage = 100 - ((float)whiteAmount / (float)totalPixels) *100;

        slider.value = percentage;
        text.text = Mathf.RoundToInt(percentage).ToString() + " / 100";

        if(percentage >= 100)
        {
            var gameState = GameObject.FindGameObjectWithTag("GamePlay").GetComponent<GameState>();

            gameState.isWin = true;
            gameState.IsGameOver = true;
        }
    }

    Task GetWhiteAmount(Color[] pixels)
	{
        whiteAmount = 0;

        foreach (var pixel in pixels)
        {
            if (pixel == Color.white)
            {
                whiteAmount++;
            }
        }

        return Task.CompletedTask;
    }

    public void ResetBrush()
    {
        brush.transform.position = brushPos;
        brush.transform.rotation = brushRot;
    }

    public void ResetWall()
	{
        var paints = GameObject.FindGameObjectsWithTag("Paint").ToList();
        foreach (var p in paints)
		{
            Destroy(p);
		}


        slider.value = 0;
        text.text = "00 / 100";
        ResetBrush();
	}


}
