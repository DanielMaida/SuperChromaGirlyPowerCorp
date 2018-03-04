using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHazard : MonoBehaviour {

    private float waveSpeed = 5.5f;
    private Rigidbody2D _rb;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody2D>();
	}
    
	// Update is called once per frame
	void FixedUpdate () {
		_rb.position += (Vector2.right * Time.deltaTime * waveSpeed);
	}

    

}
