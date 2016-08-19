using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameOverController : MonoBehaviour {
  
    public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }
}
