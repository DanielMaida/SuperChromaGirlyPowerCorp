using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform sprite;
    private Collider2D spriteCollider;

    private const float gravityForce = 13f;
    private const float jumpSpeed = 12f;
    private const float jumpHeight = 3f;

    private bool stomp;
    private bool dazed;
    public int struggleCounter = 0;

    public bool isGrounded;
    public bool shaking;

    void Start()
    {
        sprite = transform.Find("Sprite");
        spriteCollider = sprite.GetComponent<Collider2D>();
        spriteCollider.enabled = false;
        isGrounded = true;
    }

    void Update()
    {
        if (shaking)
        {
            sprite.localScale -= new Vector3(0, 1f * Time.deltaTime, 0);
            if (sprite.localScale.y <= .5)
            {
                shaking = false;
                sprite.localScale = new Vector2(sprite.localScale.x, .5f);
            }
        }
        if (Input.GetKeyDown(KeyCode.Z) && isGrounded && !dazed)
        {
            StartCoroutine(Jump());
        }
        if (dazed)
        {
            sprite.GetComponent<SpriteRenderer>().color = Color.grey;
            if (Input.GetKeyDown(KeyCode.Z))
            {
                struggleCounter++;
                sprite.localScale = new Vector2(sprite.localScale.x, 0.7f);
                shaking = true;
                if (struggleCounter > 6)
                {
                    dazed = false;
                    sprite.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }
    }

    private bool HighestPeak()
    {
        return sprite.localPosition.y >= jumpHeight;
    }

    IEnumerator Jump()
    {
        spriteCollider.enabled = true;
        isGrounded = false;
        while (!HighestPeak())
        {
            sprite.position = sprite.position + (Vector3.up * jumpSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(.05f);
        spriteCollider.enabled = true;
        while (sprite.localPosition.y > 0)
        {
            sprite.position = sprite.position - (Vector3.up * gravityForce * Time.deltaTime);
            yield return null;
        }
        spriteCollider.enabled = false;
        isGrounded = true;
        sprite.localPosition = Vector2.zero;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hazard") && CanBeHurt())
        {
            HitPlayer();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("HittableHazard"))
        {
            Debug.Log("filha da puta");
            Destroy(collision.transform.parent.gameObject);
        }
    }

    private void HitPlayer()
    {
        dazed = true;
        struggleCounter = 0;
    }

    private bool CanBeHurt()
    {
        return sprite.localPosition.y < .5f;
    }
    
}
