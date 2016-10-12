using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SeasonManager : MonoBehaviour {

    public WeatherManager wm;

    //Background and Filter
    public Image filter;
    public SpriteRenderer background;
    public Sprite spring, summer, autumn, winter;

    //Seasonal Trees
    public SpriteRenderer tree1, tree2;
    public Sprite springTree, summerTree, autumnTree, winterTree;

    private enum SEASON
    {
        SPRING, SUMMER, AUTUMN, WINTER
    }
    private SEASON season = SEASON.SPRING;

    public void update(float dayNum)
    {
        if (dayNum % 3f == 0f)
        {
            switch (season)
            {
                case SEASON.SPRING: season = SEASON.SUMMER;
                    //filter.color = new Color(1, 1, 1, 0);
                    background.sprite = summer; 
                    tree1.sprite = summerTree; tree2.sprite = summerTree;
                    break;

                case SEASON.SUMMER: season = SEASON.AUTUMN;
                    //filter.color = new Color(1, .7f, 0, .15f);
                    background.sprite = autumn;
                    tree1.sprite = autumnTree; tree2.sprite = autumnTree;
                    break;

                case SEASON.AUTUMN: season = SEASON.WINTER;
                    //filter.color = new Color(.3f, .2f, 0, .2f);
                    background.sprite = winter;
                    tree1.sprite = winterTree; tree2.sprite = winterTree;
                    break;

                case SEASON.WINTER: season = SEASON.SPRING;
                    //filter.color = new Color(.5f, .5f, .5f, .55f);
                    background.sprite = spring;
                    tree1.sprite = springTree; tree2.sprite = springTree;
                    break;
            }
        }
    }

    public string getSeason()
    {
        return season + "";
    }

    //LOAD
    public void loadSeason(string s)
    {
        switch (s)
        {
            case "SPRING":
                season = SEASON.SPRING;
                //filter.color = new Color(.5f, .5f, .5f, .55f);
                background.sprite = spring;
                tree1.sprite = springTree; tree2.sprite = springTree;
                break;

            case "SUMMER":
                season = SEASON.SUMMER;
                //filter.color = new Color(1, 1, 1, 0);
                background.sprite = summer;
                tree1.sprite = summerTree; tree2.sprite = summerTree;
                break;

            case "AUTUMN":
                season = SEASON.AUTUMN;
                //filter.color = new Color(1, .7f, 0, .15f);
                background.sprite = autumn;
                tree1.sprite = autumnTree; tree2.sprite = autumnTree;
                break;

            case "WINTER":
                season = SEASON.WINTER;
                //filter.color = new Color(.3f, .2f, 0, .2f);
                background.sprite = winter;
                tree1.sprite = winterTree; tree2.sprite = winterTree;
                break;
        }

        wm.secondLoad(getSeason());
        tree1.sortingOrder = 2; tree2.sortingOrder = 2;
    }
}

