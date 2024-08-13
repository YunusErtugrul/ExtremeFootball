using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 10;
    private float jumpHeight = 2;
    public CharacterController player;

    private float groundDistance = 0.4f;
    public Transform groundCheck;
    public LayerMask groundMask;
    bool isGround;

    private float gravity = -19.65f;
    Vector3 velocity;
    // Update is called once per frame
    void Update()
    {
        isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if(Input.GetKeyDown(KeyCode.Space) && isGround) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        player.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        player.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ammo"))
        {
            Debug.Log("POWEEER!");
            Destroy(other.gameObject);
        }
    }
}
