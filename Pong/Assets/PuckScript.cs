using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuckScript : MonoBehaviour
{
    public float speed = 7;
    Vector3 direction;

    float puckRadius;
    float screenHeight, screenWidth;

    GameObject player1, player2;
    Vector3 player1Dim, player2Dim;

    float player1x, player1y, player1h, player1w;
    float player2x, player2y, player2h, player2w;

    float ct;

    [SerializeField] GameObject score;

    Vector3[] directions = new Vector3[] {
        new Vector3(-1,0,0).normalized,
        new Vector3(1,0,0).normalized,
        new Vector3(-1,1,0).normalized,
        new Vector3(-1,-1,0).normalized,
        new Vector3(1,1,0).normalized,
        new Vector3(1,-1,0).normalized

    };

    // Start is called before the first frame update
    void Start()
    {

        screenHeight = Screen.height / 100;
        screenWidth = Screen.width / 100;
        Vector3 dim = gameObject.GetComponent<SpriteRenderer>().bounds.size;

        puckRadius = dim.x / 2;


        transform.position = new Vector3(0, 0, 0);

        direction = directions[(int)Random.Range(0, directions.Length-1e-6f)];

        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");

        


    }

    // Update is called once per frame
    void Update()
    {
        ct += Time.deltaTime;

        if(ct <= 2)
        {
            return;
        }
        transform.position += direction * speed * Time.deltaTime;

        if (transform.position.y + puckRadius >= screenHeight / 2 && direction.y > 0)
        {
            direction.y = -direction.y;
        }
        if (transform.position.y - puckRadius <= -screenHeight / 2 && direction.y < 0)
        {
            direction.y = -direction.y;
        }


        if(direction.x > 0)
        {
            if (CollisionDetection(player2) )
            {
                float angle = Angle(player2);
                direction = PolarToCart(1, -angle + 180);

            }
        }
        else
        {   
            if (CollisionDetection(player1))
            {
                float angle = Angle(player1);
                direction = PolarToCart(1, angle);

            }
        }


        if(transform.position.x <= -screenWidth / 2)
        {

            score.GetComponent<ScoreScript>().IncreaseScore2();
            SceneManager.LoadScene("Main");
        }
        else if (transform.position.x >= screenWidth / 2)
        {
            score.GetComponent<ScoreScript>().IncreaseScore1();
            SceneManager.LoadScene("Main");
        }
    }


    bool CollisionDetection(GameObject player)
    {
        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;

        Vector3 playerDim = player.GetComponent<SpriteRenderer>().bounds.size;

        float playerH = playerDim.y;
        float playerW = playerDim.x;

        Vector3 cp = closestPoint(
            new Rect(playerX - playerW / 2, playerY - playerH / 2, playerW, playerH),
            transform.position,
            puckRadius);


        return Vector3.Distance(cp, transform.position) < puckRadius;
    }

    Vector3 closestPoint(Rect rect, Vector2 center, float R)
    {
        float u = rect.y + rect.height;
        float d = rect.y;
        float l = rect.x;
        float r = rect.x + rect.width;

        // gore
        if(center.x  <= r && center.x >=l  && center.y >= u)
        {
            return new Vector3(center.x,u,0);
        }

        // dolje
        else if(center.x <= r && center.x >= l && center.y <= d)
        {
            return new Vector3(center.x, d, 0);
        }

        // lijevo
        else if(center.x <= l && center.y >= d && center.y <= u)
        {
            return new Vector3(l, center.y, 0);
        }

        // desno 
        else if (center.x >= r && center.y >= d && center.y <= u)
        {
            return new Vector3(r, center.y, 0);
        }

        // gore lijevo
        else if(center.x <= l && center.y >= u)
        {
            return new Vector3(l, u, 0);
        }
        // gore desno
        else if(center.x >= d && center.y >= u)
        {
            return new Vector3(r, u, 0);
        }

        // dolje lijevo
        else if(center.x <= l && center.y <= d)
        {
            return new Vector3(l, d, 0);
        }
        // dolje desno
        else
        {
            return new Vector3(r, d, 0);
        }


    }


    float Angle(GameObject player)
    {
        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;

        Vector3 playerDim = player.GetComponent<SpriteRenderer>().bounds.size;

        float playerH = playerDim.y;
        float playerW = playerDim.x;

        //iznad
        if(transform.position.y >= playerY + playerH / 2)
        {
            return 75;
        }
        // ispod
        else if(transform.position.y <= playerY - playerH / 2){
            return -75;
        }
        else
        {
            return LinearTrans(playerY - playerH/2, playerH);
        }
    }

    float LinearTrans(float rectY, float rectH)
    {
        return (transform.position.y - rectY) / rectH * 150 - 75;
    }


    Vector3 PolarToCart(float r, float theta)
    {
        return new Vector3(r * Mathf.Cos(theta * Mathf.Deg2Rad), r * Mathf.Sin(theta*Mathf.Deg2Rad), 0);
    }

}
