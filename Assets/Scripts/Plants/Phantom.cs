using UnityEngine;
using System.Collections;

public class Phantom : Plant {

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

    }

    public override void setGrowth(int num)
    {

    }

    public override void setThirst(int num)
    {

    }

    public override void setSunlight(int num)
    {

    }

    public override string getMaturity()
    {
        return "MATURE";
    }

    public override void setInfested(bool boolean)
    {
        infested = false;
    }

    public override bool isPollinated()
    {
        return true;
    }

    public override void setHappiness(int num)
    {
        happiness -= 2;

        happiness += num;
        if (happiness < 0) happiness = 0;
        else if (happiness > maxHappiness) happiness = maxHappiness;

        float current = happiness * 1f;
        float max = maxHappiness * 1f;
        float ratio = current / max;
        happinessBar.rectTransform.sizeDelta = new Vector2(ratio * barWidth, happinessBar.rectTransform.sizeDelta.y);

        if (happiness < 25) statusText.text = "Miserable";
        else if (happiness < 50) statusText.text = "Discontent";
        else if (happiness < 75) statusText.text = "Reasonably Content";
        else if (happiness < 90) statusText.text = "Happy";
        else statusText.text = "Exuberant";
    }

    public override int calculateScore()
    {
        return 0;
    }
}
