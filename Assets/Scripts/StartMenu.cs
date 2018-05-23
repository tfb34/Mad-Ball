using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {
	public Text bestScore;
	public Text playerName;
	// Use this for initialization
	void Start () {
		//PlayerPrefs.DeleteAll ();
		print("best Score: "+PlayerPrefs.GetInt("bestScore"));
		if (PlayerPrefs.GetInt ("bestScore")>0) {
			bestScore.text = "Best Time: " + PlayerPrefs.GetString ("timeString")+"s";
			if (PlayerPrefs.GetString ("playerName") != null)
				playerName.text = PlayerPrefs.GetString ("playerName");
		}else
			bestScore.text = null;
	}
		
    public void startLevel()
    {
        SceneManager.LoadScene(1);
    }
	public void quit(){
		Application.Quit();
	}
}
