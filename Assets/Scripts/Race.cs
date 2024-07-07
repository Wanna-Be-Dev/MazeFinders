using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Race : MonoBehaviour
{
    public int playerAmount = 0;

    public float currentTime = 0;

    [Header("UI")]
    public TextMeshProUGUI currentTimeText;
    public TextMeshProUGUI CountDown;
    public TextMeshProUGUI ErrorMessage;

    private bool raceStarted = false;

    [SerializeField] 
    private GameObject wall;

    void Start()
    {
        currentTime = 0;
        SetWall(true);
        ErrorMessage.gameObject.SetActive(false);
    }
    public int PlayersInGame()
    {
        return playerAmount;
    }
    public bool StartedGame()
    {
        return raceStarted;
    }
    public void AddPlayer()
    {
        playerAmount++;
    }
    private void SetWall(bool state)
    {
       wall.SetActive(state);
    }
    private IEnumerator StartCountDown()
    {
        for(int count = 3; count > 0;count--)
        {
            CountDown.text = count.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }
        CountDown.gameObject.SetActive(false);
        SetWall(false);
        raceStarted = true;
        Debug.Log(raceStarted);
    }
    private void StopWatch(bool startCount)
    {
        if(startCount)
        {
            currentTime = currentTime + Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            currentTimeText.text = time.ToString(@"mm\:ss\:fff");
        }
    }
       
    public void StartRace()
    {
        if (playerAmount >= 2)
        {
            ErrorMessage.gameObject.SetActive(false);
            StartCoroutine(StartCountDown());
        } 
        else
        {
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = "Please Connect one more player";
        }     
    }
   
    void Update()
    {    
    
        if (Input.GetKeyDown("return"))
        {
            Debug.Log("route started");
            StartRace();
        }
        StopWatch(raceStarted);
 
    }

    
}
