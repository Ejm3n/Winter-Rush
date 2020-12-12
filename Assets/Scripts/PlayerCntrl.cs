using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
public class PlayerCntrl : MonoBehaviour
{
    public float ForcePower;
    private int PresentsCounter;

    private bool FaceRight = true;
    private bool Started;
    public GameObject Explosion;
    public GameObject Die;
    public Text PresentsCount;
    public Text StartingText;
    public Text FinalScore;
    public AudioSource TakePresent;
    public AudioSource LoseSound;
    public Image PauseImage;
    private bool Paused = false;
    // Start is called before the first frame update
    void Start()
    {

        PresentsCounter = 0;
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !Paused && Started)
        {

            PauseImage.enabled = true;
            Time.timeScale = 0;
            Paused = true;
            Debug.Log("3");
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.A) || (Input.GetKeyDown(KeyCode.D) || (Input.GetKeyDown(KeyCode.W))) || (Input.GetKeyDown(KeyCode.UpArrow))
           || (Input.GetKeyDown(KeyCode.RightArrow)) || (Input.GetKeyDown(KeyCode.LeftArrow))) && Paused) && Started)
        {
            PauseImage.enabled = false;
            Time.timeScale = 1;
            Paused = false;
            Debug.Log("4");
        }
        if (Input.GetKeyDown(KeyCode.W) || (Input.GetKeyDown(KeyCode.UpArrow)))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * ForcePower, ForceMode2D.Force);
            Started = true;
        }

        if (Input.GetKeyDown(KeyCode.D) || (Input.GetKeyDown(KeyCode.RightArrow)))
        {
            GetComponent<Rigidbody2D>().AddForce((Vector2.up + Vector2.right) * (ForcePower - 10), ForceMode2D.Force);
            Started = true;
            if (FaceRight)
            {
                flip();
            }
        }
        if (Input.GetKeyDown(KeyCode.A) || (Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            GetComponent<Rigidbody2D>().AddForce((Vector2.up + Vector2.left) * (ForcePower - 10), ForceMode2D.Force);
            Started = true;
            if (!FaceRight)
            {
                flip();
            }
        }
        if (Started && !Paused)
        {
            Destroy(StartingText);
            Time.timeScale = 1;
        }

    }
    void flip()
    {
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        FaceRight = !FaceRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Present")
        {
            var exp = Instantiate(Explosion, collision.transform.position, Quaternion.identity);
            TakePresent.Play();
            Destroy(collision.gameObject);
            PresentsCounter++;
            GetComponent<Rigidbody2D>().mass += 0.01f; 
            PresentsCount.text = PresentsCounter.ToString();
        }
        if (collision.gameObject.tag == "Wall")
        {
            var die = Instantiate(Die, transform.position, Quaternion.identity);
            FinalScore.text = PresentsCounter.ToString();
            LoseSound.Play();
            HighScore();
            Destroy(gameObject);
        }
    }
    private void HighScore()
    {
        if(PlayerPrefs.GetInt ("Score") <= PresentsCounter)
        {
            PlayerPrefs.SetInt("Score", PresentsCounter);
        }
    }

}
