using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 8f;

    float objHeight, objWidth;
    float screenHeight, screenWidth;

    [SerializeField] int ID;

    // Start is called before the first frame update
    void Start()
    {
        screenHeight = Screen.height/100;
        screenWidth = Screen.width/100;

        if(ID == 1)
        {
            transform.position = new Vector3(-screenWidth / 2 + 1, 0, 0);
        }
        else if(ID == 2)
        {
            transform.position = new Vector3(screenWidth / 2 - 1, 0, 0);
        }

        Vector3 dim = gameObject.GetComponent<SpriteRenderer>().bounds.size;
        objHeight = dim.y;
        objWidth = dim.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(ID == 1)
        {
            Movement(KeyCode.W, KeyCode.S);
        }
        else if(ID == 2)
        {
            Movement(KeyCode.UpArrow, KeyCode.DownArrow);
        }
    }

    void Movement(KeyCode up, KeyCode down)
    {
        if (Input.GetKey(up))
        {
            if (transform.position.y + objHeight / 2 <= screenHeight / 2 - 0.25)
            {
                transform.position += new Vector3(0, 1, 0) * speed * Time.deltaTime;
            }

        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();

        }
        if (Input.GetKey(down))
        {
            if (transform.position.y - objHeight / 2 >= -screenHeight / 2 + 0.25)
            {
                transform.position -= new Vector3(0, 1, 0) * speed * Time.deltaTime;
            }

        }
    }
}
