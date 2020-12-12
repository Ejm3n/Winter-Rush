
using UnityEngine;

public class Portals : MonoBehaviour
{
    public float TpTo;
    //портал переносит объект вниз или вверх при соприкосновении игрока с ним
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = new Vector2(collision.transform.position.x,collision.transform.position.y+TpTo) ;

    }
}
