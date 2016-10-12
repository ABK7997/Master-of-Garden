using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeatherManager : MonoBehaviour {

    //Background Panel
    public Image filter;

    //Weather Background
    public SpriteRenderer background;
    public Sprite none;
    public Sprite rain1, rain2;
    public Sprite snow1, snow2;

    //Clouds
    public Transform cloudHolder;
    public GameObject cloud;

    private enum CLOUDCOVER
    {
        NONE, LIGHT, PARTIAL, HEAVY, OVERCAST
    }
    private CLOUDCOVER cloudCover = CLOUDCOVER.LIGHT;

    //Timer
    private float timer = 0f;

    //State
    private enum WEATHER
    {
        SUNNY, CLOUDY, RAINY, SNOWY
    }
    private WEATHER weather = WEATHER.SUNNY;

    void Start()
    {
        spawnCloud();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > .75f)
        {
            timer = 0f;

            switch (weather)
            {
                case WEATHER.RAINY:
                    if (background.sprite == rain1) background.sprite = rain2;
                    else background.sprite = rain1;
                    break;

                case WEATHER.SNOWY:
                    if (background.sprite == snow1) background.sprite = snow2;
                    else background.sprite = snow1;
                    break;
            }
        }

        
    }

    public void cycle(string season)
    {
        int chance = Random.Range(0, 101);

        switch(season)
        {
            case "SPRING":
                if (weather == WEATHER.SUNNY)
                {
                    if (chance < 10)
                    {
                        weather = WEATHER.CLOUDY; //10% chance of Cloudy
                        cloudCover = CLOUDCOVER.HEAVY;
                    }
                    else cloudCover = CLOUDCOVER.LIGHT;
                }

                else if (weather == WEATHER.CLOUDY)
                {
                    if (chance < 25) //25% chance of Rainy
                    {
                        weather = WEATHER.RAINY;
                        cloudCover = CLOUDCOVER.OVERCAST;
                    }
                    else if (chance < 75) //50% chance of Sunny
                    {
                        weather = WEATHER.SUNNY;
                        cloudCover = CLOUDCOVER.LIGHT;
                    }
                    //25% chance REMAINS Cloudy
                }

                else if (weather == WEATHER.RAINY)
                {
                    if (chance < 50) //50% chance of Cloudy
                    {
                        weather = WEATHER.CLOUDY;
                        cloudCover = CLOUDCOVER.HEAVY;
                    }
                    else if (chance < 70) //20% chance of IMMEDIATELY Sunny
                    {
                        weather = WEATHER.SUNNY;
                        cloudCover = CLOUDCOVER.LIGHT;
                    }
                    //30% chance REMAINS Cloudy
                }

                else if (weather == WEATHER.SNOWY) //IMMEDIATELY Sunny if still Snowy at the end of Winter
                {
                    weather = WEATHER.SUNNY;
                    cloudCover = CLOUDCOVER.LIGHT;
                }

                break;

            case "SUMMER":
                if (weather == WEATHER.SUNNY)
                {
                    if (chance < 10) //10% chance of CLOUDY
                    {
                        weather = WEATHER.CLOUDY;
                        cloudCover = CLOUDCOVER.HEAVY;
                    }
                    else cloudCover = CLOUDCOVER.NONE;
                    //Otherwise REMAINS Sunny
                }

                else if (weather == WEATHER.CLOUDY)
                {
                    if (chance < 40) //40$ chance of Sunny
                    {
                        weather = WEATHER.SUNNY;
                        cloudCover = CLOUDCOVER.NONE;
                    }
                    //otherwise REAMINS Cloudy
                }

                else if (weather == WEATHER.RAINY) //Always return to Cloudy
                {
                    weather = WEATHER.CLOUDY;
                    cloudCover = CLOUDCOVER.HEAVY;
                }
                break;

            case "AUTUMN":
                if (weather == WEATHER.SUNNY)
                {
                    if (chance < 60) //60% chance of Cloudy
                    {
                        weather = WEATHER.CLOUDY;
                        cloudCover = CLOUDCOVER.HEAVY;
                    }
                    else if (chance < 80) //20% chance of IMMEDIATE Rainy
                    {
                        weather = WEATHER.RAINY;
                        cloudCover = CLOUDCOVER.OVERCAST;
                        spawnCloud(); spawnCloud();
                    }
                    //Otherwise REMAINS Sunny
                }

                else if (weather == WEATHER.CLOUDY)
                {
                    if (chance < 70) //70% chance of Rainy
                    {
                        weather = WEATHER.RAINY;
                        cloudCover = CLOUDCOVER.OVERCAST;
                    }
                    else if (chance < 85) //10% chance of Sunny
                    {
                        weather = WEATHER.SUNNY;
                        cloudCover = CLOUDCOVER.LIGHT;
                    }
                }

                else if (weather == WEATHER.RAINY)
                {
                    if (chance < 30) //30% chance of Cloudy
                    {
                        cloudCover = CLOUDCOVER.HEAVY;
                    }
                    //Otherwise REMAINS Rainy
                }
                break;

            case "WINTER":
                if (weather == WEATHER.SUNNY)
                {
                    if (chance < 80) //80% chance of Cloudy
                    {
                        weather = WEATHER.CLOUDY;
                        cloudCover = CLOUDCOVER.HEAVY;
                    }
                    else if (chance < 90) //10% chance of IMMEDIATE Snowy
                    {
                        weather = WEATHER.SNOWY;
                        cloudCover = CLOUDCOVER.OVERCAST;
                        spawnCloud(); spawnCloud();
                    }
                    //Otherwise REMAINS Sunny
                }

                else if (weather == WEATHER.CLOUDY)
                {
                    if (chance < 90) //90% chance of Snowy
                    {
                        weather = WEATHER.SNOWY;
                        cloudCover = CLOUDCOVER.OVERCAST;
                    }
                    else if (chance < 95) //5% chance of Sunny
                    {
                        weather = WEATHER.SUNNY;
                        cloudCover = CLOUDCOVER.PARTIAL;
                    }
                    //Otherwise REMAINS Cloudy
                }

                else if (weather == WEATHER.SNOWY)
                {
                    if (chance < 30) //30% chance of Cloudy 
                    {
                        weather = WEATHER.CLOUDY;
                        cloudCover = CLOUDCOVER.HEAVY;
                    }
                    else if (chance < 35) //5% chance of IMMEDIATE Sunny
                    {
                        weather = WEATHER.SUNNY;
                        cloudCover = CLOUDCOVER.PARTIAL;
                    }
                    //Otherwise REMAINS Snowy
                }

                else if (weather == WEATHER.RAINY)
                {
                    weather = WEATHER.SNOWY;
                    cloudCover = CLOUDCOVER.OVERCAST;
                }

                break;
        }

        changeWeather();
    }

    public void changeWeather()
    {
        switch(weather)
        {
            case WEATHER.SUNNY: background.sprite = none; filter.color = new Color(0, 0, 0, 0); break;
            case WEATHER.CLOUDY: background.sprite = none; filter.color = new Color(0, 0, 0, .2f); break;
            case WEATHER.RAINY: background.sprite = rain1; filter.color = new Color(0, 0, 0, .3f); break;
            case WEATHER.SNOWY: background.sprite = rain2; filter.color = new Color(0, 0, 0, .3f); break;
        }
    }

    public string getWeather()
    {
        return "" + weather;
    }

    //CLOUDS
    public void spawnCloud()
    {
        int quantity = 0;

        switch (cloudCover)
        {
            case CLOUDCOVER.NONE: quantity = Random.Range(0, 0); break;
            case CLOUDCOVER.LIGHT: quantity = Random.Range(0, 2); break;
            case CLOUDCOVER.PARTIAL: quantity = Random.Range(1, 2); break;
            case CLOUDCOVER.HEAVY: quantity = Random.Range(3, 5); break;
            case CLOUDCOVER.OVERCAST: quantity = Random.Range(4, 7); break;
        }

        for (int i = 0; i < quantity; i++)
        {
            float randomY = Random.Range(.75f, 1.5f);
            GameObject instance = Instantiate(cloud, new Vector3(-2.3f, randomY), Quaternion.identity) as GameObject;

            instance.transform.SetParent(cloudHolder);
        }
    }

    //LOAD
    public void loadWeather(string w)
    {
        switch (w)
        {
            case "SUNNY": background.sprite = none; filter.color = new Color(0, 0, 0, 0); weather = WEATHER.SUNNY; break;
            case "CLOUDY": background.sprite = none; filter.color = new Color(0, 0, 0, .2f); weather = WEATHER.CLOUDY; break;
            case "RAINY": background.sprite = rain1; filter.color = new Color(0, 0, 0, .3f); weather = WEATHER.RAINY; break;
            case "SNOWY": background.sprite = rain2; filter.color = new Color(0, 0, 0, .3f); weather = WEATHER.SNOWY; break;
        }
    }

    public void secondLoad(string season)
    {
        switch (season)
        {
            case "SPRING":
                if (weather == WEATHER.SUNNY) cloudCover = CLOUDCOVER.LIGHT;
                else if (weather == WEATHER.CLOUDY) cloudCover = CLOUDCOVER.HEAVY;
                else if (weather == WEATHER.RAINY) cloudCover = CLOUDCOVER.OVERCAST;
                else if (weather == WEATHER.SNOWY) //IMMEDIATELY Sunny if still Snowy at the end of Winter
                {
                    weather = WEATHER.SUNNY;
                    cloudCover = CLOUDCOVER.LIGHT;
                }
                break;

            case "SUMMER":
                if (weather == WEATHER.SUNNY) cloudCover = CLOUDCOVER.NONE;
                else if (weather == WEATHER.CLOUDY) cloudCover = CLOUDCOVER.HEAVY;
                else if (weather == WEATHER.RAINY) //Always return to Cloudy
                {
                    weather = WEATHER.CLOUDY;
                    cloudCover = CLOUDCOVER.HEAVY;
                }
                break;

            case "AUTUMN":
                if (weather == WEATHER.SUNNY) cloudCover = CLOUDCOVER.PARTIAL;
                else if (weather == WEATHER.CLOUDY) cloudCover = CLOUDCOVER.HEAVY;
                else if (weather == WEATHER.RAINY) cloudCover = CLOUDCOVER.OVERCAST;
                break;

            case "WINTER":
                if (weather == WEATHER.SUNNY) cloudCover = CLOUDCOVER.PARTIAL;
                else if (weather == WEATHER.CLOUDY) cloudCover = CLOUDCOVER.HEAVY;
                else if (weather == WEATHER.SNOWY) cloudCover = CLOUDCOVER.OVERCAST;
                else if (weather == WEATHER.RAINY)
                {
                    weather = WEATHER.SNOWY;
                    cloudCover = CLOUDCOVER.OVERCAST;
                }
                break;
        }
    }
}
