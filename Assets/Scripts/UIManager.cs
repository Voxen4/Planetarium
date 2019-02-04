using UnityEngine;
using UnityEditor;

public class UIManager : MonoBehaviour
{
    public GameObject trackedPlanet;
    public GameObject planetDataTable;
    public GameObject dateDisplayObject;
    public int tageCounter;
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
        var customDate = ((GameMgr)GameMgr.Instance).getDate();
        if (planetDataTable != null && trackedPlanet != null)
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
        if (dateDisplayObject != null && customDate != null)
        {
            var dateObject = dateDisplayObject.transform.Find("Date").GetComponent<UnityEngine.UI.Text>();
            var date = new System.DateTime(customDate.Year, customDate.Month, customDate.Day);

            if(tageCounter > 0)date = date.AddDays(tageCounter);
            dateObject.text = date.ToString("MM/dd/yyyy");
        }
      //  planetDataTable.
    }


}