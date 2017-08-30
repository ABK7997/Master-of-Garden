using UnityEngine;
using System.Collections.Generic;

public class BeastManager : MonoBehaviour {

    public SaveManager sm;

    public Transform animalHolder;
    public GameObject[] pollinators;
    public GameObject pest, hornet, hostileBird, winterAnimal, rainyAnimal;

    private string stage;

    void Start()
    {
        stage = sm.stageName;
    }

    //Beast appearances at dawn
    public void newDawn(string season, string weather, List<Plant> plants)
    {
        int chance = Random.Range(0, 101);

        if (stage == "Mountain")
        {
            switch(season)
            {
                case "SPRING":
                    spawner(hostileBird, 2);
                    if (weather == "RAINY") groundSpawner2(rainyAnimal, 3);
                    break;

                case "SUMMER":
                    spawnHigh(hornet, 8);
                    break;

                case "AUTUMN":
                    spawner(hostileBird, 3);
                    if (weather == "RAINY") groundSpawner2(rainyAnimal, 2);
                    groundSpawner2(winterAnimal, 3);
                    break;

                case "WINTER":
                    spawner(hostileBird, 0);
                    groundSpawner2(winterAnimal, 5);
                    if (weather == "SNOWY") groundSpawner2(winterAnimal, 2);
                    break;
            }
        }

        else if (stage == "Zen")
        {
            switch (season)
            {
                case "SPRING":
                    spawner(hostileBird, 3);
                    if (weather == "RAINY") groundSpawner2(rainyAnimal, 1);
                    break;

                case "SUMMER":
                    spawnHigh(hornet, 2);
                    break;

                case "AUTUMN":
                    spawner(hostileBird, 3);
                    if (weather == "RAINY") groundSpawner2(rainyAnimal, 2);
                    break;

                case "WINTER":
                    spawner(hostileBird, 0);
                    groundSpawner2(winterAnimal, 3);
                    if (weather == "SNOWY") groundSpawner2(winterAnimal, 2);
                    break;
            }
        }

        else {
            switch (season)
            {
                case "SPRING":
                    spawner(hostileBird, 5);
                    if (chance < 5) spawner(pest, 1);
                    if (weather == "RAINY") groundSpawner(rainyAnimal, 2);
                    break;

                case "SUMMER":
                    spawnHigh(hornet, 4);
                    if (chance < 15) spawner(pest, 1);
                    break;

                case "AUTUMN":
                    spawner(hostileBird, 3);
                    if (chance < 25) spawner(pest, 1);
                    if (weather == "RAINY") groundSpawner(rainyAnimal, 5);
                    break;

                case "WINTER":
                    spawner(hostileBird, 5);
                    groundSpawner(winterAnimal, 2);
                    if (weather == "SNOWY") groundSpawner(winterAnimal, 1);
                    break;
            }
        }
    }

