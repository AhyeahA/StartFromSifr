using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine.UI;
using ArabicSupport;

public class L3R3Script : MonoBehaviour
{
    FirebaseFirestore db;
    DocumentReference stuRef;

    public TextMesh sentence1, sentence2;
    public Text selSentGUI;

    private int questionIndex = 1;
    private static int incorrectIndex;
    private static string correctSentence;
    private static string wrongSentence;
    private string selectedSentence;

    public static bool isCollided = false;
    public static string collidedObj="";
    
    public static bool setAnswer = false;
    // Start is called before the first frame update
    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Debug.Log("available for use");
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                //app = Firebase.FirebaseApp.DefaultInstance;
                db = FirebaseFirestore.DefaultInstance;
                getSentence();
                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollided) //check if user has collided with a frame
        {
            isCollided = false;
            if (collidedObj.Equals("Sen1Pic")) //if collided with frame 1
            {
                selSentGUI.text = sentence1.text;
            }
            else if (collidedObj.Equals("Sen2Pic")) //if collided with frame2
            {
                selSentGUI.text = sentence2.text;
            }
        }
        

        if (setAnswer) //when user clicks of "checkmark" button to finalize answer
        {
            setAnswer = false;
            selectedSentence = selSentGUI.text;
            if (sentence1.text.Equals(correctSentence)) //check answer and accordingly indicate to the user whether right or wrong
            {
                sentence1.color = Color.green;
                sentence2.color = Color.red;
            }
            else
            {
                sentence1.color = Color.red;
                sentence2.color = Color.green;
            }
                
            if (selectedSentence.Equals(correctSentence))
            {
                selSentGUI.color = Color.green;
                questionIndex++;
                StartCoroutine(DelayAction(3));
            }
            else if (selectedSentence.Equals(wrongSentence))
            {
                selSentGUI.color = Color.red;
                questionIndex++;
                StartCoroutine(DelayAction(3));
            }
            else if (selectedSentence == "") //if user hasn't selected a sentence
            {
                selSentGUI.text = "PLEASE SELECT A SENTENCE!";
            }
        }
    }

    IEnumerator DelayAction(float delayTime) //used to delay getting the next question
    {
        yield return new WaitForSeconds(delayTime);
        getSentence();
    }

    async void getSentence()
    {
        //reset text components
        if (questionIndex > 1)
        {
            sentence1.color = Color.white;
            sentence1.text = "";
            sentence2.color = Color.white;
            sentence2.text = "";
            selSentGUI.color = Color.white;
            selSentGUI.text = "";
        }
        

        stuRef = db.Collection("Level 3 Room 3").Document(questionIndex.ToString()); //get document from firebase
        stuRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Dictionary<string, object> qs = snapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in qs)
                {
                    if (pair.Key.Equals("Number"))
                    {
                        incorrectIndex = Int16.Parse(pair.Value.ToString());
                    }
                    if (pair.Key.Equals("Right Sentence")) //correct sentence
                    {
                        correctSentence = ArabicFixer.Fix(pair.Value.ToString(), false, false); //display sentence with Arabic support
                    }
                    if (pair.Key.Equals("Sentence 1")) //wrong sentence
                    {
                        wrongSentence = ArabicFixer.Fix(pair.Value.ToString(), false, false);
                    }
                }
                if (UnityEngine.Random.Range(0.0f, 1.0f) < 0.5f)
                { // randomly display correct sentence in frame 1 and wrong sentence in frame 2
                    sentence1.text = correctSentence;
                    sentence2.text = wrongSentence;
                }
                else
                { //randomly display wrong sentence in frame 1 and correct sentence in frame 2
                    sentence1.text = wrongSentence; 
                    sentence2.text = correctSentence;
                }
            }
            else
            {
                Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
            }
        });
    }

}
