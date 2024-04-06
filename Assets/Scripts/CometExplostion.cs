using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometExplostion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("kill", .5f);
    }

    void kill()
    {
        Destroy(gameObject);
    }
}