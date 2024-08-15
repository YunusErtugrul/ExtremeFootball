using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject footballBall;
    public GameObject ammo;
    public GameObject dash;

    private float xRange = 10;

    public int ballCount;
    public int waveCount = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ballCount = GameObject.FindGameObjectsWithTag("Ball").Length;
        if (ballCount == 0)
        {
            Spawner(waveCount);
        }
        if (GameObject.FindGameObjectsWithTag("ammo").Length == 0)
        {
            Instantiate(ammo, RandomPosition(), ammo.transform.rotation);
        }
        if (GameObject.FindGameObjectsWithTag("boost").Length == 0)
        {
            Instantiate(dash, RandomPosition(), dash.transform.rotation);
        }
    }

    Vector3 RandomPosition()
    {
        float xPos = Random.Range(xRange, -xRange);
        float zPos = Random.Range(10,-5f);
        return new Vector3(xPos, 1f, zPos);
    }

    void Spawner(int enemiesToSpawn)
    {

        for (int i = 0; i < waveCount; i++)
        {
            Instantiate(footballBall, RandomPosition(), footballBall.transform.rotation);
        }

        waveCount++;
    }
}
