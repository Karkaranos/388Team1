using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    public bool isKey = false;
    private BrickManagers BM;

    public Sprite KeyBrickSprite;
    [SerializeField] private Sprite NormalBrickSprite;
    private void Start()
    {
        BM = FindObjectOfType<BrickManagers>();
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
    }

    public void DestroyThisBrick()
    {
        DropPowerup();
        BM.keyDestroyed(gameObject);
    }
}
