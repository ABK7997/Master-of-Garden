using UnityEngine;
using System.Collections.Generic;
using System;

public class Animal : MonoBehaviour {

    protected List<Plant> plants = new List<Plant>();
    protected Plant target;

    //Bestiary info
    public Sprite sprite;
    public string description;

    public float reward;

    public float minSpeed, maxSpeed;
    protected float speed;
    protected float distanceX, distanceY;

    public string effect;
    protected bool hovering = false;

    protected Animator anim;
    protected int direction;

    // Use this for initialization
    protected virtual void Start () {
        anim = GetComponent<Animator>();

        Plant[] targets = FindObjectsOfType<Plant>();
        for (int i = 0; i < targets.Length; i++)
        {
            plants.Add(targets[i]);
        }
        findRandomPlant();

        if (transform.position.x < target.transform.position.x)
        {
            anim.SetInteger("Direction", 0);
            direction = 0;
        }
        else
        {
            anim.SetInteger("Direction", 1);
            direction = 1;
        }

        //Distance from targeted plant
        distanceX = Mathf.Abs(target.transform.position.x - transform.position.x);
        distanceY = Mathf.Abs(target.transform.position.y - transform.position.y);

        //Speed
        speed = UnityEngine.Random.Range(minSpeed, maxSpeed + 1);
    }
	
	// Update is called once per frame
	protected virtual void Update () {
        if (!Menu.running) return;

        //Swatting
        if (hovering && Input.GetMouseButtonDown(0) && Tool.action == Tool.ACTION.SWATTER)
        {
            ItemManager.money += reward;

            Destroy(gameObject);
        }
    }

    protected virtual void OnMouseOver()
    {
        hovering = true;
    }
    protected virtual void OnMouseExit()
    {
        hovering = false;
    }

    //BESTIARY
    public virtual Sprite getSprite()
    {
        return sprite;
    }
    public virtual string getDescription()
    {
        return description;
    }

    //PLANT TARGETING
    protected virtual void findRandomPlant()
    {
        if (effect == "pollinator") pollinatorTarget();
        else randomPlant();
    }

    protected virtual void pollinatorTarget()
    {
        for (int i = 0; i < plants.Count; i++)
        {
            if (plants[i].getGrowth() != 100) plants.Remove(plants[i]);
        }

        for (int i = 0; i < plants.Count; i++)
        {
            if (plants[i].getMaturity() != "MATURE")
            {
                //Find only young plants
                for (int j = 0; j < plants.Count; j++)
                {
                    if (plants[j].getMaturity() == "MATURE")
                    {
                        plants.Remove(plants[j]);
                        pollinatorTarget();
                        return;
                    }
                }

                randomPlant();
                return;
            }
        }

        randomPlant();
    }

    protected virtual void randomPlant()
    {
        for (int i = 0; i < plants.Count; i++)
        {
            if (plants[i].getSpecial() == "new")
            {
                plants.Remove(plants[i]);
                randomPlant();
                return;
            }
        }

        target = plants[UnityEngine.Random.Range(0, plants.Count)];
    }

    protected virtual bool checkRange()
    {
        try
        {
            Vector3 bug = transform.position;
            Vector3 t = target.transform.position;

            if (Mathf.Abs(bug.x - t.x) <= .2f && Mathf.Abs(bug.y - t.y) <= .2f) return true;
            else return false;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            findRandomPlant();
            return false;
        }
        
    }

    protected virtual void moveTowardsPlant()
    {
        Vector3 bug = transform.position;
        Vector3 t = target.transform.position;
        Vector3 newPos = new Vector3(0f, 0f);

        if (bug.x < t.x) newPos.x += distanceX * (Time.deltaTime / 5f) * speed;
        else newPos.x -= distanceX * (Time.deltaTime / 5f) * speed;

        if (bug.y < t.y) newPos.y += distanceY * (Time.deltaTime / 5f) * speed;
        else newPos.y -= distanceY * (Time.deltaTime / 5f) * speed;

        transform.position += newPos;
    }

    protected virtual void moveAway()
    {
        Vector3 newPos = new Vector3(0f, 0f);

        if (direction == 0) newPos.x += (Time.deltaTime / 1.5f) * speed;
        else newPos.x -= (Time.deltaTime / 1.5f) * speed;
        newPos.y += (Time.deltaTime / 1.5f) * speed;

        transform.position += newPos;

        if (Mathf.Abs(transform.position.x) >= 3) Destroy(gameObject);
    }

    //EFFECT
    protected virtual void action(Plant plant)
    {
        switch (effect)
        {
            case "pollinator": plant.pollinate(); break;
            case "harm": plant.setHealth(-10); break;
        }
    }
}
