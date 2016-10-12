using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Plant : MonoBehaviour {

    //Statistics
    public Canvas stats, sell;

    public string Name, special, description, specialDescription;
    public Sprite[] displaySprites;
    protected bool specialing = false;
    public int index;
    private int order = 0;

    public Text nameText, statusText, coinText;
    public Image healthBar, growthBar, thirstBar, sunBar, happinessBar;
    protected float barWidth = 50f;
    public int maxHealth, maxGrowth, maxThirst, maxSun, maxHappiness , price;
    protected int health, growth, thirst, sun, happiness;
    public float coinage;

    //Booleans
    public GameObject cover, heater;
    protected bool hovering = false, pollinated = false, covered = false, warmed = false, infested = false;
    protected float heatTimer = 0f;

    //States
    protected enum MATURITY
    {
        SAPLING, JUVENILE, MATURE
    }
    protected MATURITY maturity = MATURITY.SAPLING;

    protected enum TEMPERATURE
    {
        FREEZING, NORMAL, HOT
    }
    protected TEMPERATURE temp = TEMPERATURE.NORMAL;

    //MISC
    protected PlantManager pm;
    protected Animator anim;

    // Use this for initialization
    protected virtual void Awake() {
        setHealth(maxHealth);
        setGrowth(0);
        setThirst(maxThirst);
        setSunlight(maxSun);
        setHappiness(maxHappiness);

        coinText.text = "+$" + coinage;
        nameText.text = Name;

        pm = FindObjectOfType<PlantManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!Menu.running) return;

        if (hovering && Input.GetMouseButtonDown(0))
        {
            switch (Tool.action)
            {
                case Tool.ACTION.WATER: ItemManager.useWater(this); break;
                case Tool.ACTION.FERTILIZER: ItemManager.useFertilizer(this); break;
                case Tool.ACTION.SYNTH: ItemManager.useSynthesis(this); break;
                case Tool.ACTION.HEATER: ItemManager.useHeater(this); break;
                case Tool.ACTION.SPRAY: ItemManager.useSpray(this); break;
                case Tool.ACTION.COVER: ItemManager.useCover(this); break;
                case Tool.ACTION.MUSIC: ItemManager.useMusic(this); Tool.action = Tool.ACTION.NONE; break;
            }
        }
        else if (hovering && Input.GetMouseButtonDown(1))
        {
            sell.gameObject.SetActive(true);
            Menu.running = false;
        }

        //Heater
        if (warmed)
        {
            heatTimer += Time.deltaTime;
            if (heatTimer >= 120) setWarmed(false);
        }
    }

    protected virtual void OnMouseOver()
    {
        if (!Menu.running) return;
        if (special != "new") stats.gameObject.SetActive(true);
        hovering = true;
    }
    protected virtual void OnMouseExit()
    {
        stats.gameObject.SetActive(false);
        hovering = false;
    }

    //Season-Specific
    public virtual void seasonal(string season)
    {
        if (season == "SUMMER") temp = TEMPERATURE.HOT;
        else if (season == "WINTER") temp = TEMPERATURE.FREEZING;
        else temp = TEMPERATURE.NORMAL;
    }

    //STAT METHODS
    public virtual string getName()
    {
        return Name;
    }
    public virtual string getDescription()
    {
        return description;
    }
    public virtual string getSpecialDescription()
    {
        return specialDescription;
    }
    public virtual string getSpecial()
    {
        return special;
    }
    public virtual bool isSpecialing()
    {
        return specialing;
    }
    public virtual void setSpecial(bool b)
    {
        specialing = b;

        //anim.SetTrigger("Biting");
    }
    public virtual int getIndex()
    {
        return index;
    }
    public void setOrder(int index)
    {
        order = index;
    }
    public int getOrder()
    {
        return order;
    }
    public Sprite[] getSprites()
    {
        return displaySprites;
    }

    public virtual int getPrice()
    {
        return price;
    }

    public virtual int getHealth()
    {
        return (health * 100) / maxHealth;
    }
    public virtual void setHealth(int num)
    {
        health += num;
        if (health < 0) health = 0;
        else if (health > maxHealth) health = maxHealth;

        float current = health * 1f;
        float max = maxHealth * 1f;
        float ratio = current / max;
        healthBar.rectTransform.sizeDelta = new Vector2(ratio * barWidth, healthBar.rectTransform.sizeDelta.y);
    }

    public virtual int getGrowth()
    {
        return (growth * 100) / maxGrowth;
    }
    public virtual void setGrowth(int num)
    {
        growth += num;
        if (growth < 0) growth = 0;
        else if (growth > maxGrowth) growth = maxGrowth;

        if (getGrowth() == 100 && pollinated) maturation();

        float current = growth * 1f;
        float max = maxGrowth * 1f;
        float ratio = current / max;
        growthBar.rectTransform.sizeDelta = new Vector2(ratio * barWidth, growthBar.rectTransform.sizeDelta.y);
    }

    public virtual int getThirst()
    {
        return (thirst * 100) / maxThirst;
    }
    public virtual void setThirst(int num)
    {
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

    public virtual int getHappiness()
    {
        return (happiness * 100) / maxHappiness;
    }
    public virtual void setHappiness(int num)
    {
        happiness += num;
        if (happiness < 0) happiness = 0;
        else if (happiness > maxHappiness) happiness = maxHappiness;

        float current = happiness * 1f;
        float max = maxHappiness * 1f;
        float ratio = current / max;
        happinessBar.rectTransform.sizeDelta = new Vector2(ratio * barWidth, happinessBar.rectTransform.sizeDelta.y);
    }

    public virtual int getSunlight()
    {
        return (sun * 100) / maxSun;
    }
    public virtual void setSunlight(int num)
    {
        statusText.text = "Warm";

        //Hotness
        sun += num;
        if (sun < 0)
        {
            setHealth(-1);
            sun = 0;
        }
        else if (sun > maxSun)
        {
            sun = maxSun;
            if (temp == TEMPERATURE.HOT)
            {
                setHealth(-2);
                setHappiness(-1);
                statusText.text = "Overheating";
            }
        }

        //Freezing
        if (temp == TEMPERATURE.FREEZING && !warmed)
        {
            setHealth(-2);
            setHappiness(-1);
            statusText.text = "Freezing";
        }

        float current = sun * 1f;
        float max = maxSun * 1f;
        float ratio = current / max;
        sunBar.rectTransform.sizeDelta = new Vector2(ratio * barWidth, sunBar.rectTransform.sizeDelta.y);

        //Infestation
        if (infested) statusText.text = "Infested";
    }

    public virtual string getMaturity()
    {
        return "" + maturity;
    }

    public virtual void maturation()
    {
        anim.SetTrigger("Growth");
        growth = 0;
        if (maturity != MATURITY.MATURE)
        {
            coinage *= 1.5f;
            maxGrowth *= 2;
            maxHealth *= 2;
            setHealth(health);
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

    //Boolean Methods
    public virtual void infestation()
    {
        if (infested)
        {
            setHealth(-1);
            setThirst(-1);
        }
    }
    public virtual void setInfested(bool boolean)
    {
        infested = boolean;
    }

    public virtual bool isPollinated()
    {
        return pollinated;
    }
    public virtual void pollinate()
    {
        pollinated = true;
    }

    public virtual bool isCovered()
    {
        return covered;
    }

    public virtual void setCover(bool boolean)
    {
        covered = boolean;
        if (covered) cover.SetActive(true);
        else cover.SetActive(false);
    }

    public virtual void setWarmed(bool boolean)
    {
        warmed = boolean;
        if (warmed)
        {
            heater.SetActive(true);
            heatTimer = 0f;
        }
        else heater.SetActive(false);
    }
    public virtual bool getWarmed()
    {
        return warmed;
    }

    public virtual float getCoinage()
    {
        return coinage;
    }

    public virtual int calculateScore()
    {
        int score = 0;

        score += getThirst() / 4;
        score += getSunlight() / 4;

        if (getHappiness() > 75) score *= 2;
        else if (getHappiness() > 50) score = (int)(score * 1.5);
        else if (getHappiness() <= 25) score = (int)(score * .5);
        else if (getHappiness() == 0) score = (int)(score * .25);

        if (maturity == MATURITY.JUVENILE) score = (int)(score * 1.5);
        else if (maturity == MATURITY.MATURE) score = score * 2;

        return score;
    }

    public virtual void reset()
    {
        warmed = false;
        heater.SetActive(false);
    }

    public virtual void calculateHealth()
    {
        if (thirst == 0) setHealth(-1);
        if (sun == 0) setHealth(-1);
    }

    //SELLING
    public void sellPlant()
    {
        if (pm.getCount() == 1) return;

        int value = price;

        if (maturity == MATURITY.SAPLING) value = (int)(price * .75f);
        else if (maturity == MATURITY.JUVENILE) value = price * 2;
        else value = (int)(price * 3.5);

        ItemManager.money += value;

        sell.gameObject.SetActive(false);
        Menu.running = true;

        pm.sellPlant(pm.getAll()[0], gameObject);
    }

    public void dontSell()
    {
        sell.gameObject.SetActive(false);

        Menu.running = true;
    }

    //SAVING AND LOADING
    public virtual int[] save()
    {
        int mat = 0;
        if (maturity == MATURITY.JUVENILE) mat = 1;
        else if (maturity == MATURITY.MATURE) mat = 2;

        int[] stats = new int[]
        {
            health, maxHealth,
            growth, maxGrowth,
            thirst, maxThirst,
            sun, maxSun,
            happiness, maxHappiness,
            index, order,
            mat
        };

        return stats;
    }

    public virtual void load(int[] data)
    {
        index = data[10]; order = data[11];

        pm.loadPlant(pm.getPlants()[order].gameObject, pm.getAll()[index], data);
    }

    public virtual void loadStats(int[] data)
    {
        health = data[0]; maxHealth = data[1];
        growth = data[2]; maxGrowth = data[3];
        thirst = data[4]; maxThirst = data[5];
        sun = data[6]; maxSun = data[7];
        happiness = data[8]; maxHappiness = data[9];
        index = data[10]; order = data[11];

        updateStats();

        //Maturity
        if (data[12] == 1)
        {
            maturity = MATURITY.JUVENILE;
            anim.SetTrigger("Growth");

            coinage *= 1.5f;
            coinText.text = "+$" + coinage;
        }
        else if (data[12] == 2)
        {
            maturity = MATURITY.MATURE;
            anim.SetTrigger("FullGrown");

            coinage *= 2.25f;
            coinText.text = "+$" + coinage;
        }
    }

    public virtual void updateStats()
    {
        float current = health * 1f;
        float max = maxHealth * 1f;
        float ratio = current / max;
        healthBar.rectTransform.sizeDelta = new Vector2(ratio * barWidth, healthBar.rectTransform.sizeDelta.y);

        float current2 = growth * 1f;
        float max2 = maxGrowth * 1f;
        float ratio2 = current2 / max2;
        growthBar.rectTransform.sizeDelta = new Vector2(ratio2 * barWidth, growthBar.rectTransform.sizeDelta.y);

        float current3 = thirst * 1f;
        float max3 = maxThirst * 1f;
        float ratio3 = current3 / max3;
        thirstBar.rectTransform.sizeDelta = new Vector2(ratio3 * barWidth, thirstBar.rectTransform.sizeDelta.y);

        float current4 = sun * 1f;
        float max4 = maxSun * 1f;
        float ratio4 = current4 / max4;
        sunBar.rectTransform.sizeDelta = new Vector2(ratio4 * barWidth, sunBar.rectTransform.sizeDelta.y);

        float current5 = happiness * 1f;
        float max5 = maxHappiness * 1f;
        float ratio5 = current5 / max5;
        happinessBar.rectTransform.sizeDelta = new Vector2(ratio5 * barWidth, happinessBar.rectTransform.sizeDelta.y);
    }

}