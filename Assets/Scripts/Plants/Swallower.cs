using UnityEngine;
using System.Collections;

public class Swallower : Plant
{
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
