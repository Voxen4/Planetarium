using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartExitButtonHandler : MonoBehaviour
{
    public void ExitButton()
    {
        Application.Quit();
    }

    public void StartButton()
    {
        ((GameMgr)GameMgr.Instance).TogglePauseMenu();
    }
}
