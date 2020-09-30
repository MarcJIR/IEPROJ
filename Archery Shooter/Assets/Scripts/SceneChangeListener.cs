using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangeListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == SceneNames.MAIN_MENU)
        {
            for (int i = 1; i < 4; i++)
            {
                if (!PlayerPrefs.HasKey("Level " + i)) PlayerPrefs.SetInt("Level " + i, 0);
                if (PlayerPrefs.GetInt("Level 1") == 0)
                {
                    PlayerPrefs.SetInt("Level 1", 1);
                    PlayerPrefs.Save();
                }
            }
            if (!PlayerPrefs.HasKey("SFX"))
            {
                PlayerPrefs.SetInt("SFX", 1);
                PlayerPrefs.Save();
            }
            if (!PlayerPrefs.HasKey("Music"))
            {
                PlayerPrefs.SetInt("Music", 1);
                PlayerPrefs.Save();
            }
        }

        if(SceneManager.GetActiveScene().name == SceneNames.SETTINGS || SceneManager.GetSceneByName(SceneNames.PAUSE).isLoaded)
        {
            this.GlobalSettings();
        }

        if(SceneManager.GetActiveScene().name == SceneNames.LEVELS)
        {
            for (int i = 1; i < 4; i++)
            {
                string lvl = "Level " + i;
                if(PlayerPrefs.GetInt(lvl) > 0)
                {
                    GameObject.Find("Canvas").transform.Find(lvl).gameObject.GetComponent<Button>().interactable = true;
                }
            }
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName(SceneNames.STAGES))
        {
            GameObject lvl = GameObject.Find("Canvas").transform.Find(UIReceiver.Level).gameObject;
            lvl.SetActive(true);
            int i = 1;
            while(PlayerPrefs.GetInt(UIReceiver.Level) >= i)
            {
                lvl.transform.Find("Stage " + i).GetComponent<Button>().interactable = true;
                i++;
            }
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName(SceneNames.CLASSIC_GAME))
        {
            if(UIReceiver.Stage == "Stage 1" && UIReceiver.Level == "Level 1")
            {
                GameObject.Find("Canvas").transform.Find("Tutorial").gameObject.SetActive(true);
            }
            else
            {
                GameObject.Find("Canvas").transform.Find("Tutorial").gameObject.SetActive(false);
            }
            string obj = UIReceiver.Level + " " + UIReceiver.Stage;
            if (GameObject.Find(obj) == null)
            {
                //Debug.Log(obj);
                GameObject myStage = GameObject.Instantiate(Resources.Load("Prefabs/Stages/" + obj) as GameObject);
                myStage.SetActive(true);
            }
        }
        if (SceneManager.GetSceneByName(SceneNames.PAUSE).isLoaded)
        { 
            string mode = UIReceiver.Mode;
            GameObject[] list = SceneManager.GetSceneByName(SceneNames.PAUSE).GetRootGameObjects();
            foreach (GameObject obj in list)
            {
                if (obj.name == "Canvas")
                {
                    obj.transform.Find(mode).gameObject.SetActive(true);
                    if(mode == "Classic")
                    {
                        obj.transform.Find(mode).transform.Find("LevelStageText").GetComponent<UnityEngine.UI.Text>().text = UIReceiver.Level + " - " + UIReceiver.Stage;
                    }                    
                    break;
                }
            }
        }

        if (SceneManager.GetSceneByName(SceneNames.CLASSIC_SUCCESS).isLoaded)
        {
            if (UIReceiver.Stage.EndsWith("10"))
            {
                GameObject[] gObj = SceneManager.GetSceneByName(SceneNames.CLASSIC_SUCCESS).GetRootGameObjects();
                gObj[0].transform.GetChild(2).Find("Level").gameObject.SetActive(true);
                gObj[0].transform.GetChild(2).Find("Stage").gameObject.SetActive(false);
                string[] level = UIReceiver.Level.Split(' ');
                int lvl = int.Parse(level[1]) + 1;
                if (PlayerPrefs.GetInt(level[0] + ' ' + lvl) == 0)
                {
                    PlayerPrefs.SetInt(level[0] + ' ' + lvl, 1);
                    PlayerPrefs.Save();
                }
            }
            else
            {
                string[] stage = UIReceiver.Stage.Split(' ');
                int stg = int.Parse(stage[1]) + 1;
                if(PlayerPrefs.GetInt(UIReceiver.Level) < stg)
                {
                    PlayerPrefs.SetInt(UIReceiver.Level, stg);
                    PlayerPrefs.Save();
                }
            }
        }
    }

    private void GlobalSettings()
    {
        GameObject sfx;
        GameObject music;
        GameObject[] settings = GameObject.FindGameObjectsWithTag("GlobalSettings");
        if (settings[0].name == "SFX")
        {
            sfx = settings[0];
            music = settings[1];
        }
        else
        {
            sfx = settings[1];
            music = settings[0];
        }
        if (PlayerPrefs.GetInt("SFX") == 1)
        {
            sfx.transform.Find("On").gameObject.SetActive(true);
            sfx.transform.Find("Off").gameObject.SetActive(false);
        }
        else
        {
            sfx.transform.Find("On").gameObject.SetActive(false);
            sfx.transform.Find("Off").gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetInt("Music") == 1)
        {
            music.transform.Find("On").gameObject.SetActive(true);
            music.transform.Find("Off").gameObject.SetActive(false);
        }
        else
        {
            music.transform.Find("On").gameObject.SetActive(false);
            music.transform.Find("Off").gameObject.SetActive(true);
        }
    }
}
