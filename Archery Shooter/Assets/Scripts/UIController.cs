using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public void CallPlayEvent()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.Archery_Events.ON_GO_TO_MODE);
    }

    public void CallSettingsEvent()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.Archery_Events.ON_GO_TO_SETTINGS);
    }

    public void CallPauseEvent()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.Archery_Events.ON_PAUSE);
    }

    public void CallResumeEvent()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.Archery_Events.ON_RESUME);
        Time.timeScale = 1.0f;
    }

    public void CallSFXEvent(Button button)
    {
        Parameters parameter = new Parameters();
        parameter.PutExtra("State", button.name);
        EventBroadcaster.Instance.PostEvent(EventNames.Archery_Events.ON_SFX, parameter);
    }

    public void CallMusicEvent(Button button)
    {
        Parameters paramater = new Parameters();
        paramater.PutExtra("State", button.name);
        EventBroadcaster.Instance.PostEvent(EventNames.Archery_Events.ON_MUSIC, paramater);
    }

    public void CallBackEvent()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.Archery_Events.ON_GO_BACK);
    }

    public void CallMainMenuEvent()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.Archery_Events.ON_GO_TO_MAIN_MENU);
    }

    public void CallLevelEvent(Button button)
    {
        Parameters parameter = new Parameters();
        parameter.PutExtra("Mode", button.name);
        EventBroadcaster.Instance.PostEvent(EventNames.Archery_Events.ON_GO_TO_LEVEL, parameter);
    }
    public void CallStageEvent(Button button)
    {
        Parameters parameter = new Parameters();
        if (button.name.Contains("Level"))
        {
            parameter.PutExtra("Level", button.name);
        }
        else parameter.PutExtra("Level", UIReceiver.Level);
        EventBroadcaster.Instance.PostEvent(EventNames.Archery_Events.ON_GO_TO_STAGE, parameter);
    }

    public void CallEndlessEvent(Button button)
    {
        Parameters parameter = new Parameters();
        parameter.PutExtra("Mode", button.name);
        EventBroadcaster.Instance.PostEvent(EventNames.Archery_Events.ON_GO_TO_ENDLESS, parameter);
    }
    public void CallClassicEvent(Button button)
    {
        Parameters parameter = new Parameters();
        parameter.PutExtra("Stage", button.name);
        EventBroadcaster.Instance.PostEvent(EventNames.Archery_Events.ON_GO_TO_CLASSIC, parameter);
    }

    public void CallNextEvent()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.Archery_Events.ON_NEXT);
    }

    public void CallRetryEvent()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.Archery_Events.ON_RETRY);
    }

    public void CallExitEvent()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.Archery_Events.ON_EXIT_GAME);
    }

    public void CallReset()
    {
        for (int i = 1; i < 4; i++)
        {
            PlayerPrefs.SetInt("Level " + i, 0);
            PlayerPrefs.SetInt("Level 1", 1);
        }
        PlayerPrefs.Save();
    }

}
