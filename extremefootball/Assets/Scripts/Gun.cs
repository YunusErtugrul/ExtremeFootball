using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private float distance = 100f;

    public Camera mainCamera;
    public ParticleSystem shotPartical;
    public AudioSource shotSound;
    public GameObject impactEffect;

    private Rigidbody footballBall;
    private float ballSpeed = -250;
    // Start is called before the first frame update
    void Start()
    {
        footballBall = GameObject.Find("Ball").GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GunShoot();
        }
    }

    void GunShoot()
    {
        RaycastHit hit; 
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, Mathf.Infinity))
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
}
