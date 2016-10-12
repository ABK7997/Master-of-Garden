using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager : MonoBehaviour {

    public string stageName;

    public AudioManager am;
    public PlantManager pm;
    public WeatherManager wm;
    public LunaSol dm;
    public SeasonManager sm;

	// Use this for initialization
	void Start () {
        load();
	}

    public void save()
    {
        //Save score
        switch (stageName)
        {
            case "Small":
                if (ScoreManager.smallDays < dm.getDays()) ScoreManager.smallDays = dm.getDays();
                if (ScoreManager.smallScore < pm.getScore()) ScoreManager.smallScore = pm.getScore();
                break;

            case "Medium":
                if (ScoreManager.medDays < dm.getDays()) ScoreManager.medDays = dm.getDays();
                if (ScoreManager.medScore < pm.getScore()) ScoreManager.medScore = pm.getScore();
                break;

            case "Large":
                if (ScoreManager.largeDays < dm.getDays()) ScoreManager.largeDays = dm.getDays();
                if (ScoreManager.largeScore < pm.getScore()) ScoreManager.largeScore = pm.getScore();
                break;

            case "Mountain":
                if (ScoreManager.mountainDays < dm.getDays()) ScoreManager.mountainDays = dm.getDays();
                if (ScoreManager.mountainScore < pm.getScore()) ScoreManager.mountainScore = pm.getScore();
                break;

            case "Zen":
                if (ScoreManager.zenDays < dm.getDays()) ScoreManager.zenDays = dm.getDays();
                if (ScoreManager.zenScore < pm.getScore()) ScoreManager.zenScore = pm.getScore();
                break;
        }

        ScoreManager.save();

        //Save plant data
        pm.save(stageName);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + stageName + name + ".dat");

        SaveData data = new SaveData();

        data.weather = wm.getWeather();
        data.season = sm.getSeason();
        data.dayNum = dm.getDays();
        data.money = ItemManager.money;

        //Items
        data.water = ItemManager.waters;
        data.fertilizer = ItemManager.fertilizer;
        data.synth = ItemManager.synth;
        data.heater = ItemManager.heaters;
        data.spray = ItemManager.sprays;
        data.cover = ItemManager.cover;
        data.music = ItemManager.music;

        bf.Serialize(file, data);
        file.Close();
    }

    public void load()
    {
        pm.load(stageName);

        if (File.Exists(Application.persistentDataPath + "/" + stageName + name + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + stageName + name + ".dat", FileMode.Open);

            SaveData data = bf.Deserialize(file) as SaveData;
            file.Close();

            dm.setDayNum(data.dayNum);
            wm.loadWeather(data.weather);
            sm.loadSeason(data.season);
            ItemManager.money = data.money;

            //Music
            am.changeSeason(data.season);
            am.changeSongs();

            //Items
            ItemManager.waters = data.water;
            ItemManager.fertilizerCost = data.fertilizer;
            ItemManager.synth = data.synth;
            ItemManager.heaters = data.heater;
            ItemManager.sprays = data.spray;
            ItemManager.cover = data.cover;
            ItemManager.music = data.music;

            if (data.music) Reusable.setMusic(true);
            Cover.purchased = data.cover;
        }
    }

    public void delete()
    {
        pm.delete(stageName);

        if (File.Exists(Application.persistentDataPath + "/" + stageName + name + ".dat"))
        {
            File.Delete(Application.persistentDataPath + "/" + stageName + name + ".dat");
        }
        
    }
}

[Serializable]
class SaveData
{
    public string weather, season;
    public float dayNum, money;
    public int water, fertilizer, synth, heater, spray;
    public bool cover, music;
}
