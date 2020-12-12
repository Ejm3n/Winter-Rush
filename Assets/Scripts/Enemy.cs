using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Explosion;
    public AudioSource EnemyTakesPresent;
    private void OnCollisionEnter2D(Collision2D collision)
    {    
        if (collision.gameObject.tag == "Present")
        {
            var exp = Instantiate(Explosion, collision.transform.position, Quaternion.identity);
            EnemyTakesPresent.Play();
            Destroy(collision.gameObject);
        }
    }
}
