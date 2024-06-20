using System;
using UnityEngine;
using UnityEngine.Events;

public class Drops : MonoBehaviour
{
    public float speed = 0.4f;
    public GameObject hitEffect;
    private Rigidbody2D rb;
    public int point;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3) // the Player layer
        {
            // TODO add the point, then play the appropriate SFX
            UIManager.instance.AddPoints(point);
            // tell the player to play the right animation
            if (point > 0) PlayerMovement.instance.PlayYay();
            else if (point < 0) PlayerMovement.instance.PlayUgh();
        }
        else if (collision.gameObject.layer == 7)
        {
            // TODO if it is a healthy item and on [lose life on missed items] mode, then lose life, else just do nothing
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
        Destroy(gameObject);
    }
}
