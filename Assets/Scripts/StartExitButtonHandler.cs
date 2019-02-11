using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Utility Klasse die auf Drücken des Start Buttons/ Stop Buttons Wartet und dann entsprechend die Simulation startet oder das Program beendet
/// </summary>
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