    //Beast appearances at the start of a new day
    public void newDay(string season, string weather, List<Plant> plants)
    {
        bool pollination = false;

        //Pollination
        if (season != "WINTER" && season != "AUTUMN" && weather != "RAINY")
        {
            foreach (Plant plant in plants)
            {
                if (plant.getGrowth() == 100 && !plant.isPollinated()) pollination = true;
            }
        }

        if (stage == "Mountain")
        {
            switch (season)
            {
                case "SPRING":
                    if (pollination) spawner(pollinators[0], 1);
                    spawner(hostileBird, 5);
                    if (weather == "RAINY") groundSpawner2(rainyAnimal, 3);
                    break;

                case "SUMMER":
                    if (pollination) spawner(pollinators[1], 1);
                    spawnHigh(hornet, 8);
                    break;

                case "AUTUMN":
                    if (weather == "RAINY") groundSpawner2(rainyAnimal, 4);
                    spawner(hostileBird, 3);
                    break;

                case "WINTER":
                    spawner(hostileBird, 0);
                    groundSpawner2(winterAnimal, 5);
                    if (weather == "SNOWY") groundSpawner2(winterAnimal, 2);
                    break;
            }
        }

        else if (stage == "Zen")
        {
            switch (season)
            {
                case "SPRING":
                    if (pollination) spawner(pollinators[0], 1);
                    spawner(hostileBird, 2);
                    if (weather == "RAINY") groundSpawner2(rainyAnimal, 1);
                    break;

                case "SUMMER":
                    if (pollination) spawner(pollinators[1], 1);
                    spawnHigh(hornet, 2);
                    break;

                case "AUTUMN":
                    if (weather == "RAINY") groundSpawner2(rainyAnimal, 2);
                    spawner(hostileBird, 1);
                    break;

                case "WINTER":
                    spawner(hostileBird, 0);
                    groundSpawner2(winterAnimal, 1);
                    if (weather == "SNOWY") groundSpawner2(winterAnimal, 1);
                    break;
            }
        }

        else
        {
            switch (season)
            {
                case "SPRING":
                    spawner(hostileBird, 10);
                    if (pollination) spawner(pollinators[0], 1);
                    if (weather == "RAINY") groundSpawner(rainyAnimal, 2);
                    break;

                case "SUMMER":
                    spawnHigh(hornet, 5);
                    if (pollination) spawner(pollinators[1], 1);
                    break;

                case "AUTUMN":
                    spawner(hostileBird, 5);
                    if (weather == "RAINY") groundSpawner(rainyAnimal, 3);
                    break;

                case "WINTER":
                    spawner(hostileBird, 7);
                    groundSpawner(winterAnimal, 2);
                    if (weather == "SNOWY") groundSpawner(winterAnimal, 1);
                    break;
            }
        }
    }

    //Beast appearances at dusk
    public void newDusk(string season, string weather, List<Plant> plants)
    {
        if (stage == "Mountain")
        {
            switch (season)
            {
                case "SPRING":
                    spawner(hostileBird, 2);
                    if (weather == "RAINY") groundSpawner2(rainyAnimal, 3);
                    groundSpawner2(winterAnimal, 1);
                    break;

                case "SUMMER":
                    spawnHigh(hornet, 8);
                    break;

                case "AUTUMN":
                    spawner(hostileBird, 2);
                    if (weather == "RAINY") groundSpawner2(rainyAnimal, 5);
                    groundSpawner2(winterAnimal, 3);
                    break;

                case "WINTER":
                    spawner(hostileBird, 0);
                    groundSpawner2(winterAnimal, 6);
                    if (weather == "SNOWY") groundSpawner2(winterAnimal, 3);
                    break;
            }
        }

        else if (stage == "Zen")
        {
            switch (season)
            {
                case "SPRING":
                    spawner(hostileBird, 2);
                    if (weather == "RAINY") groundSpawner2(rainyAnimal, 1);
                    break;

                case "SUMMER":
                    spawnHigh(hornet, 2);
                    break;

                case "AUTUMN":
                    spawner(hostileBird, 2);
                    if (weather == "RAINY") groundSpawner2(rainyAnimal, 2);
                    break;

                case "WINTER":
                    spawner(hostileBird, 0);
                    groundSpawner2(winterAnimal, 1);
                    if (weather == "SNOWY") groundSpawner2(winterAnimal, 1);
                    break;
            }
        }

        else
        {
            switch (season)
            {
                case "SPRING":
                    spawner(hostileBird, 5);
                    if (weather == "RAINY") groundSpawner(rainyAnimal, 2);
                    break;

                case "SUMMER":
                    spawnHigh(hornet, 6); break;

                case "AUTUMN":
                    spawner(hostileBird, 2);
                    if (weather == "RAINY") groundSpawner(rainyAnimal, 5);
                    break;

                case "WINTER":
                    spawner(hostileBird, 2);
                    groundSpawner(winterAnimal, 3);
                    if (weather == "SNOWY") groundSpawner(winterAnimal, 1);
                    break;
            }
        }  
    }

