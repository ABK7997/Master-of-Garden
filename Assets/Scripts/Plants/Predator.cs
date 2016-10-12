using UnityEngine;
using System.Collections;

public class Predator : Plant {

    public override void setSpecial(bool b)
    {
        specialing = b;

        anim.SetTrigger("Biting");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Pest")
        {
            ItemManager.money += 5;
            Destroy(other.gameObject);
            setSpecial(true);
        }
        else if (other.gameObject.tag == "Hostile" && maturity != MATURITY.SAPLING)
        {
            ItemManager.money += 1;
            Destroy(other.gameObject);
            setSpecial(true);
        }
        else if (other.gameObject.tag == "Hunter" && maturity == MATURITY.MATURE)
        {
            ItemManager.money += 2;
            Destroy(other.gameObject);
            setSpecial(true);
        }
    }

}
