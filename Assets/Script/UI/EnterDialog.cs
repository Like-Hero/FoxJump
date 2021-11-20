using UnityEngine;
using UnityEngine.UI;

public class EnterDialog : MonoBehaviour
{
    public GameObject enterDialog;
    public Text text;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enterDialog.SetActive(true);
            text.text = "Press E to Enter";
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enterDialog.SetActive(false);
        }
    }
}