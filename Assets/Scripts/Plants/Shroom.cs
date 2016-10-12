using UnityEngine;
using System.Collections;

public class Shroom : Plant {

    void Start()
    {
        specialing = true;
    }

    public override void setSpecial(bool b)
    {
        specialing = b;

        if (specialing)
        {
            anim.SetBool("Asleep", true);
        }
        else
        {
            anim.SetBool("Asleep", false);
        }
    }

    public override void setGrowth(int num)
    {
        growth += num;
        if (growth < 0) growth = 0;
        else if (growth > maxGrowth) maturation();

        float current = growth * 1f;
        float max = maxGrowth * 1f;
        float ratio = current / max;
        growthBar.rectTransform.sizeDelta = new Vector2(ratio * barWidth, growthBar.rectTransform.sizeDelta.y);
    }

    public override void setSunlight(int num)
    {
        if (specialing) return;

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
                setHealth(-2);
                setHappiness(-1);
                statusText.text = "Overheating";
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

    public override float getCoinage()
    {
        if (!specialing) return coinage;
        else return 0;
    }

    public override bool isPollinated()
    {
        return true;
    }

    public override string getMaturity()
    {
        return "MATURE";
    }
}
