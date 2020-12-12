using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
public class PlayerCntrl : MonoBehaviour
{
    public float ForcePower;//сила прыжка оленя
    private int PresentsCounter;//счетчик подарков

    private bool FaceRight = true;//направление взгляда оленя
    private bool Started; //проверка начинал ли игру игрок
    public GameObject Explosion; //подбор подарка и взрыв при помощи системы частиц
    public GameObject Die; //взрыв при помощи системы частиц в случае смерти
    public Text PresentsCount; //передача количества подобраных подарков в интерфейс
    public Text StartingText; //стартовый текст удаляющийся после старта игры
    public Text FinalScore;// итоговый счет передающийся во второй канвас
    public AudioSource TakePresent; // ссылка на звук подбора подарка
    public AudioSource LoseSound; // ссылка на звук смерти игрока
    public Image PauseImage; // просто текст ПАУЗА если поставили паузу
    private bool Paused = false;
    
    void Start()
    {
        PresentsCounter = 0;
        Time.timeScale = 0;
    }

    
    void Update()
    {
        //проверка можно ли поставить паузу либо ее снять
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
        //управление героем
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
        //проверка на начало игры
        if (Started && !Paused)
        {
            Destroy(StartingText);
            Time.timeScale = 1;
        }

    }
    //изменить направление взгляда оленя
    void flip()
    {
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        FaceRight = !FaceRight;
    }
    //проверка с чем столкнулся игрок
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
    //запись рекорда
    private void HighScore()
    {
        if(PlayerPrefs.GetInt ("Score") <= PresentsCounter)
        {
            PlayerPrefs.SetInt("Score", PresentsCounter);
        }
    }

}
