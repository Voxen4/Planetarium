using UnityEngine;
using UnityEditor;
/// <summary>
/// Klasse Zum Verarbeiten von Tasteneingaben in beiden Szenen und Anzeige des Datums und der Planet Informationen in der Hautpszene
/// </summary>
public class UIManager : MonoBehaviour
{
    public GameObject trackedPlanet;
    public GameObject planetDataTable;
    public GameObject dateDisplayObject;
    public int tageCounter;
    void Start()
    {

    }

    /// <summary>
    /// Update Methode, wartet auf drücken der ESC Taste und führt dann einen Szenen Wechsel aus.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            ((GameMgr)GameMgr.Instance).TogglePauseMenu();
        }
    }

    /// <summary>
    /// OnGUI Methode aktualisiert bei ausgewähltem Planeten die Positions und Velocity Informationen,
    /// bei abwählen eines Planeten löscht es die Informationen. Aktualisiert außerdem das Datum in der GUI.
    /// </summary>
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