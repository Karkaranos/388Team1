using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.Oculus.Input;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

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
    public float SaveBallTimer;
    public float BigBallTimer;
    public float BigBallMultiplier;
    public bool IsSplitBall;
    public float SplitBallTimerMax;
    public float splitBallStrength;
    public bool hitplayer;
    public Sprite SplitBallSprite;

    public GameObject Explosion;

    Coroutine SaveBallInstance;

    public LayerMask brickLayer;
    public GameManager.LastingPowerupType currentPowerup;
    public float cometExplosionRadius;
    public int PierceNumber;
    private int currentPierce;

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
            if (SaveBallInstance != null)
            {
                StopCoroutine(SaveBallInstance);
            }
            hitplayer = true;
        }

        if (collision.gameObject.CompareTag("Deathbox"))
        {
            if (SaveBallInstance != null)
            {
                StopCoroutine(SaveBallInstance);
            }
            StopBall( new Vector2(-4.84f, 0));
        }

        if (collision.gameObject.CompareTag("Brick") && currentPowerup 
            == GameManager.LastingPowerupType.Comet)
        {
            if (SaveBallInstance != null)
            {
                StopCoroutine(SaveBallInstance);
            }
            SaveBallInstance = StartCoroutine("SaveBall");
            currentPowerup = GameManager.LastingPowerupType.None;
            Explode();
            UpdateSprite();
        }

        if (collision.gameObject.CompareTag("Brick"))
        {
            if (SaveBallInstance != null)
            {
                StopCoroutine(SaveBallInstance);
            }
            SaveBallInstance = StartCoroutine("SaveBall");
        }
    }
    public void Explode()
    {
        Collider2D[] bricksToExplode = 
            Physics2D.OverlapCircleAll(transform.position, cometExplosionRadius, brickLayer);
        Instantiate(Explosion, transform.position, Quaternion.identity);
        foreach (Collider2D col in bricksToExplode)
        {
            if (col.gameObject.GetComponent<BrickBehavior>() != null)
            {
                col.gameObject.GetComponent<BrickBehavior>().DestroyThisBrick(transform.position);
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
        currentPowerup = GameManager.LastingPowerupType.None;
        UpdateSprite();
    }

    public void StopBall(Vector2 hitPoint)
    {
        gm.HitBall();
        rb.velocity = Vector2.zero;
        transform.position = new Vector2(hitPoint.x + 1f, hitPoint.y);
        Debug.Log("Stopped");
        currentPowerup = GameManager.LastingPowerupType.None;
        UpdateSprite();

    }

    public void Kicked(GameManager.LastingPowerupType type)
    {
        if (SaveBallInstance != null)
        {
            StopCoroutine(SaveBallInstance);
        }
        SaveBallInstance = StartCoroutine("SaveBall");
        if (type != GameManager.LastingPowerupType.None)
        {
            currentPowerup = type;
        }
        if (currentPowerup == GameManager.LastingPowerupType.Piercing)
        {
            currentPierce = 0;
            GetComponent<CircleCollider2D>().isTrigger = true;
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
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("KickBox"))
        {
            if (hitplayer)
            {
                StopBall(collision.GetComponent<KickBox>().Player.transform.position);
                hitplayer = false;
            }
        }

        if ((collision.CompareTag("PierceWall") || collision.CompareTag("KickBox"))&& currentPowerup == 
                GameManager.LastingPowerupType.Piercing) {
            GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PierceWall") || collision.CompareTag("KickBox"))
        {
            GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Brick") && currentPowerup == 
            GameManager.LastingPowerupType.Piercing)
        {
            collision.GetComponent<BrickBehavior>().DestroyThisBrick(transform.position);
            currentPierce++;
            if (currentPierce >= PierceNumber)
            {
                currentPowerup = GameManager.LastingPowerupType.None;
                GetComponent<CircleCollider2D>().isTrigger = false;
                UpdateSprite();
            }
        }
    }
    public void SplitBall(Vector3 dir)
    {
        IsSplitBall = true;
        StartCoroutine(SplitBallTimer());
        GetComponent<SpriteRenderer>().sprite = SplitBallSprite;
        GetComponent<Rigidbody2D>().AddForce(transform.right * splitBallStrength, ForceMode2D.Impulse);
    }

    private IEnumerator SplitBallTimer()
    {
        yield return new WaitForSecondsRealtime(SplitBallTimerMax);
        Destroy(gameObject);
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

    public IEnumerator SaveBall()
    {
        yield return new WaitForSeconds(SaveBallTimer);
        Vector2 newForce = new Vector2(-3f, 0);
        rb.AddForce(newForce, ForceMode2D.Force);
    }
}
