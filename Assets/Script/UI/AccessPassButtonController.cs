using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AccessPassButtonController : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate ()
        {
            OnClick(gameObject);
        });
    }
    public void OnClick(GameObject obj)
    {
        switch (obj.tag)
        {
            case "NextSceneButton":
                TaskPanelController task = GameObject.Find("Canvas").transform.Find("Task").GetComponent<TaskPanelController>();
                task.gameObject.SetActive(true);
                task.AccessPass();
                //±£´æÊý¾Ý
                SaveManager.Ins.Save();
                break;
        }
    }
}
