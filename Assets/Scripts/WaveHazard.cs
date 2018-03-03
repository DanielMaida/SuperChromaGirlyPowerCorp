using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHazard : MonoBehaviour {

    private float waveSpeed = 5.5f;
    private Rigidbody2D _rb;
    private GameObject[] waves = new GameObject[5];

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody2D>();
        for(int i = 1; i < 5; i++)
        {
            waves[i] = transform.Find(i.ToString()).gameObject;
            int choose = Random.Range(1, 3);
            if (choose == 2)
                waves[1].SetActive(false);
        }
	}
    
	// Update is called once per frame
	void FixedUpdate () {
        _rb.position += (Vector2.right * Time.deltaTime * waveSpeed);
	}

    

}
