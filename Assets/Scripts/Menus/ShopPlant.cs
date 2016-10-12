using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopPlant : MonoBehaviour {

    public Image image;

    public PlantManager pm;
    public Store store;

    public GameObject entity;
    private Plant plant;
    public Text text;

    private int price;

	// Use this for initialization
	void Start () {
        plant = entity.GetComponent<Plant>();

        image.sprite = plant.getSprites()[0];

        text.text = plant.getName() + "\n\n" +
            plant.getDescription() + "\n\n" +
            "Cost: -$" + plant.getPrice() + "; Production: +$" + plant.getCoinage();

        price = plant.getPrice();
	}

    public void buyPlant()
    {
        if (ItemManager.money < price || pm.getCount() == pm.getMax()) return;

        ItemManager.money -= price;
        pm.addPlant(store.getPlant(), entity);
    }

}
