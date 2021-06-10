using Firebase;
using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ArabicSupport;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;

    [Header("Login")]
    public Text emailLoginField;
    public Text passwordLoginField;
    public Text warningLoginText;

    private bool validUser = false;
    public static string uid;
    void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
    }

    //Function for the login button
    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }

    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email + "@test.com", _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = ArabicFixer.Fix("البريد الإلكتروني مفقود", false, false);
                    break;
                case AuthError.MissingPassword:
                    message = ArabicFixer.Fix("كلمة المرور مفقودة", false, false); 
                    break;
                case AuthError.WrongPassword:
                    message = ArabicFixer.Fix("كلمة مرور خاطئة", false, false); 
                    break;
                case AuthError.InvalidEmail:
                    message = ArabicFixer.Fix("بريد إلكتروني خاطئ", false, false);
                    break;
                case AuthError.UserNotFound:
                    message = ArabicFixer.Fix("الحساب غير موجود", false, false);
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result;
            validUser = true;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            
            
            //warningLoginText.text = "Logged In";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (validUser)
        {
            validUser = false;
            uid = User.UserId;
            Debug.Log("USER UID" + uid);
            SceneManager.LoadScene("ChooseMode");
        }
        
    }
}
