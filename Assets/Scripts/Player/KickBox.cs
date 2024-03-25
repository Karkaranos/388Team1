using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickBox : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    public GameObject ball;
    public float KickStrength;

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

    public void Kickball()
    {
        var kickDirection = ball.transform.position - Player.transform.position;
        kickDirection.Normalize();
        ball.GetComponent<Rigidbody2D>().AddForce(kickDirection * KickStrength, ForceMode2D.Impulse);
    }
}
