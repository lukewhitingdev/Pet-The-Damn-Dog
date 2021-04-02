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

    public void spawnPetPoint()
    {
        // TODO: Make logic for adding the total click force to this part.

        // Spawn the petpoint object.
        GameObject petpoint = Instantiate(petPointPrefab, this.gameObject.transform);
        petpoint.GetComponent<PetPoint>().setValue(1.0f);
    }
}
