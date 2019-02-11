using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartExitButtonHandler : MonoBehaviour
{
    
    ///  Exit Button zum schließen der APP
    public void ExitButton()
    {
        Application.Quit();
    }

    /// Starten der Scene über den StartKnopf, Pausemenü wird beendet und Startkoordinaten übergeben
    public void StartButton()
    {
        ((GameMgr)GameMgr.Instance).TogglePauseMenu();
    }
}
