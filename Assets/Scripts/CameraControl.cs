using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CameraControl : MonoBehaviour //for LEVEL 1 ROOM 3 (first person controller)
{
    public float speed;
    public ControlButtons frontBtn, backBtn, rightBtn,leftBtn, rotateLeftBtn, rotateRightBtn, selectBtn;
    private string collidedPic;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {

        if (rightBtn.isPressed)
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (leftBtn.isPressed)
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (backBtn.isPressed)
        {
            transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
        }
        if (frontBtn.isPressed)
        {
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }
        if (rotateLeftBtn.isPressed)
        {
            Vector3 rotation = transform.eulerAngles;

            rotation.y -= 50f * Time.deltaTime; // Standart Left-/Right Arrows and A & D Keys

            transform.eulerAngles = rotation;
        }
        if (rotateRightBtn.isPressed)
        {
            Vector3 rotation = transform.eulerAngles;

            rotation.y += 50f * Time.deltaTime; // Standart Left-/Right Arrows and A & D Keys

            transform.eulerAngles = rotation;
        }
        if (selectBtn.isPressed)
        {
           // isPressed = false;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(SceneManager.GetActiveScene().name.Equals("Level 1 Room 3"))
        {
            Debug.Log(collision.gameObject.GetComponent<Renderer>().material.name);
            if(!collision.gameObject.GetComponent<Renderer>().material.name.Contains("New Material"))
            {
                
            collidedPic = collision.gameObject.GetComponent<Renderer>().material.mainTexture.name;
            
            Debug.Log("collidePic"+ collidedPic);
            }
            
        }
            
            
    }

    public void selectL1R3()
    {
        DisplayLetterForm.chosenAnswer = collidedPic;
        DisplayLetterForm.answerSet = true;
    }

    /*
    public void goFront()
    {
        gofrontbool = true;
        gobackbool = goleftbool = gorightbool = gorotateleftbool = gorotaterightbool = false;
    }

    public void goBack()
    {
        gobackbool = true;
        gofrontbool = goleftbool = gorightbool = gorotateleftbool = gorotaterightbool = false;
    }

    public void goRight()
    {
        gorightbool = true;
        gofrontbool = gobackbool = goleftbool = gorotateleftbool = gorotaterightbool = false;
    }
    public void goLeft()
    {
        goleftbool = true;
        gofrontbool  = gobackbool = gorightbool = gorotateleftbool = gorotaterightbool = false;
    }
    public void rotateRight()
    {
        gorotaterightbool = true;
        gofrontbool = gobackbool = goleftbool = gorightbool = gorotateleftbool  = false;
    }
    public void rotateLeft()
    {
        gorotateleftbool = true;
        gofrontbool = goleftbool = gorightbool = gobackbool = gorotaterightbool = false;
    }
    public void selectAnswerBtn()
    {
        //gofrontbool = goleftbool = gorightbool = gorotateleftbool = gorotaterightbool = false;
    }

    

    //unused atm
    public void TaskOnClick()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 rotation = transform.eulerAngles;

            rotation.y -= 50f * Time.deltaTime; // Standart Left-/Right Arrows and A & D Keys

            transform.eulerAngles = rotation;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 rotation = transform.eulerAngles;

            rotation.y += 50f * Time.deltaTime; // Standart Left-/Right Arrows and A & D Keys

            transform.eulerAngles = rotation;
        }
    }*/
}
