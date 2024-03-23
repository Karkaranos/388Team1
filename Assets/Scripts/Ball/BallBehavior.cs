using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameManager gm;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<GameManager>();
        Launch();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StopBall();
            Kicked();
        }

        if (collision.gameObject.tag == "Deathbox")
        {
            gm.LoseLife();
            StopBall();
            transform.position = Vector2.zero;
        }
    }
    public void Launch()
    {
        Launch(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
    }

    public void Launch(Vector2 direction)
    {
       /* if (direction.x > 1)
        {
            direction.x = 1;
        }
        if (direction.x < -1)
        {
            direction.x = -1;
        }*/
        rb.velocity = direction * moveSpeed;
    }

    public void StopBall()
    {
        rb.velocity = Vector2.zero;
    }

    public void Kicked()
    {

    }
}
