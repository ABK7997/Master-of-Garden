using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Menu : MonoBehaviour {

    public static bool running = true;

    public Canvas menu, store, help;
    public Text text;

    void Start()
    {
        ScoreManager.load();
    }

	public void loadLevel(int index)
    {
        if (text != null) text.text = "Loading...";
        running = true;

        //SceneManager.LoadScene(index);
        Application.LoadLevel(index);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void toggleMenu()
    {
        menu.gameObject.SetActive(!menu.gameObject.activeInHierarchy);
        running = !running;
    }

    public void toggleStore()
    {
        store.gameObject.SetActive(!store.gameObject.activeInHierarchy);
        running = !running;
    }

    public void toggleHelp()
    {
        help.gameObject.SetActive(!help.gameObject.activeInHierarchy);
        running = !running;
    }

    public void deleteAll(string stageName)
    {
        if (File.Exists(Application.persistentDataPath + "/" + stageName + "Garden.dat"))
        {
            File.Delete(Application.persistentDataPath + "/" + stageName + "Garden.dat");
        }

        if (File.Exists(Application.persistentDataPath + "/" + stageName + "Save Manager.dat"))
        {
            File.Delete(Application.persistentDataPath + "/" + stageName + "Save Manager.dat");
        }
    }
}
