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

    private float ballPowerUp;
    public bool ballPowerActive = false;

    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private float dashPower = 10f;
    private bool isDashing = false;
    private float lastDashTime = -Mathf.Infinity;
    public bool dashActive = false;

    public void Start()
    {
        ballPowerUp = GameObject.Find("pistols").GetComponent<Gun>().ballSpeed;
    }
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

        if (isGround == false && Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && Time.time >= lastDashTime + dashCooldown && dashActive)
        {
            StartCoroutine(Dash());
            dashActive = false;
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        lastDashTime = Time.time;
        Vector3 dashMove = Camera.main.transform.forward.normalized * dashPower;
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            player.Move(dashMove * (Time.deltaTime / dashDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
        Debug.Log("Dash completed!");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ammo"))
        {
            StartCoroutine(reloadTime());
            Debug.Log("Relodingg!");
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("boost"))
        {
            dashActive = true;
            Destroy(other.gameObject);
        }
    }

    IEnumerator reloadTime()
    {
        ballPowerActive = true;
        yield return new WaitForSeconds(6);
        ballPowerActive = false;
    }
}