    //Beast appearances at night
    public void newNight(string season, string weather, List<Plant> plants)
    {
        if (stage == "Mountain")
        {
            switch (season)
            {
                case "SPRING":
                    spawner(hostileBird, 3);
                    if (weather == "RAINY") groundSpawner2(rainyAnimal, 1);
                    groundSpawner2(winterAnimal, 2);
                    break;

                case "SUMMER":
                    spawnHigh(hornet, 8);
                    break;

                case "AUTUMN":
                    spawner(hostileBird, 3);
                    if (weather == "RAINY") groundSpawner2(rainyAnimal, 2);
                    groundSpawner2(winterAnimal, 4);
                    break;

                case "WINTER":
                    spawner(hostileBird, 0);
                    groundSpawner2(winterAnimal, 7);
                    if (weather == "SNOWY") groundSpawner2(winterAnimal, 5);
                    break;
            }
        }

        else if (stage == "Zen")
        {
            switch (season)
            {
                case "SPRING":
                    spawner(hostileBird, 2);
                    if (weather == "RAINY") groundSpawner2(rainyAnimal, 1);
                    break;

                case "SUMMER":
                    spawnHigh(hornet, 2);
                    break;

                case "AUTUMN":
                    spawner(hostileBird, 2);
                    if (weather == "RAINY") groundSpawner2(rainyAnimal, 1);
                    break;

                case "WINTER":
                    spawner(hostileBird, 0);
                    groundSpawner2(winterAnimal, 2);
                    if (weather == "SNOWY") groundSpawner2(winterAnimal, 1);
                    break;
            }
        }

        else
        {
            switch (season)
            {
                case "SPRING":
                    spawner(hostileBird, 6);
                    if (weather == "RAINY") groundSpawner(rainyAnimal, 1);
                    break;

                case "SUMMER":
                    spawner(hostileBird, 5);
                    break;

                case "AUTUMN":
                    spawner(hostileBird, 5);
                    if (weather == "RAINY") groundSpawner(rainyAnimal, 2);
                    break;

                case "WINTER":
                    spawner(hostileBird, 0);
                    groundSpawner(winterAnimal, 4);
                    if (weather == "SNOWY") groundSpawner(winterAnimal, 2);
                    break;
            }
        }
    }

    public void spawner(GameObject spawn, int num)
    {
        for (int i = 0; i < num; i++)
        {
            float[] rangeX = { -2.5f, 2.5f };
            float randomX = rangeX[Random.Range(0, rangeX.Length)];

            float randomY = Random.Range(.75f, 1.5f);

            spawn = Instantiate(spawn, new Vector3(randomX, randomY), Quaternion.identity) as GameObject;

            spawn.transform.SetParent(animalHolder);
        }
    }

    public void groundSpawner(GameObject spawn, int num)
    {
        for (int i = 0; i < num; i++)
        {
            float[] rangeX = { -2.5f, 2.5f };
            float randomX = rangeX[Random.Range(0, rangeX.Length)];

            float randomY = Random.Range(0.075f, .25f);

            spawn = Instantiate(spawn, new Vector3(randomX, randomY), Quaternion.identity) as GameObject;

            spawn.transform.SetParent(animalHolder);
        }
    }

    public void groundSpawner2(GameObject spawn ,int num)
    {
        for (int i = 0; i < num; i++)
        {
            float[] rangeX = { -2.5f, -2f, -1.5f, -1f, -.5f, 0f, .5f, 1f, 1.5f, 2f, 2.5f };
            float randomX = rangeX[Random.Range(0, rangeX.Length)];

            spawn = Instantiate(spawn, new Vector3(randomX, -.6f), Quaternion.identity) as GameObject;

            spawn.transform.SetParent(animalHolder);
        }
    }

    public void spawnHigh(GameObject spawn, int num)
    {
        for (int i = 0; i < num; i++)
        {
            float[] rangeX = { -2.5f, -2f, -1.5f, -1f, -.5f, 0, .5f, 1f, 1.5f, 2f, 2.5f };
            float randomX = rangeX[Random.Range(0, rangeX.Length)];

            float randomY = Random.Range(2f, 3f);

            spawn = Instantiate(spawn, new Vector3(randomX, randomY), Quaternion.identity) as GameObject;

            spawn.transform.SetParent(animalHolder);
        }
    }
}
