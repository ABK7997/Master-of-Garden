using UnityEngine;
using System.Collections;

public class JoyFlower : Plant {

    public override void setHappiness(int num)
    {
        if (num > 0) happiness += num;

        happiness += num;
        if (happiness < 0) happiness = 0;
        else if (happiness > maxHappiness) happiness = maxHappiness;

        float current = happiness * 1f;
        float max = maxHappiness * 1f;
        float ratio = current / max;
        happinessBar.rectTransform.sizeDelta = new Vector2(ratio * barWidth, happinessBar.rectTransform.sizeDelta.y);
    }

}
