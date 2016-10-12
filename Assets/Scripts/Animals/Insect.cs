using UnityEngine;
using System.Collections.Generic;

public class Insect : Animal
{
    private bool finished = false;

    // Update is called once per frame
    protected override void Update()
    {
        if (!Menu.running) return;

        base.Update();

        if (!checkRange() && !finished) moveTowardsPlant();
        else if (finished) moveAway();
        else action(target);
    }

    //Effect on plants
    protected override void action(Plant plant)
    {
        switch (effect)
        {
            case "pollinator": plant.pollinate(); plant.setGrowth(0); break;

            case "harm":
                if (plant.getSpecial() == "carnivore" && !plant.isSpecialing() && plant.getMaturity() != "SAPLING")
                {
                    plant.setHealth(15);
                    plant.setThirst(15);
                    plant.setGrowth(15);
                    plant.setHappiness(25);
                    plant.setSpecial(true);
                    ItemManager.money += reward;
                    Destroy(gameObject);
                }
                else if (plant.getSpecial() == "ghost") plant.setHealth(1);
                else plant.setHealth(-2);
                break;

            case "pest":
                if (plant.getSpecial() == "carnivore" && !plant.isSpecialing() && plant.getMaturity() != "SAPLING")
                {
                    plant.setHealth(15);
                    plant.setThirst(15);
                    plant.setGrowth(15);
                    plant.setHappiness(25);
                    plant.setSpecial(true);
                    ItemManager.money += reward;
                    Destroy(gameObject);
                }
                else if (plant.getSpecial() == "ghost") plant.setHealth(1);
                else {
                    plant.setInfested(true);
                    Destroy(gameObject);
                }
                break;
        }

        finished = true;
    }
}
