using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CataloguePlant : MonoBehaviour {

    public Text display;
    public Plant plant;
    public Image sapling, juvenile, mature;

    private int hp, growth, thirst, sun, happiness;
    private float coinage;

	// Use this for initialization
	void Start () {

        hp = plant.maxHealth;
        growth = plant.maxGrowth;
        thirst = plant.maxThirst;
        sun = plant.maxSun;
        happiness = plant.maxHappiness;
        coinage = plant.coinage;

        sapling.sprite = plant.getSprites()[0];
        juvenile.sprite = plant.getSprites()[1];
        mature.sprite = plant.getSprites()[2];

        if (plant.getSpecial() == "crystal")
        {
            display.text = plant.getIndex() + ". " + plant.getName() + "\n\n" +
            "Health: " + hp + "\n" +
            "Growth: " + growth + " / " + (growth * 2) + " / " + (growth * 4) + "\n" +
            "Thirst: " + thirst + "\n" +
            "Sun Energy: " + sun + "\n" +
            "Happiness: " + happiness + "\n" +
            "Coin Production: " + coinage + " / " + (coinage * 1.5f) + " / " + (coinage * 2.25f) + "\n\n" +
            plant.getDescription() + "\n\n" +
            "Ability: " + plant.getSpecialDescription();
            ;
        }
        else if (plant.getSpecial() == "ghost" || plant.getSpecial() == "weak")
        {
            display.text = plant.getIndex() + ". " + plant.getName() + "\n\n" +
            "Health: " + hp + "\n" +
            "Growth: " + growth + "\n" +
            "Thirst: " + thirst + "\n" +
            "Sun Energy: " + sun + "\n" +
            "Happiness: " + happiness + "\n" +
            "Coin Production: " + coinage + "\n\n" +
            plant.getDescription() + "\n\n" +
            "Ability: " + plant.getSpecialDescription();
            ;
        }
        else if (plant.getSpecial() == "lunar")
        {
            display.text = plant.getIndex() + ". " + plant.getName() + "\n\n" +
            "Health: " + hp + " / " + (hp * 2) + " / " + (hp * 4) + "\n" +
            "Growth: " + growth + " / " + (growth * 2) + " / " + (growth * 4) + "\n" +
            "Thirst: " + thirst + "\n" +
            "Moon Energy: " + sun + "\n" +
            "Happiness: " + happiness + "\n" +
            "Coin Production: " + coinage + " / " + (coinage * 1.5f) + " / " + (coinage * 2.25f) + "\n\n" +
            plant.getDescription() + "\n\n" +
            "Ability: " + plant.getSpecialDescription();
            ;
        }
        else if (plant.getSpecial() == "mushroom")
        {
            display.text = plant.getIndex() + ". " + plant.getName() + "\n\n" +
            "Health: " + hp + " / " + (hp * 2) + " / " + (hp * 4) + "\n" +
            "Growth: " + growth + " / " + (growth * 2) + " / " + (growth * 4) + "\n" +
            "Thirst: " + thirst + "\n" +
            "Energy: " + sun + "\n" +
            "Happiness: " + happiness + "\n" +
            "Coin Production: " + coinage + " / " + (coinage * 1.5f) + " / " + (coinage * 2.25f) + "\n\n" +
            plant.getDescription() + "\n\n" +
            "Ability: " + plant.getSpecialDescription();
            ;
        }

        else
        {
            display.text = plant.getIndex() + ". " + plant.getName() + "\n\n" +
            "Health: " + hp + " / " + (hp * 2) + " / " + (hp * 4) + "\n" +
            "Growth: " + growth + " / " + (growth * 2) + " / " + (growth * 4) + "\n" +
            "Thirst: " + thirst + "\n" +
            "Sun Energy: " + sun + "\n" +
            "Happiness: " + happiness + "\n" +
            "Coin Production: " + coinage + " / " + (coinage * 1.5f) + " / " + (coinage * 2.25f) + "\n\n" +
            plant.getDescription() + "\n\n" +
            "Ability: " + plant.getSpecialDescription();
            ;
        }
        
	}
}
