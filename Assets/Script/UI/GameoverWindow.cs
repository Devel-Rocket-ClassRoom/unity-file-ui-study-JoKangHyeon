using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameoverWindow : GenericWindow
{
    public TextMeshProUGUI[] leftStatLabel;
    public TextMeshProUGUI[] rightStatLabel;

    public TextMeshProUGUI[] leftStatValue;
    public TextMeshProUGUI[] rightStatValue;

    public TextMeshProUGUI totalScoreValue;

    public Button nextButton;

    public float statShowInterval = 1.0f;
    public float scoreShowTime = 1.0f;
    public float scoreShowTimeUnit = 0.1f;

    WaitForSeconds statShowWait;
    WaitForSeconds scoreShowUnitWait;

    int[] stats;
    int totalScore;

    Coroutine showWindowCoroutine;

    private void Awake()
    {
        nextButton.onClick.AddListener(OnNext);
        statShowWait = new WaitForSeconds(statShowInterval);
        scoreShowUnitWait = new WaitForSeconds(scoreShowTimeUnit);
    }

    public override void Open()
    {
        stats = new int[leftStatLabel.Length + rightStatLabel.Length];
        totalScore = Random.Range(0, 100000000);
        for (int i = 0; i < stats.Length; i++)
        {
            stats[i] = Random.Range(0, 10000);
        }

        base.Open();

        if(showWindowCoroutine != null)
        {
            StopCoroutine(showWindowCoroutine);
        }
        showWindowCoroutine = StartCoroutine(ShowOpenWindowAnimation());
    }

    public override void Close()
    {
        if (showWindowCoroutine != null)
        {
            StopCoroutine(showWindowCoroutine);
            showWindowCoroutine = null;
        }

        base.Close();
    }

    public void OnNext()
    {
        windowManager.Open(0);
    }

    public IEnumerator ShowOpenWindowAnimation()
    {
        Color originalColor = leftStatLabel[0].color;

        //init
        for (int i = 0; i < leftStatLabel.Length; i++)
        {
            leftStatValue[i].text = stats[i].ToString("D4");
            leftStatLabel[i].color = Color.clear;
            leftStatValue[i].color = Color.clear;
        }
        for (int i = 0; i < rightStatLabel.Length; i++)
        {
            rightStatValue[i].text = stats[leftStatLabel.Length + i].ToString("D4");
            rightStatLabel[i].color = Color.clear;
            rightStatValue[i].color = Color.clear;
        }
        totalScoreValue.text = "00000000";

        //Show label
        for (int i = 0; i < leftStatLabel.Length; i++)
        {
            leftStatLabel[i].color = originalColor;
            leftStatValue[i].color = originalColor;

            yield return statShowWait;
        }

        for (int i = 0; i < rightStatLabel.Length; i++)
        {
            rightStatLabel[i].color = originalColor;
            rightStatValue[i].color = originalColor;

            yield return statShowWait;
        }

        //show total score
        float timer = 0f;
        while (timer < scoreShowTime)
        {
            timer += scoreShowTimeUnit;
            int currentScoreShown = Mathf.FloorToInt(Mathf.Lerp(0, totalScore, timer / scoreShowTime));
            totalScoreValue.text = currentScoreShown.ToString("D8");
            yield return scoreShowUnitWait;
        }

        totalScoreValue.text = totalScore.ToString("D8");

        showWindowCoroutine = null;
    }
}
