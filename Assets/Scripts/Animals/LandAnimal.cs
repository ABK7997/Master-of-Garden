using UnityEngine;
using System.Collections;

public class LandAnimal : Animal {

    private float idleTimer = 0f;

    public float minWait, maxWait;
    private float waitTime;

    private bool mountainAnimal = false;

    private enum STATE
    {
        IDLING, MOVING, FLEEING
    }
    private STATE state = STATE.IDLING;

    protected override void Start()
    {
        base.Start();

        waitTime = Random.Range(minWait, maxWait);

        if (transform.position.y < 0) mountainAnimal = true;
    }

    protected override void Update()
    {
        if (!Menu.running) return;

        base.Update();

        switch (state)
        {
            case STATE.IDLING: idling(); break;
            case STATE.MOVING: moving(); break;
            case STATE.FLEEING: moveAway(); break;
        }
    }

    private void idling()
    {
        idleTimer += Time.deltaTime;

        if (idleTimer > waitTime)
        {
            idleTimer = 0;
            anim.SetBool("Idling", false);
            state = STATE.MOVING;
        }
    }

    private void moving()
    {
        if (!checkRange())
        {
            idleTimer += Time.deltaTime;
            moveTowardsPlant();

            if (idleTimer > waitTime)
            {
                idleTimer = 0;
                anim.SetBool("Idling", true);
                state = STATE.IDLING;
            }
        }
        else
        {
            action(target);
            state = STATE.FLEEING;
        }
    }

    protected override void moveTowardsPlant()
    {
        float time = 7.5f;

        Vector3 bug = transform.position;
        Vector3 t = target.transform.position;
        Vector3 newPos = new Vector3(0f, 0f);

        if (bug.x < t.x) newPos.x += distanceX * (Time.deltaTime / time) * speed;
        else newPos.x -= distanceX * (Time.deltaTime / time) * speed;

        if (bug.y < t.y) newPos.y += distanceY * (Time.deltaTime / time) * speed;
        else newPos.y -= distanceY * (Time.deltaTime / time) * speed;

        transform.position += newPos;
    }

    protected override void moveAway()
    {
        Vector3 newPos = new Vector3(0f, 0f);

        if (mountainAnimal) newPos.y -= (Time.deltaTime / 1.5f) * speed;
        else if (direction == 0) newPos.x += (Time.deltaTime / 1.5f) * speed;
        else newPos.x -= (Time.deltaTime / 1.5f) * speed;

        transform.position += newPos;

        if (Mathf.Abs(transform.position.x) >= 3) Destroy(gameObject);
    }

    //EFFECT
    protected override void action(Plant plant)
    {
        switch (effect)
        {
            case "pollinator": plant.pollinate(); break;
            case "harm":
                if (plant.getSpecial() == "ghost") plant.setHealth(1);
                else if (plant.getSpecial() == "prickly" && plant.getMaturity() != "SAPLING")
                {
                    ItemManager.money += reward;
                    plant.setHealth(-3);
                    Destroy(gameObject);
                }
                else plant.setHealth(-5);
                break;
        }

        switchAnimation();
    }

    private void switchAnimation()
    {
        if (direction == 0) direction = 1;
        else direction = 0;
        anim.SetInteger("Direction", direction);
        anim.SetBool("Idling", false);
        idleTimer = 0f;
    }
}
