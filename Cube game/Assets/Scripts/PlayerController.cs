using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText; 
    private Rigidbody rb;
    private int count;
    public Text WinText;
    public Text loseText;
    public bool GameOver;
    public Text ReturnText;
    public AudioClip Bellclip;
    public AudioSource BellSource;
    public Text HighScore;
    public Text GameOverText;
    public bool canJump; 
  
  
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText ();
        WinText.text = "";
        ReturnText.text = "";
        BellSource.clip = Bellclip;
        GameOverText.text = "";

        HighScore.text =  "High Score: " + PlayerPrefs.GetInt("High Score", 0).ToString(); 

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = 0.7f;
        float moveUp = 0.0f;
       

        if(Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            moveUp = 60.0f;
            canJump = false; 
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveUp = -60.0f;
        }

        Vector3 movement = new Vector3(moveHorizontal, moveUp, moveVertical);

        rb.AddForce(movement * speed);

        if(rb.position.y < -10)
        {
            if(rb.position.z < 246)
            {
                loseText.text = "You lose! Score:" + count.ToString();
                ReturnText.text = "Press M to Return to Main Menu";
                GameOver = true;
            }
            

        }

        if (GameOver == true)
        {
            
            if (Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene(0);
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();  
        }
        
        if(rb.position.z >= 246)
        {
            if (rb.position.y >= -10)
            {
                GameOverText.text = "Game Over!";
                ReturnText.text = "Press M to Return to Main Menu";
                GameOver = true;
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            BellSource.Play(); //play the sound
        
        }

       

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Solid"))
        {
            canJump = true; 
        }
    }
    void SetCountText ()
    {
        countText.text = "Score: " + count.ToString();

        
      
        if (count > PlayerPrefs.GetInt("High Score", 0)) {
            PlayerPrefs.SetInt("High Score", count);
            HighScore.text =  "High Score: " + count.ToString();
        }
        
    }

    
}
