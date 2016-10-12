using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerClickHandler {

    public int index, cost;
    public Text text;

    protected virtual void Start()
    {
        text.text = "-$" + cost;
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            switch (index)
            {
                case 1: ItemManager.buyWater(); Tool.action = Tool.ACTION.WATER; break;
                case 2: ItemManager.buyFertilizer(); Tool.action = Tool.ACTION.FERTILIZER; break;
                case 3: ItemManager.buySynthesis(); Tool.action = Tool.ACTION.SYNTH; break;
                case 4: ItemManager.buyHeater(); Tool.action = Tool.ACTION.HEATER; break;
                case 5: ItemManager.buySpray(); Tool.action = Tool.ACTION.SPRAY; break;
                case 6: ItemManager.buyUmbrella(); Tool.action = Tool.ACTION.COVER; break;
                case 7: ItemManager.buyMusic(); Tool.action = Tool.ACTION.MUSIC; break;
            }
        }
    }

}
