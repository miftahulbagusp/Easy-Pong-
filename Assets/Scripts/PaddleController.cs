using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaddleController : MonoBehaviour
{
    public enum player
    {
        Player1, Player2
    }

    public player playerNumber;
    public string playerName;
    private SpriteRenderer spriteRenderer;
    public Color playerColor;
    public Text textName;
    public Text textScore;
    public int score;
    [Range(5, 15)]
    public float speed;
    private string axis;
    public float topLimit;
    public float bottomLimit;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        switch (playerNumber)
        {
            case player.Player1:
                playerName = PlayerPrefs.GetString("Player1");
                textName.text = playerName;
                float rColor1 = PlayerPrefs.GetFloat("rColor1");
                float gColor1 = PlayerPrefs.GetFloat("gColor1");
                float bColor1 = PlayerPrefs.GetFloat("bColor1");
                playerColor = new Color(rColor1, gColor1, bColor1, 1f);
                spriteRenderer.color = playerColor;
                textName.color = playerColor;
                
                break;
            case player.Player2:
                playerName = PlayerPrefs.GetString("Player2");
                textName.text = playerName;
                float rColor2 = PlayerPrefs.GetFloat("rColor2");
                float gColor2 = PlayerPrefs.GetFloat("gColor2");
                float bColor2 = PlayerPrefs.GetFloat("bColor2");
                playerColor = new Color(rColor2, gColor2, bColor2, 1f);
                spriteRenderer.color = playerColor;
                textName.color = playerColor;
               
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (playerNumber)
        {
            case player.Player1:
                axis = "Vertical1";
                break;
            case player.Player2:
                axis = "Vertical2";
                break;
        }
        float move = Input.GetAxis(axis) * speed * Time.deltaTime;
        float nextPos = transform.position.y + move;
        if (nextPos > topLimit)
        {
            move = 0;
        }
        if (nextPos < bottomLimit)
        {
            move = 0;
        }
        transform.Translate(0, move, 0);
    }
}
