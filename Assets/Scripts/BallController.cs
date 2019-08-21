using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BallController : MonoBehaviour
{
    public int forceMinimal;
    public int forceMaximal;
    [Range(2,50)]
    [SerializeField]
    private int scoreToWin;
    private int force;
    Rigidbody2D rigid;
    private int startToRight;
    public PaddleController[] player;
    [SerializeField]
    private GameObject panelFinish;
    [SerializeField]
    Text textWinner;
    private AudioSource _audio;
    public AudioClip[] hitSound;
    // Start is called before the first frame update
    void Start()
    {
        startToRight = Random.Range(0, 1);
        force = Random.Range(forceMinimal, forceMaximal);
        rigid = GetComponent<Rigidbody2D>();
        if (startToRight == 0)
        {
            Vector2 direction = new Vector2(-2, 0).normalized;
            rigid.AddForce(direction * force);
        }
        else
        {
            Vector2 direction = new Vector2(2, 0).normalized;
            rigid.AddForce(direction * force);
        }
        foreach(PaddleController _player in player)
        {
            _player.score = 0;
        }
        panelFinish.SetActive(false);
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetBall()
    {
        transform.localPosition = new Vector2(0, 0);
        rigid.velocity = new Vector2(0, 0);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Goal Line")
        {
            int audioCelebration = Random.Range(2,hitSound.Length-1);
            _audio.PlayOneShot(hitSound[audioCelebration]);
            ResetBall();
            if (coll.gameObject.name == "Goal Line Right")
            {
                Vector2 direction = new Vector2(2, 0).normalized;
                rigid.AddForce(direction * force);
                player[0].score += 1;
                if (player[0].score == scoreToWin)
                {
                    panelFinish.SetActive(true);
                    textWinner.text = "the Winner is " + player[0].playerName;
                    Destroy(gameObject);
                    return;
                }
            }
            else
            {
                Vector2 direction = new Vector2(-2, 0).normalized;
                rigid.AddForce(direction * force);
                player[1].score += 1;
                if (player[1].score == scoreToWin)
                {
                    panelFinish.SetActive(true);
                    textWinner.text = "the Winner is " + player[1].playerName;
                    Destroy(gameObject);
                    return;
                }
            }
            DisplayScore();
        }

        if (coll.gameObject.tag == "Side Line")
        {
            _audio.PlayOneShot(hitSound[1]);
            Color newBallColor = Random.ColorHSV();
            gameObject.GetComponent<SpriteRenderer>().color = newBallColor;
        }

        if (coll.gameObject.tag == "Player")
        {
            _audio.PlayOneShot(hitSound[0]);
            float angle = (transform.position.y - coll.transform.position.y) * 5f;
            Vector2 direction = new Vector2(rigid.velocity.x, angle).normalized;
            rigid.velocity = new Vector2(0, 0);
            rigid.AddForce(direction * force * 2);
            float randomScale = Random.Range(.1f, 3f);
            gameObject.GetComponent<Transform>().localScale = new Vector3(randomScale, randomScale, randomScale);
        }
    }

    void DisplayScore()
    {
        foreach (PaddleController _player in player)
        {
            _player.textScore.text = _player.score.ToString();
        }
    }
}
