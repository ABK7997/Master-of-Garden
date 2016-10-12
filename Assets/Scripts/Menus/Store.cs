using UnityEngine;
using System.Collections;

public class Store : MonoBehaviour {

    private GameObject plant;

    public void changePlant(GameObject entity)
    {
        plant = entity;
    }

    public GameObject getPlant()
    {
        return plant;
    }

}
