using UnityEngine;

public class ErrorReporter : MonoBehaviour
{
	public string debugText = string.Empty;
	private string postURL = "http://www.mercuryleaf.com/report_robo_v13.php";
	private string app_name = "Rescue_Roby_Full";
	private FPS fps;
}
