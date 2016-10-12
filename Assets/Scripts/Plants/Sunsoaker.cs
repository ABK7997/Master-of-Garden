using UnityEngine;
using System.Collections;

public class Sunsoaker : Plant {

    public override void setSunlight(int num)
    {
        if (num > 0) sun += num; //Doubles sunlight intake

        statusText.text = "Warm";

        //Cover and Hotness
        if (covered) sun -= 2;

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
                setHealth(2);
                setHappiness(3);
                statusText.text = "Sunbathing";
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
