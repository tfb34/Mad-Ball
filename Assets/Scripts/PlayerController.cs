using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerController : MonoBehaviour {
    private Rigidbody rb;
    private Renderer rend;
    private int count;
    private int purse;
    private Color[] colors;
    private AudioSource hurtAudio;

    public float speed;
    public Text score;
    public Text winText;
    public GameObject gameOverMenu;
	public Text countDown;
	public float timer = 99;
	const int maxTime = 99;
	private int numDiamonds = 30; //31 including the bonus diamond
	public GameObject congratsMenu;
	public Text finalScore;
	public GameObject mainCamera;
	public Text gameOverFinalScore;
	public Text diamondsLeft;
	public InputField playerInputField;
	public Button enterNameButton;

    void Start()
    {
        //gameOver = GetComponent<Canvas>();
        Time.timeScale = 1;
        gameOverMenu.SetActive(false);
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        colors = new Color[] {Color.red,Color.black,Color.gray };
        hurtAudio = GetComponent<AudioSource>();
		enterNameButton.onClick.AddListener(savePlayerName);
        count = 30;//46,30
        purse = 0;
        setScore();
        winText.text = "";
    }
	void FixedUpdate()
    {
        // the horizontal and vertical axes are controlled by the keys on keyboard
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal,0.0f,moveVertical);
        rb.AddForce(movement*speed);
    }
	// gameOver everytime
	void Update(){
		timer -= Time.deltaTime;
		countDown.text = timer.ToString ("f0");
		if (timer <= 0) {
			Time.timeScale = 0;
			lowerMusic();
			gameOverMenu.SetActive (true);
		}
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pickUp"))
        {
            other.gameObject.SetActive(false);
            count=count-1; //counts 
            purse = purse + 1;
            setScore();
        }else if(other.gameObject.CompareTag("BombBall"))
        {
            other.gameObject.SetActive(false);
            hurtAudio.Play();
            purse = purse - 1;
            setScore();
        }
    }

    void setScore()//needs adjustment
    {
		print ("count: "+count);
        if (count == 0)
        {
			lowerMusic ();
			Time.timeScale = 0;
			congratsMenu.SetActive (true);
			int time = calculateScore ();
			//set best players score
			if (PlayerPrefs.GetInt ("bestScore") ==0 || time < PlayerPrefs.GetInt ("bestScore")) {
				finalScore.text = "Record Time " + time+"s";
				playerInputField.gameObject.SetActive(true);
				enterNameButton.gameObject.SetActive(true);
			} else {
				finalScore.text = time.ToString()+"s";
				playerInputField.gameObject.SetActive(false);
				enterNameButton.gameObject.SetActive(false);
			}
			evaluateScore (time);
        }
        else if (purse < 0)//gameOver
        {
			lowerMusic ();
            gameOverMenu.SetActive(true);
            Time.timeScale = 0;
        }
		else
        {
			score.text = "Collected " + purse+" / "+ numDiamonds;
			diamondsLeft.text = "Left: " + count;
			print (numDiamonds);
        }
    }

	// only called when player has collected all coins
	void evaluateScore(int time){
		if (PlayerPrefs.GetInt ("bestScore") == 0 || (PlayerPrefs.GetInt ("bestScore") > time) ) {
			PlayerPrefs.SetInt ("bestScore", time);// int
			PlayerPrefs.SetString ("timeString", (maxTime - timer).ToString("f0"));//string
		}
			
	}

	void savePlayerName(){
		if (playerInputField.text == null)
			PlayerPrefs.SetString ("playerName", "Anonymous");
		else {
			PlayerPrefs.SetString ("playerName", playerInputField.text);
		}
	}

	int calculateScore(){
		return (int)(maxTime-timer);
	}

	void lowerMusic(){
		mainCamera.GetComponent<AudioSource> ().volume = 0.2f;
	}

   	void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("enemy"))
        {
            hurtAudio.Play();
            purse = purse - 3;
            setScore();
            StartCoroutine(hurt());
        }
    }
    IEnumerator hurt()
    {
      for(var x=0; x<5; x++)
        {
            rend.material.color = Color.white;
            yield return new WaitForSeconds(0.1F);
            rend.material.color= colors[Random.Range(0, colors.Length)];
            yield return new WaitForSeconds(0.1F);
        }
       rend.material.color = Color.white;
    }


}
