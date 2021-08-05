using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PetPoint : MonoBehaviour
{
    public float destroyDelay = 0.0f;               // Delay on destruction after animation for point has finished.
    private Animator petPointAnimator;
    private TextMeshProUGUI petPointText;

    void Awake()
    {
        petPointAnimator = GetComponent<Animator>();
        petPointText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        // Destroys the current gameObject after the animation has finished + delay.
        Destroy(this.gameObject, petPointAnimator.GetCurrentAnimatorStateInfo(0).length + destroyDelay);    
    }

    // Sets the text inside the prefab to the desired value.
    public void setValue(float val) { petPointText.text = "+" + val.ToString(); }                        
}
