﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform sprite;
    private Transform shadow;
    private Collider2D spriteCollider;
    private Collider2D shadowCollider;

    private const float gravityForce = 17f;
    private const float jumpSpeed = 13f;
    private const float jumpHeight = 3f;

    private bool stomp;
    private bool dazed;
    public int struggleCounter = 0;

    public string laneTag;
    public bool isGrounded;
    public bool shaking;

    public KeyCode jumpKey;

    void Start()
    {
        sprite = transform.Find("Sprite");
        spriteCollider = sprite.GetComponent<Collider2D>();
        spriteCollider.enabled = false;
        shadow = transform.Find("Shadow");
        shadowCollider = shadow.GetComponent<Collider2D>();
        isGrounded = true;
    }

    void Update()
    {
        if (shaking)
        {
            sprite.localScale -= new Vector3(0, 1f * Time.deltaTime, 0);
            if (sprite.localScale.y <= 1)
            {
                shaking = false;
                sprite.localScale = new Vector2(sprite.localScale.x, sprite.localScale.y);
            }
        }
        if (Input.GetKeyDown(jumpKey) && isGrounded && !dazed)
        {
            StartCoroutine(Jump());
        }
        if (dazed)
        {
            sprite.GetComponent<SpriteRenderer>().color = Color.grey;
            if (Input.GetKeyDown(jumpKey))
            {
                struggleCounter++;
                sprite.localScale = new Vector2(sprite.localScale.x, 1.2f);
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
            if (!collision.gameObject.CompareTag("TentacleDanger") && !collision.gameObject.CompareTag("Tornado"))
            {
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("WeakPoint") && collision.CompareTag(laneTag))
        {
            if (collision.transform.parent.CompareTag("Tentacle"))
            {
                collision.transform.parent.gameObject.GetComponent<TentacleHazard>().DecreaseHitpoints();
                Destroy(collision.transform.gameObject.GetComponent<CircleCollider2D>());
            }
            if (collision.transform.parent.CompareTag("Tornado"))
            {
                collision.transform.parent.gameObject.GetComponent<TornadoScript>().DecreaseHitpoints();
            }
            else
            {
                //Minion death animation here
                Destroy(collision.transform.parent.gameObject);
            }
        }
    }

    IEnumerator Bump()
    {
        shadowCollider.enabled = false;
        while (sprite.localPosition.y < 2.3f)
        {
            sprite.position = sprite.position + (Vector3.up * jumpSpeed * Time.deltaTime);
            yield return null;
        }
        isGrounded = false;
        shadowCollider.enabled = true;
    }

    public void HitPlayer()
    {
        dazed = true;
        struggleCounter = 0;
    }

    private bool CanBeHurt()
    {
        return sprite.localPosition.y < .5f;
    }

}
