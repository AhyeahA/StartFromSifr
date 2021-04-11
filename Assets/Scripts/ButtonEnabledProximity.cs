using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEnabledProximity : MonoBehaviour
{
    // Start is called before the first frame update
    static public bool isCloseby;
    static public bool isClicked = false;
    private Button btn;
    void Start()
    {
        btn = this.gameObject.GetComponent<Button>();
        btn.onClick.AddListener(SelectOnClick);
    }

    public void SelectOnClick()
    {
        isClicked = true;
        Debug.Log("Select on click");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCloseby)
        {
            btn.interactable = true;
        }
        else
        {
            btn.interactable = false;
            
        }
    }
}
