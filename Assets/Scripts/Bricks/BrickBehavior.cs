using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    private BrickManagers BM;
    private GameManager gm;

    //these are all public to work with the custom editor
    public bool isKey = false;
    public GameManager.PowerupType heldPowerup;
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
            DestroyThisBrick();
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

    public void DropPowerup()
    {

        //handle dropping powerups here
        gm.ObtainedPowerup(heldPowerup);

    }

    public void DestroyThisBrick()
    {
        DropPowerup();
        BM.keyDestroyed(gameObject);
    }
}
