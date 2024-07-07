using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    [Header("Doors")]
    [SerializeField] public GameObject DoorU;
    [SerializeField] public GameObject DoorR;
    [SerializeField] public GameObject DoorD;
    [SerializeField] public GameObject DoorL;


    [Header("Props")]
    [SerializeField] private GameObject[] randomObjects;
    
    public Renderer rend;

    [Header("Materials")]
    [SerializeField] private Material[] material;


    private void Start()
    {
        Randomobjects();
        rend.GetComponent<MeshRenderer>().material = material[Random.Range(0, material.Length)];
    }
    private void Randomobjects()
    {
        foreach (GameObject prop in randomObjects)
        {
            if(Random.Range(0, 2) == 1)
                prop.SetActive(true);
            else
                prop.SetActive(false);
        }    
    }


    public void RotateRandomly()
    {
        int count = Random.Range(0, 4);

        for (int i = 0; i < count; i++)
        {
            transform.Rotate(0, 90, 0);

            GameObject tmp = DoorL;
            DoorL = DoorD;
            DoorD = DoorR;
            DoorR = DoorU;
            DoorU = tmp;
        }
    }

}
