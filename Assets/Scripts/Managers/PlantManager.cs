using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
//using UnityEngine.SceneManagement;

public class PlantManager : MonoBehaviour {

    public GameObject[] allPlants;
    public int plantMax = 3;
    public int plantCount = 1;
    private int saveNum = 13;

    public GameObject newPlant;
    public GameObject[] plants;
    public List<Plant> scripts;
    public Text score;
    private int scoreNum = 0;

    public Store store;

    public SaveManager saveManager;

    void Start()
    {
        score.text = "Score: " + scoreNum;

        for (int i = 0; i < plants.Length; i++)
        {
            scripts[i].setOrder(i);
        }
    }

    public void cycle(string season, string weather, bool isDayTime)
    {
        foreach (Plant plant in scripts)
        {
            //Shroom
            if (plant.getSpecial() == "mushroom")
            {
                if (weather != "SUNNY")
                {
                    int hp = plant.getHealth();

                    plant.seasonal(season);
                    plant.setSpecial(false);
                    plant.setThirst(-2);
                    plant.setSunlight(-2);
                    plant.setGrowth(1);

                    if (plant.getHealth() == hp) plant.setHealth(2);

                    int hap = -1;
                    if (plant.getThirst() > 75) hap += 1;
                    if (plant.getSunlight() > 75) hap += 1;
                    plant.setHappiness(hap);
                }
                else {
                    plant.setSpecial(true);
                }
                continue;
            }

            else if (plant.getSpecial() == "new") continue;

            plant.seasonal(season);

            scoreNum += plant.calculateScore()/2;
            ItemManager.money += plant.getCoinage();

            //Health
            int hpBefore = plant.getHealth();

            //Thirst
            int thirst = 0;

            if (season == "SUMMER" && weather == "SUNNY" && isDayTime)
            {
                if (plant.getSpecial() == "antiburn" || plant.getSpecial() == "prickly") thirst = -2;
                else thirst = -4;
            }
            else if (weather == "RAINY")
            {
                thirst = 4;

                if (plant.getThirst() == 100 && !plant.isCovered())
                {
                    if (plant.getSpecial() == "antidrown")
                    {
                        plant.setHappiness(3);
                        plant.setHealth(2);
                    }
                    else if (plant.getSpecial() == "demigod" && plant.getMaturity() == "MATURE")
                    {
                        //
                    }
                    else //Drowning
                    {
                        plant.setHealth(-2);
                        plant.setHappiness(-1);
                    }
                }
            }
            else if (weather == "SNOWY")
            {
                thirst = 4;

                if (plant.getThirst() == 100 && !plant.isCovered())
                {
                    if (plant.getSpecial() == "antidrown" || plant.getSpecial() == "antifreeze")
                    {
                        plant.setHappiness(4);
                        plant.setHealth(3);
                    }
                    else if (plant.getSpecial() == "demigod" && plant.getMaturity() == "MATURE")
                    {
                        //
                    }
                    else //Drowning
                    {
                        plant.setHealth(-2);
                        plant.setHappiness(-1);
                    }
                }
            }

            else thirst = -2;

            if (plant.isCovered()) thirst = -3;
            plant.setThirst(thirst);

            //Sun
            if (plant.getSpecial() != "lunar")
            {
                int sun = 0;

                if (isDayTime) sun = 2;
                if (season == "SUMMER" && weather == "SUNNY" && isDayTime) sun *= 2;
                else if (weather == "RAINY" || weather == "SNOWY" || weather == "CLOUDY") sun = -2;

                if (plant.isCovered() || !isDayTime) sun = -2;
                plant.setSunlight(sun);
            }
            else //Lunar Rose
            {
                int sun = 0;

                if (!isDayTime) sun = 2;
                else sun = -2;

                if (weather != "SUNNY" || plant.isCovered()) sun = -2;

                plant.setSunlight(sun);
            }

            //Happiness
            int happy = -1;
            if (plant.getThirst() > 75) happy += 1;
            if (plant.getSunlight() > 75) happy += 1;

            plant.setHappiness(happy);

            //Growth
            plant.setGrowth(1);

            //Infestation
            plant.infestation();

            //Gain health if not otherwise taking damage
            plant.calculateHealth();

            if (plant.getHealth() == hpBefore) plant.setHealth(2);
        }

        score.text = "Score: " + scoreNum;
    }

