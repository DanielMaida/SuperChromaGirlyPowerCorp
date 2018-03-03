using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardController : MonoBehaviour {

    public GameObject[] Hazards;

    private int waveSpawns = 0;
    
    // Use this for initialization
	void Start () {
        StartCoroutine(CreateHazards());
	}
	
	IEnumerator CreateHazards()
    {
        while (true) {
            if (waveSpawns < 2)
            {
                SpawnWave();
                waveSpawns++;
            }
            else
            {
                SpawnTentacle();
                waveSpawns = 0;
            }
            float frequency = Random.Range(1, 8);
            yield return new WaitForSeconds(frequency / 2);
        }
    }


    void SpawnWave()
    {
        Instantiate(Hazards[0], transform.position, Hazards[0].transform.rotation);
    }

    void SpawnTentacle() {
        Instantiate(Hazards[1], transform.position, Hazards[1].transform.rotation);
    }
}
