using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewardController : MonoBehaviour
{


    public TextMeshProUGUI[] TMPros;
    public Reward[] rewards;
    private List<string> rewardStrings = new List<string> { "Nice!", "Great job!", "You are Awesome!", "You rock!", "Never stop spinning!", "Spin me, daddy!", "CRAZZZYYY!", "FASTER!", "COMBO X4", "+1000 POINTS", "BONUS LIFE", "Much spin!" };
    public MinMax textTimerMinMax;
    private void Start()
    {
        rewards = new Reward[TMPros.Length];
        for (int i = 0; i < TMPros.Length; i++)
        {
            rewards[i] = new Reward(TMPros[i]);
        }
    }

    private void Update()
    {
        foreach (Reward r in rewards)
        {
            r.Update(Time.deltaTime);
        }
    }

    public void ShowNewReward(float spinMod)
    {
        int rewardID = FindIdleReward();
        if (rewardID != -1)
        {
            rewards[rewardID].SetUpReward(Random.Range(textTimerMinMax.min, textTimerMinMax.max), spinMod, new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f)), rewardStrings[Random.Range(0, rewardStrings.Count)]);
        }
    }

    int FindIdleReward()
    {
        for (int i = 0; i < rewards.Length; i++)
        {
            if (!(TMPros[i].IsActive()))
            {
                return i;
            }
        }
        return -1;
    }

}
public class Reward
{
    private float timer = 0;
    private float spinMod;
    private GameObject myGO;
    private TextMeshProUGUI myText;
    private Vector3 direction;
    private Vector3 startPos;

    public Reward(TextMeshProUGUI go)
    {
        myText = go;
        myGO = go.gameObject;
        startPos = myGO.transform.localPosition;
    }
    public void SetUpReward(float timer, float spinMod, Vector3 direction, string text)
    {
        myGO.transform.localPosition = startPos;
        this.timer = timer;
        this.spinMod = spinMod;
        this.direction = direction;
        myText.text = text;
        myGO.SetActive(true);
    }

    public bool Update(float deltaTime)
    {
        if (myGO.activeSelf)
        {
            if (timer <= 0)
            {
                Debug.Log("disabled");
                myGO.SetActive(false);
                return false;
            }
            timer -= deltaTime * spinMod;
            myGO.transform.localScale = new Vector3(1 / (4 + timer), 1 / (4 + timer), 1 / (4 + timer)) * spinMod;
            myGO.transform.localPosition = myGO.transform.localPosition + direction*direction.magnitude * deltaTime;
            return true;
        }
        return false;
    }
}

[System.Serializable]
public struct MinMax
{
    public float min;
    public float max;
}
