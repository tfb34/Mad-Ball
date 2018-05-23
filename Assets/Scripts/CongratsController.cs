using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CongratsController : MonoBehaviour {
	AudioSource music;

	void start(){
		print ("hello");
		music = GetComponent<AudioSource> ();
		print (music);

	}
	public void StartLevel()
	{
		SceneManager.LoadScene(1);

	}


}
