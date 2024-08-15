using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private float distance = 150f;

    public Camera mainCamera;
    public ParticleSystem shotPartical;
    public AudioSource shotSound;
    public GameObject impactEffect;

    public int curruentAmmo = 10;
    public int maxAmmo = 15;
    public float reloadTime = 1f;
    private bool isReload = false;
    public Animator animator;

    public LineRenderer lineRenderer;
    private Vector3 targetPosition;
    private bool isMoving = false;
    public float moveSpeed = 5f;
    public GameObject player;

    public Rigidbody footballBall;
    public float ballSpeed = -250;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        //footballBall = GameObject.Find("Ball").GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Rope();
        }
        if (Input.GetMouseButton(1) && isMoving)
        {
            MoveRope();
        }
        if (Input.GetMouseButtonUp(1))
        {
            StopRope();
        }
        if (isReload)
            return;
        if (player.GetComponent<PlayerController>().ballPowerActive == true && curruentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            GunShoot();
        }
    }

    void GunShoot()
    {
        curruentAmmo--;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, distance) && curruentAmmo > 0)
        {
            shotSound.Play();
            shotPartical.Play();
            print(hit.collider.gameObject);

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(hit.normal * ballSpeed);
            }

            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2f);
        }
        
    }

    public IEnumerator Reload()
    {
        isReload = true;
        Debug.Log("Reloding!");
        animator.SetBool("reload", true);
        yield return new WaitForSeconds(reloadTime);
        curruentAmmo = maxAmmo;
        isReload = false;
        animator.SetBool("reload", false);
    }

    void Rope()
    {
        if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, distance))
        {
            if (hit.collider.gameObject.CompareTag("tp"))
            {
                targetPosition = hit.point;
                isMoving = true;

                lineRenderer.SetPosition(0, player.transform.position);
                lineRenderer.SetPosition(1, targetPosition);
                lineRenderer.enabled = true;

                //player.transform.position = hit.point;
                Debug.Log("Ropeees");
            }
        }
    }

    void MoveRope()
    {
       
        player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition, moveSpeed * Time.deltaTime);

       
        if (Vector3.Distance(player.transform.position, targetPosition) < 0.1f)
        {
            StopRope();
        }
    }

    void StopRope()
    {
        // Hareketi durdur ve halatý gizle
        isMoving = false;
        lineRenderer.enabled = false;
    }

}
