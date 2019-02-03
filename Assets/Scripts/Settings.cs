using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings: MonoBehaviour
{
    public GameObject[] playersName;
    public Dropdown numberPlayers;
    public InputField timeUpdate;
    public InputField timeFight;
    public GameObject ToastTest;

    private void Start()
    {
        InitializeOptions();
    }

    public void ChangeNumberOfPlayer()
    {
        int n = numberPlayers.value+2;
        foreach (GameObject x in playersName)
            x.SetActive(false);
        for (int i = 0; i < n; i++)
            playersName[i].SetActive(true);
    }

    public void ConfirmSettings()
    {
        int n = numberPlayers.value+2;
        PlayerPrefs.SetInt("Players", n);
        for (int i = 0; i < n; i++)
        {
            string key = "Player" + i;
            string name = playersName[i].GetComponent<InputField>().text;
            PlayerPrefs.SetString(key, name);
        }
        if(!timeUpdate.text.Equals(""))
            PlayerPrefs.SetInt("TimeUpdate", int.Parse(timeUpdate.text));
        if (!timeFight.text.Equals(""))
            PlayerPrefs.SetInt("TimeFight", int.Parse(timeFight.text));
        StartCoroutine(Toast());
    }

    IEnumerator Toast()
    {
        ToastTest.SetActive(true);
        yield return new WaitForSeconds(2);
        ToastTest.SetActive(false);
    }

    public void CancelSettings()
    {
        SceneManager.LoadScene("Home", LoadSceneMode.Single);
    }

    private void InitializeOptions()
    {
        //Initialize number of players
        int n = 2;
        if (PlayerPrefs.GetInt("Players") > 0)
            n = PlayerPrefs.GetInt("Players");
        numberPlayers.value = n-2;
        foreach (GameObject x in playersName)
            x.SetActive(false);
        for (int i = 0; i < n; i++)
            playersName[i].SetActive(true);
        //Initialize players
        for (int i = 0; i < n; i++)
        {
            string key = "Player" + i;
            playersName[i].GetComponent<InputField>().text = PlayerPrefs.GetString(key);
        }
        //Initialize timers
        Debug.Log(PlayerPrefs.GetInt("TimeUpdate").ToString());
        timeUpdate.text = PlayerPrefs.GetInt("TimeUpdate").ToString();
        timeFight.text = PlayerPrefs.GetInt("TimeFight").ToString();
    }

}
