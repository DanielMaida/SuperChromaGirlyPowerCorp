using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardController : MonoBehaviour {

    public GameObject[] Hazards;

    
    
    // Use this for initialization
	void Start () {
        StartCoroutine(CreateHazards());
	}
	
	IEnumerator CreateHazards()
    {
        while (true) {
            SpawnWave();
            yield return new WaitForSeconds(1.5f);
        }
    }


    void SpawnWave()
    {
        Instantiate(Hazards[0], transform.position, Quaternion.identity);
    }

    void SpawnTentacle() {
        Instantiate(Hazards[1], transform.position, Quaternion.identity);
    }
}
