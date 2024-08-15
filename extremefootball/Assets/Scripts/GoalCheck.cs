using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCheck : MonoBehaviour
{
    public ParticleSystem firework;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("goal"))
        {
            Instantiate(firework, gameObject.transform);
            StartCoroutine(waitGoal());
            Debug.Log("Gooool!");
        }
    }

    IEnumerator waitGoal()
    {
        
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
