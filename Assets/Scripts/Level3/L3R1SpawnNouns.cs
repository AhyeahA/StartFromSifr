using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using System;
using System.Linq;
using UnityEngine.UI;
using ArabicSupport;

public class L3R1SpawnNouns : MonoBehaviour
{
    FirebaseFirestore db;
    Dictionary<string, object> user;
    DocumentReference stuRef;
    public GameObject[] spawnPoints;
    static private string missingAns;
    static private List<String> group;
    static private int spawnIndex = 0;
    static public string nameobj;
    private static string quessent;
    private static string correctSentence;
    private static int misIndex;
    private static bool hascollided;
    public Text sent;

    public static bool setAnswer = false;
    // Start is called before the first frame update
    void Start()
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
                db = FirebaseFirestore.DefaultInstance;
                Debug.Log(db.Collection("Level 3 Room 1").Document("1"));
                SpawnQuestion();
             

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    async void SpawnQuestion()
    {
        stuRef = db.Collection("Level 3 Room 1").Document("1");
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

                    if (pair.Key.Equals("Index")){
                        misIndex = Int16.Parse(pair.Value.ToString());
                    }
                    if(pair.Key.Equals("User Sentence"))
                    {
                        quessent = pair.Value.ToString();
                        sent.text = ArabicFixer.Fix(quessent, false, false);
                    }
                    if (pair.Key.Equals("Sentence"))
                    {
                        correctSentence =  ArabicFixer.Fix(pair.Value.ToString(), false, false);
                    }
                    if (pair.Key.Equals("Object Name"))
                    {
                        missingAns = pair.Value.ToString();
                    }
                    if (pair.Key.Equals("Options"))
                    {
                        Debug.Log("Options" + pair.Value);
                        group = new List<string>(pair.Value.ToString().Split('.'));
                    }

                }
                SpawnObject();
            }
            else
            {
                Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
            }
        });

       
    }


    public void SpawnObject() //SpawnObject(string _object)
    {
        GameObject[] tempArr = Resources.LoadAll<GameObject>("Categories");
        int temp = 0;
        while (temp < tempArr.Length)
        {

            if (tempArr[temp].name.Equals(missingAns))
            {
                Debug.Log("GameObject: " + tempArr[temp]);
                tempArr[temp].transform.localScale = new Vector3(3, 3, 3);
                Debug.Log("spawning " + tempArr[temp] + " at point " + spawnPoints[spawnIndex].name);
                Instantiate(tempArr[temp], spawnPoints[spawnIndex++].transform);
            }
            temp++;
        }
        //spawn other game objects based on options
        temp = 0;


        foreach (string option in group)
        {
            int randNumIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
            Debug.Log("options" + option);
            temp = 0;
            while (temp < tempArr.Length)
            {
                if (tempArr[temp].name.Equals(option))
                {
                    Debug.Log("GameObject Random: " + tempArr[temp]);
                    tempArr[temp].transform.localScale = new Vector3(3, 3, 3);
                    Debug.Log("spawning random " + tempArr[temp] + " at point " + spawnPoints[spawnIndex].name);
                    Instantiate(tempArr[temp], spawnPoints[spawnIndex++].transform);

                }
                temp++;
            }

        }

    }

    public static void getColliderObject(string name)
    {
        nameobj = name;
        hascollided = true;
    }




    // Update is called once per frame
    void Update()
    {
        if (hascollided)
        {
            Debug.Log(nameobj.Replace("(Clone)", ""));
            Debug.Log(quessent.Replace("[]", nameobj.Replace("(Clone)", "")));
            sent.text = ArabicFixer.Fix(quessent.Replace("[]", "ال" + nameobj.Replace("(Clone)", "")), false, false);
            hascollided = false;
        }

        if (setAnswer)
        {
            setAnswer = false;
            if (sent.text.Equals(correctSentence))
            {
                Debug.Log("You got the correct answer");
                sent.color = Color.green;
            }
            else
            {
                Debug.Log("You got the wrong answer");
                sent.color = Color.red;
            }
        }
    }
}
