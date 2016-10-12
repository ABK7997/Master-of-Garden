using UnityEngine;
using System.Collections;

public class FrostDandalion : Plant
{
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
                setHealth(-3);
                setHappiness(-2);
                statusText.text = "Melting";
            }
        }

        //Freezing
        if (temp == TEMPERATURE.FREEZING && !warmed) statusText.text = "Cool";

        float current = sun * 1f;
        float max = maxSun * 1f;
        float ratio = current / max;
        sunBar.rectTransform.sizeDelta = new Vector2(ratio * barWidth, sunBar.rectTransform.sizeDelta.y);

        //Infestation
        if (infested) statusText.text = "Infested";
    }

    public override void setHappiness(int num)
    {

        if (num > 0) happiness += (num / 2);
        else happiness += num;

        if (happiness < 0) happiness = 0;
        else if (happiness > maxHappiness) happiness = maxHappiness;

        float current = happiness * 1f;
        float max = maxHappiness * 1f;
        float ratio = current / max;
        happinessBar.rectTransform.sizeDelta = new Vector2(ratio * barWidth, happinessBar.rectTransform.sizeDelta.y);
    }

}
