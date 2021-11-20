using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Text cherryValue;//ӣ��Text
    public Text gemValue;//��ʯText
    private void Update()
    {
        //print(GameManager.Ins.currentPassData.cherry);
        UpdateCollectionValue();
    }
    private void UpdateCollectionValue()
    {
        //print(DataManager.Ins);
        if (DataManager.IsInitialized)
        {
            cherryValue.text = "Cherry: " + GameManager.Ins.currentPassData.cherry.ToString();
            gemValue.text = "Gem: " + GameManager.Ins.currentPassData.gem.ToString();
        }
    }
}