    public void checkDeath()
    {
        bool allDead = true;

        for (int i = 0; i < plants.Length; i++)
        {
            if (scripts[i].getHealth() > 0 && scripts[i].getSpecial() != "new") allDead = false;
            else if (scripts[i].getSpecial() != "new")
            {
                GameObject deadPlant = plants[i];

                float x = plants[i].transform.position.x;
                float y = plants[i].transform.position.y;

                newPlant = Instantiate(newPlant, new Vector3(x, y), Quaternion.identity) as GameObject;
                plants[i] = newPlant;
                scripts[i] = newPlant.GetComponent<Plant>();

                Destroy(deadPlant.gameObject);
                plantCount--;
            }
        }

        //SceneManager.LoadScene(1);
        if (allDead)
        {
            saveManager.delete();
            Application.LoadLevel(1);
        }
    }

    //Daily reset
    public void resetAll()
    {
        foreach (Plant plant in scripts)
        {
            plant.reset();
        }
    }

    public List<Plant> getPlants()
    {
        return scripts;
    }

    public void addPlant(GameObject pot, GameObject plant)
    {
        for (int i = 0; i < plants.Length; i++)
        {
            if (plants[i] == pot)
            {
                float x = plants[i].transform.position.x;
                float y = plants[i].transform.position.y;
                GameObject oldPlant = plants[i];

                plant = Instantiate(plant, new Vector3(x, y), Quaternion.identity) as GameObject;

                scripts[i] = plant.GetComponent<Plant>();
                plants[i] = plant;

                plants[i].transform.position = new Vector3(x, y);
                Destroy(oldPlant);

                plantCount++;

                scripts[i].setOrder(i);
            }
        }
    }

    public void loadPlant(GameObject pot, GameObject plant, int[] data)
    {
        for (int i = 0; i < plants.Length; i++)
        {
            if (scripts[i].getOrder() == data[11])
            {
                float x = plants[i].transform.position.x;
                float y = plants[i].transform.position.y;
                GameObject oldPlant = plants[i];

                plant = Instantiate(plant, new Vector3(x, y), Quaternion.identity) as GameObject;

                scripts[i] = plant.GetComponent<Plant>();
                plants[i] = plant;

                plants[i].transform.position = new Vector3(x, y);
                Destroy(oldPlant);

                scripts[i].loadStats(data);

                break;
            }
        }
    }

    public void sellPlant(GameObject newPlant, GameObject plant)
    {
        for (int i = 0; i < plants.Length; i++)
        {
            if (plants[i] == plant) {
                GameObject deadPlant = plants[i];

                float x = plants[i].transform.position.x;
                float y = plants[i].transform.position.y;

                newPlant = Instantiate(newPlant, new Vector3(x, y), Quaternion.identity) as GameObject;
                plants[i] = newPlant;
                scripts[i] = newPlant.GetComponent<Plant>();

                Destroy(deadPlant.gameObject);
                plantCount--;
            }
        }
    }

    public int getMax()
    {
        return plantMax;
    }

    public int getCount()
    {
        return plantCount;
    }

    //SAVING AND LOADING
    public void save(string stageName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + stageName + "Garden.dat");

        PMData data = new PMData();

        data.plants = new int[plantMax, saveNum];

        for (int i = 0; i < plants.Length; i++)
        {
            for (int j = 0; j < saveNum; j++)
            data.plants[i, j] = scripts[i].save()[j];
        }

        data.scoreNum = scoreNum;
        data.plantCount = plantCount;

        bf.Serialize(file, data);
        file.Close();
    }

    public void load(string stageName)
    {
        if (File.Exists(Application.persistentDataPath + "/" + stageName + "Garden.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + stageName + "Garden.dat", FileMode.Open);

            PMData data = bf.Deserialize(file) as PMData;
            file.Close();

            for (int i = 0; i < plants.Length; i++)
            {
                int[] stats = new int[saveNum];
                for (int j = 0; j < saveNum; j++)
                {
                    stats[j] = data.plants[i, j];
                }
                scripts[i].load(stats);
            }

            scoreNum = data.scoreNum;
            plantCount = data.plantCount;
        }
    }

    public void delete(string stageName)
    {
        if (File.Exists(Application.persistentDataPath + "/" + stageName + "Garden.dat"))
        {
            File.Delete(Application.persistentDataPath + "/" + stageName + "Garden.dat");
        }
    }

    //All Plants
    public GameObject[] getAll()
    {
        return allPlants;
    }

    public Store getStore()
    {
        return store;
    }

    //Score
    public int getScore()
    {
        return scoreNum;
    }
}

[Serializable]
class PMData
{
    public int[,] plants;

    public int scoreNum, plantCount;


}
