using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickBox : MonoBehaviour
{
    public GameObject Player;
    public GameObject ball;
    public float KickStrength;
    public Vector2 BiggerSize;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Player.gameObject.GetComponent<Players>().Kickable = true;
            ball = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            Player.gameObject.GetComponent<Players>().Kickable = false;
            ball = null;
        }
    }

    public void MakeBigger()
    {
        transform.localScale = BiggerSize;
    }

    public void Kickball()
    {
        var kickDirection = ball.transform.position - Player.transform.position;
        kickDirection.Normalize();
        ball.GetComponent<BallBehavior>().hitplayer = false;
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        ball.GetComponent<Rigidbody2D>().AddForce(kickDirection * KickStrength, ForceMode2D.Impulse);
        ball.GetComponent<BallBehavior>().Kicked(Player.GetComponent<Players>().currentPowerup);
    }
}
