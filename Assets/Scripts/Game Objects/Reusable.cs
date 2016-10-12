using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Reusable : Item {

    private static bool open = false, charging = false;
    public static bool purchased = false;

    public Sprite ready, notReady;
    private Image image;

    private float timer = 0f;

    protected override void Start()
    {
        text.text = "-$" + cost;
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (!Menu.running) return;

        if (purchased) text.text = "";

        if (charging) timer += Time.deltaTime;
        if (timer > 20)
        {
            timer = 0;
            open = true;
            charging = false;
        }

        if (open) image.sprite = ready;
        else image.sprite = notReady;
    }

    public static void setMusic(bool b)
    {
        if (b)
        {
            open = true;
            purchased = true;
        }
        else
        {
            open = false;
            charging = true;
        }
    }

    public static bool isOpen()
    {
        return open;
    }

}
