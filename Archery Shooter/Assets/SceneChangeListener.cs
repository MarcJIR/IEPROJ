using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName(SceneNames.STAGES))
        {
            GameObject.Find("Canvas").transform.Find(UIReceiver.Level).gameObject.SetActive(true);
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName(SceneNames.CLASSIC_GAME))
        {
            //GameObject.Find("Canvas").transform.Find(UIReceiver.Level).transform.Find(UIReceiver.Stage).gameObject.SetActive(true);
        }
        if(SceneManager.GetSceneByName(SceneNames.PAUSE).isLoaded)
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
            //string[] stage = UIReceiver.Stage.Split;
            if (UIReceiver.Stage.EndsWith("10"))
            {
                GameObject[] gObj = SceneManager.GetSceneByName(SceneNames.CLASSIC_SUCCESS).GetRootGameObjects();
                gObj[0].transform.GetChild(2).Find("Level").gameObject.SetActive(true);
                gObj[0].transform.GetChild(2).Find("Stage").gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
