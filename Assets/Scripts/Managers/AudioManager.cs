using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    private AudioSource mp;

    //Track listings
    private string[] springMusic =
    {
        "Clear Waters",
        "Enchanted Valley",
        "Immersed",
        "Light Thought var 1",
        "Light Thought var 2",
        "For Originz",
        "Village Dawn",
        "Water Lilly",
        "Wet Riffs",
        "When the Wind Blows"
    };

    private string[] summerMusic =
    {
        "Americana",
        "Anguish",
        "Crowd Hammer",
        "Death of Kings",
        "Digya",
        "Lost Time",
        "Finding the Balance",
        "Ossuary 1 - A Beginning",
        "The Path of the Goblin King",
        "Thunder Dreams"
    };

    private string[] autumnMusic =
    {
        "Blue Paint",
        "Brittle Rille",
        "Colossus",
        "Crossing the Divide",
        "Dreamy Flashback",
        "Disquiet",
        "Floating Cities",
        "March of the Wind",
        "Mesmerize",
        "Moonlight Hall"
    };

    private string[] winterMusic =
    {
        "Deep Haze",
        "Final Battle of the Dark Wizards",
        "Hero Down",
        "Intrepid",
        "Minima",
        "Oppressive Gloom",
        "String Impromptu Number 1",
        "Tempting Secrets",
        "The Snow Queen",
        "Wounded",
    };

    public SeasonManager sm;

    private enum SEASON
    {
        SPRING, SUMMER, AUTUMN, WINTER
    }
    private SEASON season = SEASON.SPRING;

	// Use this for initialization
	void Awake () {
        mp = GetComponent<AudioSource>();

        mp.clip = Resources.Load(springMusic[Random.Range(0, springMusic.Length)]) as AudioClip;
        mp.Play();
	}
	
	// Update is called once per frame
	void Update () {
        changeSeason(sm.getSeason());

        if (!mp.isPlaying) changeSongs();
	}

    public void changeSeason(string timeOfYear)
    {
        switch(timeOfYear)
        {
            case "SPRING": season = SEASON.SPRING; break;
            case "SUMMER": season = SEASON.SUMMER; break;
            case "AUTUMN": season = SEASON.AUTUMN; break;
            case "WINTER": season = SEASON.WINTER; break;
        }
    }

    public void changeSongs()
    {
        mp.Stop();

        string song;
        
        switch(season)
        {
            case SEASON.SPRING: song = springMusic[Random.Range(0, springMusic.Length)]; break;
            case SEASON.SUMMER: song = summerMusic[Random.Range(0, summerMusic.Length)]; break;
            case SEASON.AUTUMN: song = autumnMusic[Random.Range(0, autumnMusic.Length)]; break;
            case SEASON.WINTER: song = winterMusic[Random.Range(0, winterMusic.Length)]; break;
            default: song = null; break;
        }

        mp.clip = Resources.Load(song) as AudioClip;

        mp.Play();
    }
}
