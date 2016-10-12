using UnityEngine;
using System.Collections;

public class UndeadPlant : Plant {

    protected override void Awake()
    {
        base.Awake();

        base.setHealth(maxHealth);
        base.setGrowth(0);
        base.setThirst(maxThirst);
        base.setSunlight(maxSun);
        base.setHappiness(maxHappiness);
    }

    public override void setHealth(int num)
    {
        health += (num / 2);

        if (health < 0) health = 0;
        else if (health > maxHealth) health = maxHealth;

        float current = health * 1f;
        float max = maxHealth * 1f;
        float ratio = current / max;
        healthBar.rectTransform.sizeDelta = new Vector2(ratio * barWidth, healthBar.rectTransform.sizeDelta.y);
    }

    public override void setGrowth(int num)
    {
        if (num > 1) growth += (num / 2);
        else growth += num;

        if (growth < 0) growth = 0;
        else if (growth > maxGrowth) growth = maxGrowth;

        if (getGrowth() == 100 && pollinated) maturation();

        float current = growth * 1f;
        float max = maxGrowth * 1f;
        float ratio = current / max;
        growthBar.rectTransform.sizeDelta = new Vector2(ratio * barWidth, growthBar.rectTransform.sizeDelta.y);
    }

    public override void setThirst(int num)
    {
        if (num > 0) thirst += (num / 2);
        else thirst += num;

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

    public override void setSunlight(int num)
    {
        statusText.text = "Warm";
        if (num > 0)
        {
            setHealth(-1);
            sun += num;
        }
        else sun += num;

        if (sun < 0) sun = 0;
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

    public override int calculateScore()
    {
        int score = 0;

        score += getThirst() / 4;
        score += getSunlight() / 4;

        if (getHappiness() > 75) score *= 2;
        else if (getHappiness() > 50) score = (int)(score * 1.5);
        else if (getHappiness() <= 25) score = (int)(score * .5);
        else if (getHappiness() == 0) score = (int)(score * .25);

        if (maturity == MATURITY.JUVENILE) score = (int)(score * 1.5);
        else if (maturity == MATURITY.MATURE) score = score * 2;

        return score*2;
    }
}
