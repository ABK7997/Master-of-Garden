using UnityEngine;
using System.Collections;

public class Trapper : Plant
{
    private float timer = 0f;

    protected override void Update()
    {
        base.Update();

        if (specialing)
        {
            timer += Time.deltaTime;

            if (timer > 25)
            {
                setSpecial(false);
                timer = 0f;
            }
        }
    }

    public override void setSpecial(bool b)
    {
        specialing = b;

        if (specialing) anim.SetBool("Closed", true);
        else anim.SetBool("Closed", false);
    }
}
