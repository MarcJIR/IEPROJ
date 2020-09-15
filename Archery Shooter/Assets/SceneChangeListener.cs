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
                    break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
