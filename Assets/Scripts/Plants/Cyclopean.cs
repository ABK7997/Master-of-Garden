using UnityEngine;
using System.Collections;

public class Cyclopean : Plant {

	public override void setHealth(int num)
    {
        if (num > 0) health += num;

        base.setHealth(num);
    }

}
