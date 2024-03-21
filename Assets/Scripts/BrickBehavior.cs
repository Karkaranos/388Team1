using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    public bool isKey = false;
    private BrickManagers BM;

    [SerializeField] private Sprite KeyBrickSprite;
    [SerializeField] private Sprite NormalBrickSprite;
    private void Start()
    {
        BM = FindObjectOfType<BrickManagers>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            BM.keyDestroyed(gameObject);
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
    }
}
