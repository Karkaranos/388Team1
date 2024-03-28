using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    [System.Serializable]
    public class BallSprites
    {
        public Sprite sprite;
        public GameManager.LastingPowerupType type;

        public BallSprites(Sprite sprite, GameManager.LastingPowerupType type)
        {
            this.sprite = sprite;
            this.type = type;
        }
    }

    private Rigidbody2D rb;
    private GameManager gm;
    public float moveSpeed;
    public bool isBig;
    public float BigBallTimer;
    public float BigBallMultiplier;

    public LayerMask brickLayer;
    public GameManager.LastingPowerupType currentPowerup;
    public float cometExplosionRadius;

    [SerializeField] BallSprites[] sprites;

    

    //this is the number of big ball powerups the player currently has.
    //does not include the active big ball powerup if the player has one
    public int BigBallInventory;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<GameManager>();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopBall(collision.transform.position);
        }

        if (collision.gameObject.CompareTag("Deathbox"))
        {
            //gm.LoseLife();
            StopBall( new Vector2(-4.84f, 0));
        }

        if (collision.gameObject.CompareTag("Brick") && currentPowerup == GameManager.LastingPowerupType.Comet)
        {
            currentPowerup = GameManager.LastingPowerupType.None;
            Explode();
            UpdateSprite();
        }
    }

    public void Explode()
    {
        Collider2D[] bricksToExplode = Physics2D.OverlapCircleAll(transform.position, cometExplosionRadius, brickLayer);
        foreach (Collider2D col in bricksToExplode)
        {
            if (col.gameObject.GetComponent<BrickBehavior>() != null)
            {
                col.gameObject.GetComponent<BrickBehavior>().DestroyThisBrick();
            }
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
        gm.HitBall();
        rb.velocity = Vector2.zero;
        transform.position = new Vector2(hitPoint.x + 1f, hitPoint.y);
        Debug.Log("Stopped");
        
    }

    public void Kicked(GameManager.LastingPowerupType type)
    {
        if (type != GameManager.LastingPowerupType.None)
        {
            currentPowerup = type;
        }
        UpdateSprite();
    }

    public void UpdateSprite()
    {
        foreach (BallSprites spr in sprites)
        {
            if (spr.type.Equals(currentPowerup))
            {
                GetComponent<SpriteRenderer>().sprite = spr.sprite;
                break;
            }
        }
    }


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
