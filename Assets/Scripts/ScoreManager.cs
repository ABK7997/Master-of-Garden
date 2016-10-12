using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ScoreManager : MonoBehaviour {

    public static int smallScore = 0, medScore = 0, largeScore = 0, mountainScore = 0, zenScore = 0;
    public static float smallDays = 0, medDays = 0, largeDays = 0, mountainDays = 0, zenDays = 0;

    public static void save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Scores.dat");

        ScoreData data = new ScoreData();

        data.smallScore = smallScore;
        data.medScore = medScore;
        data.largeScore = largeScore;
        data.mountainScore = mountainScore;
        data.zenScore = zenScore;

        data.smallDays = smallDays;
        data.medDays = medDays;
        data.largeDays = largeDays;
        data.mountainDays = mountainDays;
        data.zenDays = zenDays;

        bf.Serialize(file, data);
        file.Close();
    }

    public static void load()
    {
        if (File.Exists(Application.persistentDataPath + "/Scores.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Scores.dat", FileMode.Open);

            ScoreData data = bf.Deserialize(file) as ScoreData;
            file.Close();

            smallScore = data.smallScore;
            medScore = data.medScore;
            largeScore = data.largeScore;
            mountainScore = data.mountainScore;
            zenScore = data.zenScore;

            smallDays = data.smallDays;
            medDays = data.medDays;
            largeDays = data.largeDays;
            mountainDays = data.mountainDays;
            zenDays = data.zenDays;
        }
    }

    public static void delete()
    {
        if (File.Exists(Application.persistentDataPath + "/Scores.dat"))
        {
            File.Delete(Application.persistentDataPath + "/Scores.dat");
        }
    }
}

[Serializable]
class ScoreData
{
    public int smallScore, medScore, largeScore, mountainScore, zenScore;
    public float smallDays, medDays, largeDays, mountainDays, zenDays;
}

