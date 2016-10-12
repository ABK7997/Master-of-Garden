using UnityEngine;
using System.Collections.Generic;

public class Bird : Animal
{
    private float attackTimer = 0;

    public int minWait, maxWait;
    private int waitTime;

    private enum STATE
    {
        IDLING, ATTACKING, FLEEING
    }
    private STATE state = STATE.IDLING;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        waitTime = Random.Range(minWait, maxWait + 1); //Determines how long a bird while idle before going towards plant
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!Menu.running) return;

        base.Update();

        switch (state)
        {
            case STATE.IDLING: idling(); break;
            case STATE.ATTACKING:
                if (!checkRange()) moveTowardsPlant();
                else
                {
                    action(target);
                    state = STATE.FLEEING;
                }
                break;
            case STATE.FLEEING: moveAway(); break;
        }
    }

    private void idling()
    {
        Vector3 newPos = new Vector3(0f, 0f);

        if (direction == 0)
        {
            if (transform.position.x < 2.25f) newPos.x += distanceX * (Time.deltaTime / 5f) * speed;
            else switchAnimation();
        }
        else
        {
            if (transform.position.x > -2.25f) newPos.x -= distanceX * (Time.deltaTime / 5f) * speed;
            else switchAnimation();
        }
        transform.position += newPos;

        //ATTACK TIMER
        attackTimer += Time.deltaTime;
        if (attackTimer >= waitTime)
        {
            state = STATE.ATTACKING;
            distanceX = Mathf.Abs(target.transform.position.x - transform.position.x);
            distanceY = Mathf.Abs(target.transform.position.y - transform.position.y);

            if (transform.position.x <= target.transform.position.x)
            {
                direction = 0;
                anim.SetInteger("Direction", 0);
            }
            else
            {
                direction = 1;
                anim.SetInteger("Direction", 1);
            }
        }
    }

    //EFFECT
    protected override void action(Plant plant)
    {
        switch (effect)
        {
            case "pollinator": plant.pollinate(); break;
            case "harm":
                if (plant.getSpecial() == "carnivore" && !plant.isSpecialing() && plant.getMaturity() == "MATURE")
                {
                    plant.setHealth(30);
                    plant.setThirst(30);
                    plant.setGrowth(30);
                    plant.setHappiness(50);
                    plant.setSpecial(true);
                    ItemManager.money += reward;
                    Destroy(gameObject);
                }
                else if (plant.getSpecial() == "ghost") plant.setHealth(1);
                else if (plant.getSpecial() == "prickly" && plant.getMaturity() != "SAPLING")
                {
                    ItemManager.money += reward;
                    plant.setHealth(-3);
                    Destroy(gameObject);
                }
                else plant.setHealth(-5);
                break;
        }
    }

    public void switchAnimation()
    {
        if (direction == 0)
        {
            direction = 1;
            anim.SetInteger("Direction", 1);
        }
        else
        {
            direction = 0;
            anim.SetInteger("Direction", 0);
        }
    }
}
