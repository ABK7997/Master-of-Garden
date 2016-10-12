using UnityEngine;
using System.Collections;

public class Cactus : Plant {

    public override void setThirst(int num)
    {
        thirst += num;
        if (thirst < 0)
        {
            thirst = 0;
            setHealth(-1); //Thirsting
        }
        else if (thirst > maxThirst) thirst = maxThirst;

        float current = thirst * 1f;
        float max = maxThirst * 1f;
        float ratio = current / max;
        thirstBar.rectTransform.sizeDelta = new Vector2(ratio * barWidth, thirstBar.rectTransform.sizeDelta.y);
    }

    public override void setSunlight(int num)
    {
        statusText.text = "Warm";

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
                setHealth(-1);
                statusText.text = "Tolerating";
            }
        }

        //Freezing
        if (temp == TEMPERATURE.FREEZING && !warmed)
        {
            setHealth(-2);
            setHappiness(-1);
            statusText.text = "Freezing";
        }

        float current = sun * 1f;
        float max = maxSun * 1f;
        float ratio = current / max;
        sunBar.rectTransform.sizeDelta = new Vector2(ratio * barWidth, sunBar.rectTransform.sizeDelta.y);

        //Infestation
        if (infested) statusText.text = "Infested";
    }
}
