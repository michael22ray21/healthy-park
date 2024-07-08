using TMPro;
using UnityEngine;

public class Drops : MonoBehaviour
{
    [SerializeField] GameObject hitEffect;
    [SerializeField] GameObject floatingPoints;
    public float speed = 0.4f;
    Rigidbody2D rb;

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
            // TODO play the appropriate SFX
            GameManager.instance.AddPoints(point);
            if (floatingPoints) {
                GameObject pointEffect = Instantiate(floatingPoints, transform.position, Quaternion.identity);
                pointEffect.GetComponentInChildren<TMP_Text>().text = point.ToString();
                Destroy(pointEffect, 1f);
            }
            // tell the player to play the right animation
            if (point > 0) PlayerMovement.instance.PlayYay();
            else if (point < 0) {
                PlayerMovement.instance.PlayUgh();
                HealthManager.instance.DecreaseHealth();
            }
        }
        else if (collision.gameObject.layer == 7)
        {
            if (point > 0) HealthManager.instance.DecreaseHealth();
            if (hitEffect) {
                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(effect, 1f);
            }
        }
        Destroy(gameObject);
    }
}
