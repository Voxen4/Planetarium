using UnityEngine;
using UnityEditor;

public class UIManager : MonoBehaviour
{
    public GameMgr GM;
	void Start ()
	{

	}

    void Update ()
	{
        if (Input.GetKeyDown("escape"))
        {
            GM.TogglePauseMenu();
        }
    }
    

}