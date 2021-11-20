using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class TipDialog : MonoBehaviour
{
    public GameObject tipDialog;
    private GameObject m_tipDialog;
    public string tip;
    public LayerMask layerMask;
    private void Update()
    {
        if(Physics2D.OverlapBox(transform.position, new Vector2(1.1f, 1.25f), 0f, layerMask))
        {
            SetDialog();
        }
        else
        {
            if(m_tipDialog != null)
            {
                Destroy(m_tipDialog.gameObject);
            }
        }
    }
    private void SetDialog()
    {
        if(m_tipDialog == null)
        {
            m_tipDialog = Instantiate(tipDialog, FindObjectOfType<Canvas>().transform);
            m_tipDialog.GetComponentInChildren<Text>().text = tip;
        }
    }
}
