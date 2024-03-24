using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
<<<<<<< Updated upstream
    public bool isKey = false;
    private BrickManagers BM;

    public Sprite KeyBrickSprite;
    [SerializeField] private Sprite NormalBrickSprite;
    private void Start()
    {
        BM = FindObjectOfType<BrickManagers>();
=======
    private BrickManagers BM;
    private GameManager gm;

<<<<<<< HEAD:Assets/Scripts/BrickBehavior.cs
<<<<<<< Updated upstream:Assets/Scripts/BrickBehavior.cs
    [SerializeField] private Sprite KeyBrickSprite;
=======
    public Sprite KeyBrickSprite;
>>>>>>> 45bac85f18a667f0f2a20d45c8e6677d0e3b9e34:Assets/Scripts/Bricks/BrickBehavior.cs
    [SerializeField] private Sprite NormalBrickSprite;
=======
    //these are all public to work with the custom editor
    public bool isKey = false;
    public GameManager.PowerupType heldPowerup;
    public Sprite KeyBrickSprite;
    public Sprite NormalBrickSprite;
>>>>>>> Stashed changes:Assets/Scripts/Bricks/BrickBehavior.cs
    private void Start()
    {
        BM = FindObjectOfType<BrickManagers>();
        gm = FindObjectOfType<GameManager>();
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
=======
<<<<<<< HEAD:Assets/Scripts/BrickBehavior.cs
<<<<<<< Updated upstream:Assets/Scripts/BrickBehavior.cs
=======
=======
>>>>>>> 45bac85f18a667f0f2a20d45c8e6677d0e3b9e34:Assets/Scripts/Bricks/BrickBehavior.cs
>>>>>>> Stashed changes
        gameObject.AddComponent<PolygonCollider2D>();
    }

    public void DropPowerup()
    {
<<<<<<< Updated upstream
        //handle dropping powerups here
=======
<<<<<<< HEAD:Assets/Scripts/BrickBehavior.cs
        gm.ObtainedPowerup(heldPowerup);
=======
        //handle dropping powerups here
>>>>>>> 45bac85f18a667f0f2a20d45c8e6677d0e3b9e34:Assets/Scripts/Bricks/BrickBehavior.cs
>>>>>>> Stashed changes
    }

    public void DestroyThisBrick()
    {
        DropPowerup();
        BM.keyDestroyed(gameObject);
<<<<<<< Updated upstream
=======
<<<<<<< HEAD:Assets/Scripts/BrickBehavior.cs
>>>>>>> Stashed changes:Assets/Scripts/Bricks/BrickBehavior.cs
=======
>>>>>>> 45bac85f18a667f0f2a20d45c8e6677d0e3b9e34:Assets/Scripts/Bricks/BrickBehavior.cs
>>>>>>> Stashed changes
    }
}
