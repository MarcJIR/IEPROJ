using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIReceiver : MonoBehaviour
{
    public static string Level;
    public static string Stage;
    public static string Mode;
    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_RESUME, this.Resume);
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_GO_TO_STAGE, this.GoToStageSelect);
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_GO_TO_MAIN_MENU, this.GoToMenu);
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_SFX, this.SFX);
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_MUSIC, this.Music);
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_NEXT, this.Next);
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_RETRY, this.Retry);
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_PAUSE, this.Pause);
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_GO_TO_CLASSIC, this.GoToClassic);
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_GO_BACK, this.GoBack);
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_GO_TO_LEVEL, this.GoToLevelSelect);
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_GO_TO_ENDLESS, this.GoToEndless);
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_GO_TO_MODE, this.GoToMode);
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_GO_TO_SETTINGS, this.GoToSettings);
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_EXIT_GAME, this.ExitGame);
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_RESET, this.ResetGS);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.Archery_Events.ON_RESUME);
        EventBroadcaster.Instance.RemoveObserver(EventNames.Archery_Events.ON_GO_TO_STAGE);
        EventBroadcaster.Instance.RemoveObserver(EventNames.Archery_Events.ON_GO_TO_MAIN_MENU);
        EventBroadcaster.Instance.RemoveObserver(EventNames.Archery_Events.ON_SFX);
        EventBroadcaster.Instance.RemoveObserver(EventNames.Archery_Events.ON_MUSIC);
        EventBroadcaster.Instance.RemoveObserver(EventNames.Archery_Events.ON_NEXT);
        EventBroadcaster.Instance.RemoveObserver(EventNames.Archery_Events.ON_RETRY);
        EventBroadcaster.Instance.RemoveObserver(EventNames.Archery_Events.ON_PAUSE);
        EventBroadcaster.Instance.RemoveObserver(EventNames.Archery_Events.ON_GO_TO_CLASSIC);
        EventBroadcaster.Instance.RemoveObserver(EventNames.Archery_Events.ON_GO_BACK);
        EventBroadcaster.Instance.RemoveObserver(EventNames.Archery_Events.ON_GO_TO_LEVEL);
        EventBroadcaster.Instance.RemoveObserver(EventNames.Archery_Events.ON_GO_TO_ENDLESS);
        EventBroadcaster.Instance.RemoveObserver(EventNames.Archery_Events.ON_GO_TO_MODE);
        EventBroadcaster.Instance.RemoveObserver(EventNames.Archery_Events.ON_GO_TO_SETTINGS);
        EventBroadcaster.Instance.RemoveObserver(EventNames.Archery_Events.ON_EXIT_GAME); 
        EventBroadcaster.Instance.RemoveObserver(EventNames.Archery_Events.ON_RESET);
    }

    // Update is called once per frame
    private void Resume()
    {
        SceneManager.UnloadSceneAsync(SceneNames.PAUSE);
    }

    private void GoToMenu()
    {
        Level = " ";
        Stage = " ";
        Mode = " ";
        LoadManager.Instance.LoadScene(SceneNames.MAIN_MENU);
    }

    private void Pause()
    {
        LoadManager.Instance.LoadSceneAdditive(SceneNames.PAUSE, false);
        Time.timeScale = 0.0f;
    }

    private void GoToSettings()
    {
        LoadManager.Instance.LoadScene(SceneNames.SETTINGS);
    }
    //make levels as prefabs and just instantiate
    private void GoToClassic(Parameters parameter)
    {
        Time.timeScale = 1.0f;
        Stage = parameter.GetStringExtra("Stage", " ");
        LoadManager.Instance.LoadScene(SceneNames.CLASSIC_GAME);
    }
    
    private void GoToEndless(Parameters parameter)
    {
        Time.timeScale = 1.0f;
        Mode = parameter.GetStringExtra("Mode", " ");
        LoadManager.Instance.LoadScene(SceneNames.ENDLESS_GAME);
    }

    private void GoToMode()
    {
        LoadManager.Instance.LoadScene(SceneNames.MODE);
    }

    private void ResetGS()
    {
        for (int i = 1; i < 4; i++)
        {
            PlayerPrefs.SetInt("Level " + i, 0);
            PlayerPrefs.SetInt("Level 1", 1);
        }
        PlayerPrefs.Save();
    }

    private void SFX(Parameters parameter)
    {
        GameObject button = parameter.GetGameObjectExtra("Button");
        bool state = button.transform.Find("On").gameObject.activeSelf;
        if (state)
        {
            button.transform.Find("On").gameObject.SetActive(false);
            button.transform.Find("Off").gameObject.SetActive(true);
            PlayerPrefs.SetInt("SFX", 0);
            PlayerPrefs.Save();
        }
        else
        {
            button.transform.Find("On").gameObject.SetActive(true);
            button.transform.Find("Off").gameObject.SetActive(false);
            PlayerPrefs.SetInt("SFX", 1);
            PlayerPrefs.Save();
        }
    }

    private void Music(Parameters parameter)
    {
        GameObject button = parameter.GetGameObjectExtra("Button");
        bool state = button.transform.Find("On").gameObject.activeSelf;
        if (state)
        {
            button.transform.Find("On").gameObject.SetActive(false);
            button.transform.Find("Off").gameObject.SetActive(true);
            PlayerPrefs.SetInt("Music", 0);
            PlayerPrefs.Save();
        }
        else
        {
            button.transform.Find("On").gameObject.SetActive(true);
            button.transform.Find("Off").gameObject.SetActive(false);
            PlayerPrefs.SetInt("Music", 1);
            PlayerPrefs.Save();
        }
    }

    private void GoToLevelSelect(Parameters parameter)
    {
        Mode = parameter.GetStringExtra("Mode", Mode);
        LoadManager.Instance.LoadScene(SceneNames.LEVELS);
    }

    private void GoToStageSelect(Parameters parameter)
    {
        Level = parameter.GetStringExtra("Level", "");
        LoadManager.Instance.LoadScene(SceneNames.STAGES);

    }

    private void Retry()
    {
        Time.timeScale = 1.0f;
        if(Mode == "Classic")   LoadManager.Instance.LoadScene(SceneNames.CLASSIC_GAME);
        else if(Mode == "Endless") LoadManager.Instance.LoadScene(SceneNames.ENDLESS_GAME);
    }

    private void Next()
    {
        string[] stage = Stage.Split(' ');
        int num = int.Parse(stage[1]);
        if (num >= 10)
        {
            string[] level = Level.Split(' ');
            int lvl = int.Parse(level[1]);
            num = 1;
            lvl++;
            Stage = stage[0] + " " + num;
            Level = level[0] + " " + lvl;
        }
        else num++;
        Stage = stage[0] + " " + num;
        Time.timeScale = 1.0f;
        LoadManager.Instance.LoadScene(SceneNames.CLASSIC_GAME);
    }
    //Single fxn for going back, just determine what the active scene/what is loaded is then load the respective previous scene
    private void GoBack()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName(SceneNames.SETTINGS) || SceneManager.GetActiveScene() == SceneManager.GetSceneByName(SceneNames.MODE)) LoadManager.Instance.LoadScene(SceneNames.MAIN_MENU);
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName(SceneNames.LEVELS))
        {
            LoadManager.Instance.LoadScene(SceneNames.MODE);
            Level = " ";
            Mode = " ";
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName(SceneNames.STAGES))
        {
            LoadManager.Instance.LoadScene(SceneNames.LEVELS);
            Level = " ";
        }
    }

    private void ExitGame()
    {
        EventBroadcaster.Instance.RemoveAllObservers();
        Application.Quit();
    }
}
