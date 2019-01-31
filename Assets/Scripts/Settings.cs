using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings: MonoBehaviour
{
    public GameObject[] playersName;
    public Text numberPlayers;
    public Text timeUpdate;
    public Text timeFight;
    public GameObject ToastTest;

    private void Start()
    {
        ChangeNumberOfPlayer();
    }

    public void ChangeNumberOfPlayer()
    {
        int n = int.Parse(numberPlayers.text);
        foreach (GameObject x in playersName)
            x.SetActive(false);
        for (int i = 0; i < n; i++)
            playersName[i].SetActive(true);
    }

    public void ConfirmSettings()
    {
        int n = int.Parse(numberPlayers.text);
        for (int i = 0; i < n; i++)
        {
            string key = "Player" + i;
            string name = playersName[i].transform.Find("Text").GetComponent<Text>().text;
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
}
