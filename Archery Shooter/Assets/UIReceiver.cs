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
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_NEXT, this.NextStage);
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_RETRY, this.Retry);
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_PAUSE, this.Pause);
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_GO_TO_CLASSIC, this.GoToClassic);
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_GO_BACK, this.GoBack);
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_GO_TO_LEVEL, this.GoToLevelSelect);
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_GO_TO_ENDLESS, this.GoToEndless);
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_GO_TO_MODE, this.GoToMode);
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_GO_TO_SETTINGS, this.GoToSettings);
        EventBroadcaster.Instance.AddObserver(EventNames.Archery_Events.ON_EXIT_GAME, this.ExitGame);
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
    private void GoToClassic()
    {
        LoadManager.Instance.LoadScene(SceneNames.CLASSIC_GAME);
    }
    
    private void GoToEndless(Parameters parameter)
    {
        Mode = parameter.GetStringExtra("Mode", " ");
        LoadManager.Instance.LoadScene(SceneNames.ENDLESS_GAME);
    }

    private void GoToMode()
    {
        LoadManager.Instance.LoadScene(SceneNames.MODE);
    }

    private void SFX(Parameters parameter)
    {
        string name = parameter.GetStringExtra("State", "");
        Debug.Log(name);
    }

    private void Music(Parameters parameter)
    {
        string name = parameter.GetStringExtra("State", "");
        Debug.Log(name);
    }

    private void GoToLevelSelect(Parameters parameter)
    {
        Mode = parameter.GetStringExtra("Mode", " ");
        LoadManager.Instance.LoadScene(SceneNames.LEVELS);
    }

    private void GoToStageSelect(Parameters parameter)
    {
        Level = parameter.GetStringExtra("Level", "");
        LoadManager.Instance.LoadScene(SceneNames.STAGES);

    }

    private void Retry()
    {

    }

    private void NextStage()
    {

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
