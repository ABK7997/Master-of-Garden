using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour {

    //Money
    public int startingMoney;

    public static float money = 1200f;
    public Text moneyCount;

    //Items
    public Text waterCount, fertilizerCount, synthCount, heatCount, sprayCount;

    public static int waters = 3, fertilizer = 1, synth = 1, heaters = 1, sprays = 1;
    public static bool cover = false, music = false;
    public static int waterCost = 200, fertilizerCost = 500, synthCost = 200, heatCost = 200, sprayCost = 500, coverCost = 1000, musicCost = 1000;
    public static int waterMax = 99, fertilizerMax = 99, synthMax = 99, heaterMax = 99, sprayMax = 99;

    void Update()
    {
        moneyCount.text = "$" + money;

        waterCount.text = "" + waters;
        fertilizerCount.text = "" + fertilizer;
        synthCount.text = "" + synth;
        heatCount.text = "" + heaters;
        sprayCount.text = "" + sprays;
    }

    void Awake()
    {
        money = startingMoney;
    }

    //ITEM METHODS
    public static void useWater(Plant plant)
    {
        if (waters == 0 || plant.getThirst() == 100) return;
        plant.setThirst(40);
        waters--;
    }

    public static void useFertilizer(Plant plant)
    {
        if (fertilizer == 0 || plant.getGrowth() == 100) return;
        plant.setGrowth(30);
        fertilizer--;
    }

    public static void useSynthesis(Plant plant)
    {
        if (synth == 0 || plant.getSunlight() == 100) return;
        plant.setSunlight(60);
        synth--;
    }

    public static void useHeater(Plant plant)
    {
        if (heaters == 0 || plant.getWarmed()) return;
        plant.setHappiness(10);
        plant.setWarmed(true);
        heaters--;
    }

    public static void useSpray(Plant plant)
    {
        if (sprays == 0) return;
        plant.setInfested(false);
        sprays--;
    }

    public static void useMusic(Plant plant)
    {
        if (!Reusable.isOpen()) return;
        plant.setHappiness(50);
        Reusable.setMusic(false);
    }

    public static void setMusic()
    {
        if (music) Reusable.setMusic(true);
    }

    public static void useCover(Plant plant)
    {
        if (!cover) return;
        plant.setCover(!plant.isCovered());
    }

    //STORE METHODS
    public static void buyWater()
    {
        if (money < waterCost || waters == waterMax) return;
        waters++;
        money -= waterCost;
    }

    public static void buyFertilizer()
    {
        if (money < 500 || fertilizer == fertilizerMax) return;
        fertilizer++;
        money -= 500;
    }

    public static void buySynthesis()
    {
        if (money < synthCost || synth == synthMax) return;
        synth++;
        money -= synthCost;
    }

    public static void buyHeater()
    {
        if (money < heatCost || heaters == heaterMax) return;
        heaters++;
        money -= heatCost;
    }

    public static void buySpray()
    {
        if (money < sprayCost || sprays == sprayMax) return;
        sprays++;
        money -= sprayCost;
    }

    public static void buyUmbrella()
    {
        if (cover || money < coverCost) return;
        cover = true;
        Cover.purchased = true;
        money -= coverCost; 
    }

    public static void buyMusic()
    {
        if (music || money < musicCost) return;
        music = true;
        money -= musicCost;
        Reusable.setMusic(true);
    }
}
