using UnityEngine;
using System.Collections;

public class NewPlant : Plant {

    private Store store;

    void Start()
    {
        store = FindObjectOfType<PlantManager>().getStore();
    }

    protected override void Update()
    {
        if (!Menu.running || Tool.action == Tool.ACTION.SWATTER) return;

        if (hovering && Input.GetMouseButtonDown(0))
        {
            store.gameObject.SetActive(true);
            store.changePlant(gameObject);
            Menu.running = false;
        } 
    }

}
