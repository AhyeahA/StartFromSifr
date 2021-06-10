using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    private Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = this.gameObject.GetComponent<Button>();
        btn.onClick.AddListener(SelectOnClick);
    }

    public void SelectOnClick()
    {
        Debug.Log("Select on click called");
        if (SceneManager.GetActiveScene().name.Equals("Level 2 Room 3"))
        {
            SceneManager.LoadScene(3); //load back to main floor
        }else if (SceneManager.GetActiveScene().name.Equals("Login") || SceneManager.GetActiveScene().name.Equals("Register"))
        {
            SceneManager.LoadScene("LogReg"); //load back login or regsiter
        }
        else if (SceneManager.GetActiveScene().name.Equals("MainFloor"))
        {
            SceneManager.LoadScene("ChooseLevel"); //go back to choose level screen
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
