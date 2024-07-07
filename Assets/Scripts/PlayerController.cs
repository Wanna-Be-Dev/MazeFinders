using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction movement;

    public Rigidbody rb;

    [Header("Materials")]
    [SerializeField] private Material[] material;

    public Renderer rend;

    public Race manager;

    [Header("Movement")]
    public float moveSpeed;

    Vector3 moveDirection = Vector3.zero;
    Vector2 converstion = Vector2.zero;

    private void Start()
    {
        manager = GameObject.Find("Race Manager").GetComponent<Race>();
        manager.AddPlayer();

        rend = gameObject.GetComponent<Renderer>();
        rend.GetComponent<MeshRenderer>().material = material[Random.Range(0, material.Length)];
    }
    private void Awake()
    {
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
    }

    private void OnEnable()
    {
        movement = player.FindAction("Move");
        movement.Enable();
    }
    private void OnDisable()
    {
        movement.Disable();
    }
    private void MovePlayer()
    {

        converstion = movement.ReadValue<Vector2>();
        moveDirection.x = converstion.x;
        moveDirection.z = converstion.y;
        //rb.MovePosition( rb.position + moveDirection * moveSpeed*Time.fixedDeltaTime);
        // rb.velocity += new Vector3(moveDirection.x,0,moveDirection.z)*Time.deltaTime* moveSpeed;
        if (moveDirection != Vector3.zero)
        {
            Debug.Log("1");
            rb.AddForce(moveDirection.x, 0, moveDirection.z, ForceMode.Impulse);
        }
        else
        {
            rb.velocity = rb.velocity / 1.55f;
            Debug.Log("2");
        }
    }

    private void FixedUpdate()
    {
        //Debug.Log("Movement Values" + movement.ReadValue<Vector2>());
        MovePlayer();
    }

 
}
