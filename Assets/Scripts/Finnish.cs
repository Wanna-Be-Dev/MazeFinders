using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finnish : MonoBehaviour
{

    private int allPlayers;
    private int exitPlayers = 0;
    public Race manager;

    private void Start()
    {
        manager = GameObject.Find("Race Manager").GetComponent<Race>();
        allPlayers = manager.PlayersInGame();
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        exitPlayers++;

    }
    private void Update()
    {
        if(manager.PlayersInGame() == exitPlayers && manager.StartedGame()) 
        {
            SceneManager.LoadScene(0);
        }
    }
}
