using UnityEngine;
using System.Collections;

public class Cthulu : Plant {

    public override void setHealth(int num)
    {
        if (num > 0) health += (num / 2);

        health += num;
        if (health < 0) health = 0;
        else if (health > maxHealth) health = maxHealth;

        float current = health * 1f;
        float max = maxHealth * 1f;
        float ratio = current / max;
        healthBar.rectTransform.sizeDelta = new Vector2(ratio * barWidth, healthBar.rectTransform.sizeDelta.y);
    }

    public override void setSunlight(int num)
    {
        statusText.text = "Warm";

        if (num > 0) sun += num;

        //Hotness
        sun += num;
        if (sun < 0)
        {
            setHealth(-1);
            sun = 0;
        }
        else if (sun > maxSun)
        {
            sun = maxSun;
            if (temp == TEMPERATURE.HOT)
            {
                if (maturity == MATURITY.MATURE) statusText.text = "Unfazed";
                else {
                    setHealth(-2);
                    setHappiness(-1);
                    statusText.text = "Overheating";
                }
            }
        }

        //Freezing
        if (temp == TEMPERATURE.FREEZING && !warmed)
        {
            if (maturity == MATURITY.MATURE) statusText.text = "Unfazed";
            else
            {
                setHealth(-2);
                setHappiness(-1);
                statusText.text = "Freezing";
            }
        }

        float current = sun * 1f;
        float max = maxSun * 1f;
        float ratio = current / max;
        sunBar.rectTransform.sizeDelta = new Vector2(ratio * barWidth, sunBar.rectTransform.sizeDelta.y);

        //Infestation
        if (infested) statusText.text = "Infested";
    }

    public override void setThirst(int num)
    {
        if (num > 0) thirst += num;

        thirst += num;
        if (thirst < 0)
        {
            thirst = 0;
            setHealth(-2); //Thirsting
            setHappiness(-1);
        }
        else if (thirst > maxThirst) thirst = maxThirst;

        float current = thirst * 1f;
        float max = maxThirst * 1f;
        float ratio = current / max;
        thirstBar.rectTransform.sizeDelta = new Vector2(ratio * barWidth, thirstBar.rectTransform.sizeDelta.y);
    }

}
