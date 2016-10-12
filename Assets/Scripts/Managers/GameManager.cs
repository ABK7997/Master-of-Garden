using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //Main Menu
    //public Canvas menu;

    //Season and Weather Display
    public Text display;

    //Cycle Management
    public static float rate = 1f;
    private float timer = 0f;
    private int hours = -6;

    //Managers
    public Menu menu;
    public PlantManager pm;
    public BeastManager bm;
    public ItemManager im;
    public SeasonManager sm;
    public WeatherManager wm;

    public LunaSol lunaSol;

	// Update is called once per frame
	void Update () {
        if (!Menu.running) return;

        timer += rate * Time.deltaTime;

        //Regular updates
        lunaSol.update(pm);

        if (timer > 2.5f)
        {
            hours++;
            timer = 0f;

            pm.cycle(sm.getSeason(), wm.getWeather(), lunaSol.isDaytime());
            wm.spawnCloud();

            if (timer == 24)
            {
                hours = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.toggleMenu();
        }

        pm.checkDeath();

        display.text = sm.getSeason() + ", " + wm.getWeather();
	}
}
