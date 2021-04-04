using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetPointController : MonoBehaviour
{

    GameObject petPointPrefab;

    // Start is called before the first frame update
    void Start()
    {
        petPointPrefab = Resources.Load<GameObject>("Petpoint/PetPoint");               // Grab the petpoint prefab from the resources folder.
    }

    public void spawnPetPoint(float value)
    {
        // Spawn the petpoint object.
        GameObject petpoint = Instantiate(petPointPrefab, this.gameObject.transform);
        petpoint.GetComponent<PetPoint>().setValue(value);
    }
}
