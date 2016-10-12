using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tool : MonoBehaviour
{
    public Image water, fert, synth, heat, spray, cover, music, swat;

    public enum ACTION
    {
        NONE, WATER, FERTILIZER, SYNTH, HEATER, SPRAY, MUSIC, COVER, SWATTER
    }
    public static ACTION action = ACTION.NONE;

    //Hotket support
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            action = ACTION.SWATTER;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            action = ACTION.WATER;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            action = ACTION.FERTILIZER;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            action = ACTION.SYNTH;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            action = ACTION.HEATER;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            action = ACTION.SPRAY;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            action = ACTION.COVER;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            action = ACTION.MUSIC;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            action = ACTION.NONE;
        }

        water.color = Color.white;
        fert.color = Color.white;
        synth.color = Color.white;
        heat.color = Color.white;
        spray.color = Color.white;

        cover.color = Color.white;
        music.color = Color.white;
        swat.color = Color.white;

        //Color Code
        switch (action)
        {
            case ACTION.WATER: water.color = new Color(0.3f, 0.3f, 0.3f); break;
            case ACTION.FERTILIZER: fert.color = new Color(0.3f, 0.3f, 0.3f); break;
            case ACTION.SYNTH: synth.color = new Color(0.3f, 0.3f, 0.3f); break;
            case ACTION.HEATER: heat.color = new Color(0.3f, 0.3f, 0.3f); break;
            case ACTION.SPRAY: spray.color = new Color(0.3f, 0.3f, 0.3f); break;

            case ACTION.COVER: cover.color = new Color(0.3f, 0.3f, 0.3f); break;
            case ACTION.MUSIC: music.color = new Color(0.3f, 0.3f, 0.3f); break;
            case ACTION.SWATTER: swat.color = new Color(0.3f, 0.3f, 0.3f); break;
        }
    }

    public static void changeState(int num)
    {
        switch (num)
        {
            case 0: action = ACTION.NONE; break;
            case 1: action = ACTION.WATER; break;
            case 2: action = ACTION.FERTILIZER; break;
            case 3: action = ACTION.SYNTH; break;
            case 4: action = ACTION.HEATER; break;
            case 5: action = ACTION.SPRAY; break;
            case 6: action = ACTION.COVER; break;
            case 7: if (Reusable.isOpen()) action = ACTION.MUSIC; break;
            case 8: action = ACTION.SWATTER; break;
        }
    }

    public void tool(int num)
    {
        switch (num)
        {
            case 0: action = ACTION.NONE; break;
            case 1: action = ACTION.WATER; break;
            case 2: action = ACTION.FERTILIZER; break;
            case 3: action = ACTION.SYNTH; break;
            case 4: action = ACTION.HEATER; break;
            case 5: action = ACTION.SPRAY; break;
            case 6: action = ACTION.COVER; break;
            case 7: action = ACTION.MUSIC; break;
            case 8: action = ACTION.SWATTER; break;
        }
    }
}