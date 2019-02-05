using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fight : MonoBehaviour
{
    public GameObject[] Players;
    public GameObject TimerDuel;
    public GameObject TimerUpdate;
    public GameObject TimerRumble;

    public AudioClip ChangeTurn;
    public AudioClip ChangeTimer;
    private AudioSource Audio;

    private bool IsDuelTime;
    private bool IsUpdateTime;
    private bool IsRoyalRumble;

    private static string DefaultTimeFight = "30";
    private static string DefaultTimeUpdate = "30";
    // Start is called before the first frame update
    void Start()
    {
        Initialized();
        IsDuelTime = false;
        IsUpdateTime = false;
        IsRoyalRumble = false;
        Audio = transform.GetComponent<AudioSource>();
        StartCoroutine(IncrementTimerUpdate());
        StartCoroutine(IncrementTimerFight());
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Home", LoadSceneMode.Single);
    }

    public void AddPoints(GameObject parent)
    {
        AddSubPoints(parent, 1);
    }

    public void SubPoints(GameObject parent)
    {
        AddSubPoints(parent, -1);
    }

    private void AddSubPoints(GameObject parent,int i)
    {
        string name = parent.name;
        Text pointsText = parent.transform.Find("Points").GetComponent<Text>();
        int points = int.Parse(pointsText.text);
        points += i;
        pointsText.text = points.ToString();
        PlayerPrefs.SetInt(name, points);
    }

    public void Play(string timerName)
    {
        IsUpdateTime = false;
        IsRoyalRumble = false;
        if (timerName == "Update")
            IsUpdateTime = true;
        if (timerName == "Rumble")
            IsRoyalRumble = true;
    }

    public void Stop(string timerName)
    {
        if (timerName == "Update")
            IsUpdateTime = false;
        if (timerName == "Rumble")
            IsRoyalRumble = false;
    }

    public void Next(string timerName)
    {
        NextPrevious(timerName, 1);
    }

    public void Previous(string timerName)
    {
        NextPrevious(timerName, -1);
    }

    private void NextPrevious(string timerName, int change)
    {
        int players = PlayerPrefs.GetInt("Players");
        if (players == 0)
            players = 6;

        if (timerName == "Update")
        {
            Text timeUpdate = TimerUpdate.transform.Find("Timer").GetComponent<Text>();
            timeUpdate.text = PlayerPrefs.GetInt("TimeUpdate").ToString();
            timeUpdate.text = timeUpdate.text == "0" ? DefaultTimeUpdate : timeUpdate.text;
            Text turnUpdate = TimerUpdate.transform.Find("Player").GetComponent<Text>();
            int turn = int.Parse(turnUpdate.text);
            turn += change;
            if (turn > players)
                turn = 1;
            if (turn <= 0)
                turn = players;
            turnUpdate.text = turn.ToString();
        }
        if (timerName == "Rumble")
        {
            Text timeFight = TimerRumble.transform.Find("Timer").GetComponent<Text>();
            timeFight.text = PlayerPrefs.GetInt("TimeFight").ToString();
            timeFight.text = timeFight.text == "0" ? DefaultTimeFight : timeFight.text;
            Text turnFight = TimerRumble.transform.Find("Player").GetComponent<Text>();
            int turn = int.Parse(turnFight.text);
            turn += change;
            if (turn > players)
                turn = 1;
            if (turn <= 0)
                turn = players;
            turnFight.text = turn.ToString();
        }
        if (timerName == "Duel")
        {
            Text timeDuel = TimerDuel.transform.Find("Timer").GetComponent<Text>();
            timeDuel.text = PlayerPrefs.GetInt("TimeDuel").ToString();
            timeDuel.text = timeDuel.text == "0" ? DefaultTimeFight : timeDuel.text;
            Text turnDuel = TimerDuel.transform.Find("Player").GetComponent<Text>();
            int turn = int.Parse(turnDuel.text);
            turn = turn == 0 ? 1 : 0;
            turnDuel.text = turn.ToString();
        }
    }

    public void Reset(string timerName)
    {
        if (timerName == "Update")
        {
            Text timeUpdate = TimerUpdate.transform.Find("Timer").GetComponent<Text>();
            timeUpdate.text = PlayerPrefs.GetInt("TimeUpdate").ToString();
            timeUpdate.text = timeUpdate.text == "0" ? DefaultTimeUpdate : timeUpdate.text;
            Text turnUpdate = TimerUpdate.transform.Find("Player").GetComponent<Text>();
            turnUpdate.text = PlayerPrefs.GetInt("TurnUpdate").ToString();
            turnUpdate.text = turnUpdate.text == "0" ? "1" : turnUpdate.text;
        }
        if (timerName == "Rumble")
        {
            Text timeFight = TimerRumble.transform.Find("Timer").GetComponent<Text>();
            timeFight.text = PlayerPrefs.GetInt("TimeFight").ToString();
            timeFight.text = timeFight.text == "0" ? DefaultTimeFight : timeFight.text;
            Text turnFight = TimerRumble.transform.Find("Player").GetComponent<Text>();
            turnFight.text = PlayerPrefs.GetInt("TurnFight").ToString();
            turnFight.text = turnFight.text == "0" ? "1" : turnFight.text;
        }
        if (timerName == "Duel")
        {
            Text timeDuel = TimerDuel.transform.Find("Timer").GetComponent<Text>();
            timeDuel.text = PlayerPrefs.GetInt("TimeDuel").ToString();
            timeDuel.text = timeDuel.text == "0" ? DefaultTimeFight : timeDuel.text;
            Text turnFight = TimerDuel.transform.Find("Player").GetComponent<Text>();
            turnFight.text = PlayerPrefs.GetInt("TurnFight").ToString();
            turnFight.text = turnFight.text == "0" ? "1" : turnFight.text;
        }
    }

    private void Initialized()
    {
        int players = PlayerPrefs.GetInt("Players");
        if (players == 0)
            players = 6;
        for(int i = 0; i < players; i++)
        {
            Text name = Players[i].transform.Find("Name").GetComponent<Text>();
            Text points = Players[i].transform.Find("Points").GetComponent<Text>();
            name.text = PlayerPrefs.GetString("Player" + i);
            if (name.text == "")
                name.text = "RoboFighter " + i;
            points.text = PlayerPrefs.GetInt("PlayerPoints" + i).ToString();
            if (points.text == "")
                points.text = "0";
            Reset("Update");
            Reset("Rumble");
        }
        for(int j = players; j < 6; j++)
        {
            Players[j].SetActive(false);
        }
    }

    private IEnumerator IncrementTimerUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (IsUpdateTime)
            {
                Text timeUpdate = TimerUpdate.transform.Find("Timer").GetComponent<Text>();
                int time = int.Parse(timeUpdate.text);
                Text turnUpdate = TimerUpdate.transform.Find("Player").GetComponent<Text>();
                int turn = int.Parse(turnUpdate.text);
                int players = PlayerPrefs.GetInt("Players");
                if (players == 0)
                    players = 6;
                time--;
                if (time >= 0)
                    timeUpdate.text = time.ToString();
                else
                {
                    if (turn < players)
                    {
                        Next("Update");
                        PlayClip(ChangeTurn);
                    }
                    else
                    {
                        Play("Rumble");
                        Reset("Update");
                        PlayClip(ChangeTimer);
                    }
                }
            }
        }
    }

    private IEnumerator IncrementTimerFight()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (IsRoyalRumble)
            {
                Text timeFight = TimerRumble.transform.Find("Timer").GetComponent<Text>();
                int time = int.Parse(timeFight.text);
                Text turnFight = TimerUpdate.transform.Find("Player").GetComponent<Text>();
                int turn = int.Parse(turnFight.text);
                int players = PlayerPrefs.GetInt("Players");
                if (players == 0)
                    players = 6;
                time--;
                if (time >= 0)
                    timeFight.text = time.ToString();
                else
                {
                    if (turn < players)
                    {
                        Next("Rumble");
                        PlayClip(ChangeTurn);
                    }
                    else
                    {
                        Play("Update");
                        Reset("Rumble");
                        PlayClip(ChangeTimer);
                    }
                }
            }
        }
    }

    private void PlayClip (AudioClip clip)
    {
        Audio.clip = clip;
        Audio.Play(0);
    }
}
