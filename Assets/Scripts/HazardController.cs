using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardController : MonoBehaviour {

    /// <summary>
    /// 0 - Wave
    /// 1 - Minion
    /// 2 - Tentacle
    /// 3 - Tornado
    /// </summary>
    public GameObject[] Hazards;
    private int waveSpawns = 0;
    private GameObject[] spawns = new GameObject[5];
    private bool[] previousSpawns = new bool[5];
    public bool levelAcomplished;


    // Use this for initialization
	void Start () {
       for(int i = 1; i < 5; i++)
        {
            spawns[i] = transform.Find("Spawn" + i).gameObject;
        }
        StartCoroutine(LevelOne());
	}
 
    IEnumerator LevelOne()
    {
        while (!levelAcomplished)
        {
            StartCoroutine(SpawnHazard(10, Hazards[0], 1f, false));
            yield return new WaitForSeconds(10f);
            StartCoroutine(SpawnHazard(15, Hazards[1], 1.5f, true));
            yield return new WaitForSeconds(15 * 1.5f);
            GameObject tentacle = Instantiate(Hazards[2], transform.position, Hazards[2].transform.rotation);
            while( tentacle != null && tentacle.activeInHierarchy )
            {
                yield return null;
            }
            
        }
    }

    IEnumerator SpawnHazard(int numOfHazards, GameObject hazard, float spawnRate, bool tag)
    {
        int hazardCount = 0;
        while (hazardCount < 10)
        {
            int spawnPosition = ChooseSpawnLane();
            GameObject spawnedHazard =Instantiate(hazard, spawns[spawnPosition].transform.position, hazard.transform.rotation);
            hazardCount++;
            if(tag) {
                spawnedHazard.GetComponent<MinionController>().SetWeakPointTag("Lane" + spawnPosition);
            }
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private int ChooseSpawnLane()
    {
        return Random.Range(1, 4);
    }

}
