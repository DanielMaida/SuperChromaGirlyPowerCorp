using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class TornadoScript : MonoBehaviour
{

    private List<GameObject> players = new List<GameObject>(4);
    public float tornadoSpeed = 5f;

    private int index = 0;
    private Vector3 startPosition;
    private int hitpoints = 4;

    void Start()
    {
        startPosition = transform.position;
        players.Add(GameObject.Find("Blue"));
        players.Add(GameObject.Find("Red"));
        players.Add(GameObject.Find("Yellow"));
        players.Add(GameObject.Find("Green"));
        int prob = Random.Range(1, 10);
        players = players.OrderBy(w => w.gameObject.name).ToList();
        if (prob > 5)
        {
            players.Reverse();
        }
        this.transform.Find("WeakPoint").tag = SetTag(players[index].name);
    }

    private string SetTag(string playerName)
    {
        if (playerName == "Green")
        {
            return "Lane2";
        }
        else if (playerName == "Blue")
        {
            return "Lane3";
        }
        else if (playerName == "Yellow")
        {
            return "Lane4";
        }
        else
            return "Lane1";
    }

    void Update()
    {
        if (hitpoints <= 0)
        {
            Destroy(this.gameObject);
        }
        if (index < 4)
        {
            transform.position = Vector2.MoveTowards(transform.position, players[index].transform.position, tornadoSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, players[index].transform.position) <= 0.1f)
            {
                index++;
                if (index < 4)
                    this.transform.Find("WeakPoint").tag = SetTag(players[index].name);
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, startPosition, tornadoSpeed * Time.deltaTime);
        }
    }

    public void DecreaseHitpoints()
    {
        Debug.Log("hitou");
        hitpoints--;
    }
}
