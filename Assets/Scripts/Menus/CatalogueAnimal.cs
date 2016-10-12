using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CatalogueAnimal : MonoBehaviour {

    public Text display;
    public Animal animal;
    public Image sprite;

	// Use this for initialization
	void Start () {

        sprite.sprite = animal.getSprite();

        display.text = animal.name + "\n\n" +
            animal.getDescription() + "\n\n";

        if (animal.reward > 0) display.text += "Swatting Reward: $" + animal.reward;
        else display.text += "Swatting Penalty: -$" + -animal.reward;
	}
}
