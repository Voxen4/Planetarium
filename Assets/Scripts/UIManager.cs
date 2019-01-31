using UnityEngine;
using UnityEditor;

public class UIManager : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            GameMgr.instance.TogglePauseMenu();
        }
    }


}