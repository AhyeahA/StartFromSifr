using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

public class CloudFirebase : MonoBehaviour
{
    FirebaseFirestore db;
    Dictionary<string, object> user;
    DocumentReference stuRef;


    private bool istrue = true;
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
                Debug.Log(db.Collection("Users").Document("hello"));
                
                savedata();
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
        
      
        //db = FirebaseFirestore.DefaultInstance;
        //if (istrue)
        //{
        //    savedata();
        //    istrue = false;
        //}
    }

    public async void savedata()
    {

        stuRef = db.Collection("Users").Document("hello");
        Dictionary<string, object> city = new Dictionary<string, object>
                        {
                                { "Name", "Los Angeles" },
                                { "State", "CA" },
                                { "Country", "USA" }
                        };
        await stuRef.SetAsync(city).ContinueWithOnMainThread(task =>
        {
            Debug.Log("Added data to the LA document in the cities collection.");
        });


        db.Collection("Users").Document("7DEer1pzCCmf8FUv7MfB").GetSnapshotAsync().ContinueWith(task =>
          {
              if (task.IsCompleted)
              {

                  if (task.IsFaulted)
                  {
                      Debug.Log(task.Exception.Message);
                      foreach (var e in task.Exception.Flatten().InnerExceptions)
                      {
                          Debug.LogWarning($"Received Exception: {e.Message}");
                      }
                      //return;
                  }
                  Debug.Log("succesfully added to database");
                  Debug.Log(task);

                  DocumentSnapshot snapshot = task.Result;
                  if (snapshot.Exists)
                  {
                      Debug.Log("snapshot exists");
                      user = snapshot.ToDictionary();
                      foreach (KeyValuePair<string, object> pair in user)
                      {
                          Debug.Log(("{0}:{1}", pair.Key, pair.Value));
                      }
                  }
                  else
                  {
                      Debug.Log("snapshot does not exist");
                  }

              }
              else
              {
                  Debug.Log("failed db");
              }
          });
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
