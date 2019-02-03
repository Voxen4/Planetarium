using UnityEngine;
using UnityEditor;

public class UIManager : MonoBehaviour
{
    public GameObject trackedPlanet;
    public GameObject planetDataTable;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            ((GameMgr)GameMgr.Instance).TogglePauseMenu();
        }
    }

    void OnGUI()
    {
        if(planetDataTable != null && trackedPlanet != null)
        {
            var planet = trackedPlanet.GetComponent<PlanetModel>();
            var position = planetDataTable.transform.Find("Position").GetComponent<UnityEngine.UI.Text>();
            position.text = string.Format("Position\nX: {0}\nY: {1}\nZ: {2}\n", planet.position.x, planet.position.y, planet.position.z);
            var velocity = planetDataTable.transform.Find("Velocity").GetComponent<UnityEngine.UI.Text>();
            velocity.text = string.Format("Velocity\nX: {0}\nY: {1}\nZ: {2}\n", planet.velocity.x, planet.velocity.y, planet.velocity.z);
        }
        if(planetDataTable != null && trackedPlanet == null)
        {
            var position = planetDataTable.transform.Find("Position").GetComponent<UnityEngine.UI.Text>();
            position.text = "";
            var velocity = planetDataTable.transform.Find("Velocity").GetComponent<UnityEngine.UI.Text>();
            velocity.text = "";
        }
      //  planetDataTable.
    }


}