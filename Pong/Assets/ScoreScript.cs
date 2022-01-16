using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    public static int player1Score;
    public static int player2Score;

    public Text score1;
    public Text score2;

    public static int sceneID = 1;

    // Start is called before the first frame update
    void Start()
    {
        if(sceneID == 2)
        {
            player1Score = 0;
            player2Score = 0;
            sceneID = 1;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(sceneID == 1)
        {
            score1 = GameObject.FindGameObjectWithTag("Score1").GetComponent<Text>();
            score2 = GameObject.FindGameObjectWithTag("Score2").GetComponent<Text>();

            score1.text = "" + player1Score;
            score2.text = "" + player2Score;

            if (player1Score == 5)
            {
                sceneID = 2;
                SceneManager.LoadScene("End");
            }
            else if (player2Score == 5)
            {
                sceneID = 2;
                SceneManager.LoadScene("End");
            }
        }
        
        else if(sceneID == 2)
        {
            Text winner = GameObject.FindGameObjectWithTag("Winner").GetComponent<Text>();
            if(player1Score == 5)
            {
                winner.text = "" + "PLAYER 1 WON!";
            }else if(player2Score == 5)
            {
                winner.text = "" + "PLAYER 2 WON!";
            }
            

        }
    }

    public void IncreaseScore1()
    {
        player1Score++;
        
    }

    public void IncreaseScore2()
    {
        player2Score++;
    }
}
