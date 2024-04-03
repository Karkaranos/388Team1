using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    private BrickManagers BM;
    private GameManager gm;

    //these are all public to work with the custom editor
    public bool isKey = false;
    public GameManager.LastingPowerupType heldLastingPowerup;
    public GameManager.LimitedPowerupType heldLimitedPowerup;
    public Sprite KeyBrickSprite;
    public Sprite NormalBrickSprite;

    private void Start()
    {
        BM = FindObjectOfType<BrickManagers>();
        gm = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            DestroyThisBrick(collision.transform.position);
        }
    }

    public void MakeBrick()
    {
        if (isKey)
        {
            GetComponent<SpriteRenderer>().sprite = KeyBrickSprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = NormalBrickSprite;
        }
        gameObject.AddComponent<PolygonCollider2D>();
    }

    public void DropPowerup(Vector3 go)
    {

        //handle dropping powerups here
        gm.ObtainedPowerup(heldLastingPowerup);
        gm.ObtainedPowerup(heldLimitedPowerup, go);

    }

    public void DestroyThisBrick(Vector3 go)
    {
        DropPowerup(go);
        BM.keyDestroyed(gameObject);
    }
}
