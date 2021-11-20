using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementPopPanel : MonoBehaviour
{
    public float animationTime;
    public float continueTime;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void SetNewAchievementPanel(AchievementData achievement)
    {
        transform.GetChild(0).GetComponent<Text>().text = achievement.achievementName;
        transform.GetChild(1).GetComponent<Text>().text = achievement.achievementDescription;
        transform.GetChild(2).GetComponent<Image>().sprite = achievement.achievementIcon;
        StartCoroutine(PopAchievementPanel());
    }
    private IEnumerator PopAchievementPanel()
    {
        //audioSource.Play();
        float percent = 0;
        float amount = GetComponent<RectTransform>().anchoredPosition3D.y;
        while (percent < 1)
        {
            percent += Time.deltaTime / animationTime;
            GetComponent<RectTransform>().anchoredPosition3D += Vector3.down * amount * Time.deltaTime / animationTime;

            yield return null;
        }

        yield return new WaitForSeconds(continueTime);

        percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime / animationTime;
            GetComponent<RectTransform>().anchoredPosition3D += Vector3.up * amount * Time.deltaTime / animationTime;

            yield return null;
        }
        Destroy(gameObject);
    }
}
