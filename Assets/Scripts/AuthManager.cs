using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;

    //Login variables
    [Header("Login")]
    public InputField emailLoginField;
    public InputField passwordLoginField;
    //public TMP_Text warningLoginText;
    //public TMP_Text confirmLoginText;

    //Register variables
    //[Header("Register")]
    //public InputField usernameRegisterField;
    //public InputField emailRegisterField;
    //public TMP_InputField passwordRegisterField;
    //public TMP_InputField passwordRegisterVerifyField;
    //public TMP_Text warningRegisterText;

    void Awake()
    {
        Debug.Log("Start called");
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Debug.Log("available for use");
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                //app = Firebase.FirebaseApp.DefaultInstance;
                auth = FirebaseAuth.DefaultInstance;
                

                InitializeFirebase();
                //await stu.SetAsync(city, SetOptions.MergeAll);
                //stuRef = db.Collection("Users");
                //savedata();
                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
        //Check that all of the necessary dependencies for Firebase are present on the system
        //FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        //{
        //    dependencyStatus = task.Result;
        //    if (dependencyStatus == DependencyStatus.Available)
        //    {
        //        //If they are avalible Initialize Firebase
        //        InitializeFirebase();
        //    }
        //    else
        //    {
        //        Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
        //    }
        //});
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        //auth = FirebaseAuth.DefaultInstance;
        ////Register("test@gmail.com", "123");
        auth.CreateUserWithEmailAndPasswordAsync("test@gmail.com", "1234abcd").ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }

    //private void Start()
    //{
    //    StartCoroutine(Register("test@gmail.com", "123"));
    //}

    //Function for the login button
    //public void LoginButton()
    //{
    //    //Call the login coroutine passing the email and password
    //    StartCoroutine(Register(emailLoginField.text, passwordLoginField.text));
    //}
    //Function for the register button
    //public void RegisterButton()
    //{
    //    //Call the register coroutine passing the email, password, and username
    //    StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    //}

    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            //string message = "Login Failed!";
            //switch (errorCode)
            //{
            //    case AuthError.MissingEmail:
            //        message = "Missing Email";
            //        break;
            //    case AuthError.MissingPassword:
            //        message = "Missing Password";
            //        break;
            //    case AuthError.WrongPassword:
            //        message = "Wrong Password";
            //        break;
            //    case AuthError.InvalidEmail:
            //        message = "Invalid Email";
            //        break;
            //    case AuthError.UserNotFound:
            //        message = "Account does not exist";
            //        break;
            //}
            //warningLoginText.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            //warningLoginText.text = "";
            //confirmLoginText.text = "Logged In";
        }
    }

    private void Register(string _email, string _password)
    {
        //if (_username == "")
        //{
        //    //If the username field is blank show a warning
        //    warningRegisterText.text = "Missing Username";
        //}
        //else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        //{
        //    //If the password does not match show a warning
        //    warningRegisterText.text = "Password Does Not Match!";
        //}
        //else
        //}
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            //yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                //string message = "Register Failed!";
                //switch (errorCode)
                //{
                //    case AuthError.MissingEmail:
                //        message = "Missing Email";
                //        break;
                //    case AuthError.MissingPassword:
                //        message = "Missing Password";
                //        break;
                //    case AuthError.WeakPassword:
                //        message = "Weak Password";
                //        break;
                //    case AuthError.EmailAlreadyInUse:
                //        message = "Email Already In Use";
                //        break;
                //}
                //warningRegisterText.text = message;
            }
            else
            {
                //User has now been created
                //Now get the result
                //User = RegisterTask.Result;

                //if (User != null)
                //{
                //    //Create a user profile and set the username
                //    UserProfile profile = new UserProfile { DisplayName = _username };

                //    //Call the Firebase auth update user profile function passing the profile with the username
                //    var ProfileTask = User.UpdateUserProfileAsync(profile);
                //    //Wait until the task completes
                //    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                //    if (ProfileTask.Exception != null)
                //    {
                //        //If there are errors handle them
                //        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                //        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                //        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                //        //warningRegisterText.text = "Username Set Failed!";
                //    }
                //    else
                //    {
                //        //Username is now set
                //        //Now return to login screen
                //        //UIManager.instance.LoginScreen();
                //        //warningRegisterText.text = "";
                //    }
                //}
            }
        //}
    }
}