using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogRegBtn : MonoBehaviour
{
    public void LoginBtn()
    {
        SceneManager.LoadScene("Login");
    }
    public void RegBtn()
    {
        SceneManager.LoadScene("Register");
    }
}
