using UnityEngine;
using System.Collections;

public class walk : MonoBehaviour {
    private bool goRight = true;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (goRight)
        {
            transform.Translate(new Vector3(10, 0, 0) * Time.deltaTime);
            
        }
        else
        {
            transform.Translate(new Vector3(-10, 0, 0) * Time.deltaTime);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("rightWall"))
        {
            goRight = false;
 
        }
        else if (other.gameObject.CompareTag("leftWall"))
        {
            goRight = true;
        }
    }
}
