using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using System;
using TMPro;
using ArabicSupport;
using System.Linq;



public class L3R3RainbowRoom : MonoBehaviour
{
    FirebaseFirestore db;
    Dictionary<string, object> user;
    DocumentReference stuRef;
    [SerializeField]
    TextMesh[] listOfwords;
    static private List<String> group;
    static public string answer = " ";
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
                SpawnQuestion();
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
        
    }

    async void SpawnQuestion()
    {
        stuRef = db.Collection("Level 3 Room 4").Document("1");
        stuRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));
                Dictionary<string, object> qs = snapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in qs)
                {
                    Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));

                    if (pair.Key.Equals("Sentence"))
                    {
                        Debug.Log("Options" + pair.Value);
                        string sent = ArabicFixer.Fix(pair.Value.ToString(), false, false);
                        answer = sent;
                        group = new List<string>(sent.ToString().Split(' '));

                        for (int i = 0; i < group.Count; i++)
                        {
                            Debug.Log(group[i]);
                            listOfwords[i].text = group[i];
                        }

                    }

                }
                setQuadsText();
            }
            else
            {
                Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
            }
        });


    }

    public void setQuadsText()
    {
        //listOfwords[0].text = "???";
        Debug.Log("function called");
    }
}
