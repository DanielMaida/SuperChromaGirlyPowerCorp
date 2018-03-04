using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleHazard : MonoBehaviour {

    private float tentacleSpeed = 5.5f;
    private Rigidbody2D _rb;

    private int tentacleHitpoints;

	// Use this for initialization
	void Start () {
        tentacleHitpoints = 1;
        _rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        _rb.position += (Vector2.right * Time.deltaTime * tentacleSpeed);
        if (tentacleHitpoints == 0) {
            GameObject.Find("HazardManager").GetComponent<HazardController>().levelAcomplished = true;
            Destroy(this.gameObject); 
        }

    }

    public void DecreaseHitpoints() {
        tentacleHitpoints--;
    }

}
