using UnityEngine;
using System.Collections;

public class Crystals : Plant {

    protected override void Awake()
    {
        base.Awake();

        base.setHealth(maxHealth);
    }

    public override void setHealth(int num)
    {
        if (num < 0) health += num;
        if (health < 0) health = 0;
        else if (health > maxHealth) health = maxHealth;

        float current = health * 1f;
        float max = maxHealth * 1f;
        float ratio = current / max;
        healthBar.rectTransform.sizeDelta = new Vector2(ratio * barWidth, healthBar.rectTransform.sizeDelta.y);
    }

    public override void maturation()
    {
        anim.SetTrigger("Growth");
        growth = 0;
        if (maturity != MATURITY.MATURE)
        {
            coinage *= 1.5f;
            maxGrowth *= 2;
        }

        coinText.text = "+$" + coinage;
        pollinated = false;
        happiness = maxHappiness;

        //Bonus for pollinated plant if Plant is already fully mature
        if (maturity == MATURITY.MATURE)
        {
            ItemManager.money += 500;
        }

        switch (maturity)
        {
            case MATURITY.SAPLING: maturity = MATURITY.JUVENILE; break;
            case MATURITY.JUVENILE: maturity = MATURITY.MATURE; break;
            default: return;
        }
    }

}
