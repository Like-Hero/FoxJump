using UnityEngine;
using UnityEngine.UI;

public class TipDialog : MonoBehaviour
{
    public GameObject tipDialog;
    public Text tipText;
    private string tip;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            string name = gameObject.name;
            if (name.Equals("FirstTip")) tip = "Welcome!";
            else if (name.Equals("SecondTip")) tip = "Fighting!";
            else if (name.Equals("ThirdTip")) tip = "Go!";
            else if (name == "Enter") tip = "Press E to Enter";
            tipText.text = tip;
            tipDialog.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tipDialog.SetActive(false);
        }
    }
}
