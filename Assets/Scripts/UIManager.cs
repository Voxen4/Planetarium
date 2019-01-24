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
            if(GM != null)
            {
            GM.TogglePauseMenu();
            }
            else
            {
                GameMgr.instance.TogglePauseMenu();
            }
        }
    }
    

}