using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LunaSol : MonoBehaviour {

    //Season/Weather/Beast
    public SeasonManager sm;
    public WeatherManager wm;
    public BeastManager bm;
    public SaveManager saveManager;

    //Day Counter
    public Text days;
    private float dayNum = 0f;

    private float timer = -90f;
    private Animator anim;

    private bool beastTrigger = true;

    //Sky Color
    public Camera cam;
    private float red = 1f, green = .4f, blue = 0f;
    private bool day = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        days.text = "Day: " + dayNum;
    }

	public void update(PlantManager pm)
    {
        if (!Menu.running) return;

        timer += 6 * Time.deltaTime;

        Vector3 newPos = new Vector3(0f, 0f);
        newPos.x = Mathf.Cos((timer * Mathf.PI) / 180f) * 1.5f;
        newPos.y = Mathf.Sin((timer * Mathf.PI) / 180f) * 1.5f;

        transform.position = newPos;
        skyColor(); triggers();

        if (timer > 270)
        {
            timer = -90f;
            day = !day;
            dayNum += 0.5f;
            days.text = "Day: " + dayNum;

            sm.update(dayNum);
            wm.cycle(sm.getSeason());

            if (day)
            {
                bm.newDawn(sm.getSeason(), wm.getWeather(), pm.getPlants());
                saveManager.save();
            }
            else
            {
                bm.newDusk(sm.getSeason(), wm.getWeather(), pm.getPlants());
            }

            beastTrigger = true;
        }

        else if (timer > 90 && beastTrigger)
        {
            beastTrigger = false;
            if (day) bm.newDay(sm.getSeason(), wm.getWeather(), pm.getPlants());
            else bm.newNight(sm.getSeason(), wm.getWeather(), pm.getPlants());
        }
    }

    public void skyColor()
    {
        float rate = Time.deltaTime / 15f;

        if (day)
        {
            //Dawn to Day
            if (timer > -90 && timer < 0)
            {
                red -= rate;
                green -= .1f * rate;
                blue += rate;
            }

            //Day to Dusk
            else if (timer > 180 && timer < 270)
            {
                red += rate;
                green += .1f * rate;
                blue -= rate;
            }
        }

        else
        {
            //Dusk to Night
            if (timer > -90 && timer < 0)
            {
                red -= .8f * rate;
                green -= .2f * rate;
                blue += .2f * rate;
            }
            
            //Night to Dawn
            else if (timer > 180 && timer < 270)
            {
                red += .8f * rate;
                green += .2f * rate;
                blue -= .2f * rate;
            }
        }

        cam.backgroundColor = new Color(red, green, blue);
    }

    public void triggers()
    {
        if (day)
        {
            if (timer > 0 && timer < 140) anim.SetInteger("Time", 1);
            else if (timer > 140) anim.SetInteger("Time", 2);
        }
        else
        {
            if (timer > 20 && timer < 140) anim.SetInteger("Time", 3);
            else if (timer > 140) anim.SetInteger("Time", 4);
        }
    }

    public bool isDaytime()
    {
        return day;
    }

    public float getDays()
    {
        return dayNum;
    }

    public void setDayNum(float num)
    {
        dayNum = num;

        days.text = "Days: " + num;
    }
}
