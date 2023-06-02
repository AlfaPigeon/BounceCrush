using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndGameScript : MonoBehaviour
{
    // Start is called before the first frame update



    public GameObject tutorial;
    void Start()
    {
       
        try
        {
            float hscore = PlayerPrefs.GetFloat("HighScore");
            GameObject.FindGameObjectWithTag("HighScore").GetComponent<TMPro.TextMeshProUGUI>().text = "HIGHSCORE: " + hscore;
        }
        catch
        {
            Debug.Log("There is no highscore tag");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Replay()
    {
        

        if (PlayerPrefs.GetInt("Tutorial") == 0)
        {
            tutorial.SetActive(true);
            PlayerPrefs.SetInt("Tutorial",1);
            return;
        }

        SceneManager.LoadScene(1);
    }

    public void MainScreen()
    {
        SceneManager.LoadScene(0);
    }
}
