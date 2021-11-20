using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowAwardController : MonoBehaviour
{
    private Text m_awardText;
    public ItemType awardType;
    private void Awake()
    {
        m_awardText = GetComponent<Text>();
    }
    private void Update()
    {
        string awardName = "";
        switch (awardType)
        {
            case ItemType.Cherry:
                awardName = "Cherry";
                break;
            case ItemType.Gem:
                awardName = "Gem";
                break;
            case ItemType.Money:
                awardName = "Money";
                break;
        }
        m_awardText.text = awardName + ": " + DataManager.Ins.InventoryData.GetItem(awardName).itemHeld;
    }
}
