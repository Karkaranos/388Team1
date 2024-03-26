using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameManager gm;
    public float moveSpeed;
    public bool isBig;
    public float BigBallTimer;
    public float BigBallMultiplier;

    //this is the number of big ball powerups the player currently has.
    //does not include the active big ball powerup if the player has one
    public int BigBallInventory;
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
            StopBall(collision.transform.position);
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

        direction.Normalize();
        rb.velocity = direction * moveSpeed;
    }

    public void StopBall()
    {
        rb.velocity = Vector2.zero;
        Debug.Log("Stopped");

    }

    public void StopBall(Vector2 hitPoint)
    {
        rb.velocity = Vector2.zero;
        transform.position = new Vector2(hitPoint.x + 1f, hitPoint.y);
        Debug.Log("Stopped");
        
    }

    /*public void Kicked(GameManager.PowerupType type)
    {
        switch (type)
        {

        }
    }*/

    public IEnumerator BigBall()
    {
        BigBallInventory--;
        isBig = true;
        transform.localScale *= BigBallMultiplier;
        yield return new WaitForSeconds(BigBallTimer);
        transform.localScale = new Vector3(1, 1, 1);
        isBig = false;
        if (BigBallInventory > 0)
        {
            StartCoroutine(BigBall());
        }
    }
}
