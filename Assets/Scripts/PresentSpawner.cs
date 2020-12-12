using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentSpawner : MonoBehaviour
{
    public GameObject present;
    private GameObject currentPres;
    private Vector2 position;
    public bool LastPresent;
    private bool OnLeftSide = true;
    public float speed;
    private float timer = 5f;
    private const float TIMER_TO_RSPWN = 12f;
    private Vector2 ToWhere;

    public GameObject Enemy;
    private GameObject CurrentEnemy;
    // Start is called before the first frame update
    private void Spawn()
    {
        int randomPos = UnityEngine.Random.Range(1, 16);
        switch (randomPos)
        {
            case 1:
                position = new Vector2(Random.Range(-10.4f, -0.45f), Random.Range(5.75f, 5.75f));
                break;
            case 2:
                position = new Vector2(Random.Range(-0.45f, 11.1f), Random.Range(5.75f, 5.75f));
                break;
            case 3:
                position = new Vector2(Random.Range(-10.25f, -10.28f), Random.Range(-0.35f, 5.26f));
                break;
            case 4:
                position = new Vector2(Random.Range(-7.8f, -4.77f), Random.Range(1.98f, 3.8f));
                break;
            case 5:
                position = new Vector2(Random.Range(-3.59f, -0.66f), Random.Range(2.2f, 4.67f));
                break;
            case 6:
                position = new Vector2(Random.Range(1.26f, 4.24f), Random.Range(2.2f, 4.67f));
                break;
            case 7:
                position = new Vector2(Random.Range(5.55f, 8.55f), Random.Range(1.98f, 3.8f));
                break;
            case 8:
                position = new Vector2(Random.Range(10.75f, 10.78f), Random.Range(-0.35f, 5.26f));
                break;
            case 9:
                position = new Vector2(Random.Range(-9.35f, -0.9f), Random.Range(-0.25f, 0.1f));
                break;
            case 10:
                position = new Vector2(Random.Range(1.65f, 10.3f), Random.Range(-0.25f, 0.1f));
                break;
            case 11:
                position = new Vector2(Random.Range(-10.55f, -7.1f), Random.Range(-2.5f, -5.45f));
                break;
            case 12:
                position = new Vector2(Random.Range(-4.9f, -0.7f), Random.Range(-2.5f, -5.45f));
                break;
            case 13:
                position = new Vector2(Random.Range(1.25f, 5.5f), Random.Range(-2.5f, -5.45f));
                break;
            case 14:
                position = new Vector2(Random.Range(7.6f, 11.1f), Random.Range(-2.5f, -5.45f));
                break;
            case 15:
                int rnd = UnityEngine.Random.Range(1, 12);
                switch (rnd)
                {
                    case 1:
                        position = new Vector2(-8.89f, 3.63f);
                        break;
                    case 2:
                        position = new Vector2(9.41f, 3.63f);
                        break;
                    case 3:
                        position = new Vector2(-4.56f, 1.2f);
                        break;
                    case 4:
                        position = new Vector2(5.17f, 1.2f);
                        break;
                    case 5:
                        position = new Vector2(-7.5f, -1.5f);
                        break;
                    case 6:
                        position = new Vector2(-1.17f, -1.5f);
                        break;
                    case 7:
                        position = new Vector2(0.32f, -1.5f);
                        break;
                    case 8:
                        position = new Vector2(1.6f, -1.5f);
                        break;
                    case 9:
                        position = new Vector2(7.95f, -1.5f);
                        break;
                    case 10:
                        position = new Vector2(-6.09f, -5.21f);
                        break;
                    case 11:
                        position = new Vector2(6.6f, -5.21f);
                        break;
                }
                break;
        }

        Debug.Log("spwn");
        currentPres = Instantiate(present, position, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        if (currentPres == null)
        {
            Debug.Log("null");
            Spawn();
        }
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(CurrentEnemy);
            if (OnLeftSide)
            {
                CurrentEnemy = Instantiate(Enemy, new Vector2(-15f, currentPres.transform.position.y), Quaternion.identity);
               // CurrentEnemy.GetComponent<Rigidbody2D>().AddForce((new Vector2(1, 0) * speed));
               ToWhere = new Vector2(15, currentPres.transform.position.y);
                OnLeftSide = false;
            }
            else if(!OnLeftSide)
            {
                CurrentEnemy = Instantiate(Enemy, new Vector2(15f, currentPres.transform.position.y), Quaternion.identity);
                //CurrentEnemy.GetComponent<Rigidbody2D>().AddForce((new Vector2(-1,0)) * speed);
                ToWhere = new Vector2(-15,currentPres.transform.position.y);
                
                OnLeftSide = true;
            }
            timer = TIMER_TO_RSPWN;
        }
        if (CurrentEnemy != null)
        {
            CurrentEnemy.transform.position = Vector2.MoveTowards(CurrentEnemy.transform.position, ToWhere, step);
        }            
    }
}
