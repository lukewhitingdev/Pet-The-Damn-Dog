using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    private Animator dogAnimator;

    void Awake()
    {
        dogAnimator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        StartCoroutine(RandomChooseToSit(1.0f));
    }

    IEnumerator RandomChooseToSit(float interval)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(interval);           // Allow us to wait for seconds and also not use new alot inside a loop.
        while (true)
        {
            int rand = Random.Range(0, 100);

            if(rand % 2 == 1)                                                   // If random is even then sit if not then un-sit. 
                dogAnimator.SetBool("Sit", true);
            else
                dogAnimator.SetBool("Sit", false);

            // Place your method calls
            yield return waitForSeconds;                                       // Wait the desired seconds.
        }
    }
}
