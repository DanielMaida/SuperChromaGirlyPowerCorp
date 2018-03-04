using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardDestroyer : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent != null)
        {
            Destroy(collision.transform.parent.gameObject);
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
