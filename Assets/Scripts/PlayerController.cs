using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Transform sprite;
    private BoxCollider2D spriteCollider;

    private const float gravityForce = 13f;
    private const float jumpSpeed = 20f;
    private const float jumpHeight = 3f;

    private bool stomp; 

	void Start () {
        sprite = transform.Find("Sprite");
        spriteCollider = sprite.GetComponent<BoxCollider2D>();
        spriteCollider.enabled = false;
	}
	
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            StartCoroutine(Jump());
        }
	}

    private bool HighestPeak() {
        return sprite.localPosition.y >= jumpHeight;
    }

    private bool IsGrounded() {
        return sprite.localPosition.y <= .01f;
    }

    IEnumerator Jump()
    {
        while (!HighestPeak())
        {
            sprite.position = sprite.position + (Vector3.up * jumpSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(.05f);
        spriteCollider.enabled = true;
        while (!IsGrounded())
        {
            sprite.position = sprite.position - (Vector3.up * gravityForce * Time.deltaTime);
            yield return null;
        }
        spriteCollider.enabled = false;
        sprite.localPosition = new Vector2(0, 0.01f);
    }

    private void DamagePlayer()
    {
        Debug.Log("Damage");
        //Implement the sticky goo 
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Hazard") && CanBeHurt())
        {
            DamagePlayer();
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("HittableHazard")){
            Destroy(collision.transform.parent.gameObject);
        }
    }

 
    
    private bool CanBeHurt()
    {
        return sprite.localPosition.y < .5f;
    }


}
