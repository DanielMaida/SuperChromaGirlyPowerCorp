using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionController : MonoBehaviour {

    private Rigidbody2D _rb;

    public float minionSpeed;
    public bool prepare;

    // Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if(!prepare)
            _rb.position += (Vector2.right * Time.deltaTime * minionSpeed);
    }

    public void SetWeakPointTag(string tag)
    {
        transform.Find("WeakPoint").gameObject.tag = tag;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AttackZone"))
        {
            prepare = true;
            StartCoroutine(PrepareToAttack());
        }
    }

    IEnumerator PrepareToAttack()
    {
        yield return new WaitForSeconds(0.6f);
        prepare = false;
        minionSpeed *= 2;
    }
}
