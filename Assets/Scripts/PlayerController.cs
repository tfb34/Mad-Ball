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
    

    void Start()
    {
        //gameOver = GetComponent<Canvas>();
        Time.timeScale = 1;
        gameOverMenu.SetActive(false);
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        colors = new Color[] {Color.red,Color.black,Color.gray };
        hurtAudio = GetComponent<AudioSource>();

        count = 42;
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
        if (count == 0)
        {
            winText.text = "No life! But good job";

        }
        else if (purse < 0)
        {
            //stop game
            //activate menu
            gameOverMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            score.text = "Score : " + purse.ToString();
        }
    }
   void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("enemy"))
        {
            hurtAudio.Play();
            purse = purse - 3;
            setScore();
            StartCoroutine(hurt());
            
            //rend.material.color = Color.white;
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
