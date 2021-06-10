using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Firestore;
using Firebase.Extensions;

public class vfxController : MonoBehaviour {

	public GameObject[] starFx01Prefabs;
	public GameObject[] starFx02Prefabs;
	public GameObject[] starFx03Prefabs;
	public GameObject[] starFx04Prefabs;
	public GameObject[] starFx05Prefabs;
	public GameObject[] DesStarFxObjs;
	public GameObject[] bgFxPrefabs;
	public int	currentStarImage;
	public int	currentStarFx;
	public int	currentLevel;
	public int	currentBgFx;
	static public int CalculatedScore = 0;
	public static string CurrentSceneName = "";
	FirebaseFirestore db;
	Dictionary<string, object> user;
	DocumentReference stuRef;



	void Start () {
		currentStarImage = 0;
		currentStarFx = 0;
		currentLevel = 3;
		currentLevel = CalculatedScore;
		Debug.Log("Epic Effects Level: current/Number of Stars: " + currentLevel);
		Debug.Log("Epic Effects Level: Calculated/Number of Stars: " + CalculatedScore);
		currentBgFx = 1;
		PlayStarFX();
	}

	
	void StartFirebase()
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
				AddScores();
				// Set a flag here to indicate whether Firebase is ready to use by your app.
			}
			else
			{
				UnityEngine.Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
			}
		});
	}

	void AddScores()
    {
		DocumentReference docRef = db.Collection("Users").Document(LoginManager.uid);
		Dictionary<string, object> update = new Dictionary<string, object>
		{
		{ CurrentSceneName + " Score" , CalculatedScore }
		};
		docRef.SetAsync(update, SetOptions.MergeAll);
	}

	//public void ChangedStarImage (int i) {
	//	currentStarImage = i;
	//	PlayStarFX ();
	//}

	//public void ChangedStarFX (int i) {
	//	currentStarFx = i;
	//	PlayStarFX ();
	//}

	//public void ChangedLevel (int i) {
	//	currentLevel = i;
	//	PlayStarFX ();
	//}

	//public void ChangedBgFx (int i) {
	//	currentBgFx = i;
	//	PlayStarFX ();
	//}

	public void goToMainFloor()
    {
		SceneManager.LoadScene("MainFloor");
    }

	public void PlayStarFX () {
		DesStarFxObjs = GameObject.FindGameObjectsWithTag("Effects");

		foreach(GameObject DesStarFxObj in DesStarFxObjs)
			Destroy(DesStarFxObj.gameObject);

		if (currentBgFx != 0) {
			Instantiate (bgFxPrefabs [currentBgFx]);
		}
			
		switch (currentStarImage) {
		case 0: 
			Instantiate (starFx01Prefabs [currentStarFx]);
			starFxController.myStarFxController.ea = currentLevel;
		    StartFirebase();
			break;
		case 1: 
			Instantiate (starFx02Prefabs [currentStarFx]);
			starFxController.myStarFxController.ea = currentLevel;
				StartFirebase();
			break;
		case 2: 
			Instantiate (starFx03Prefabs [currentStarFx]);
			starFxController.myStarFxController.ea = currentLevel;
				StartFirebase();
				break;
		case 3: 
			Instantiate (starFx04Prefabs [currentStarFx]);
			starFxController.myStarFxController.ea = currentLevel;
				StartFirebase();
				break;
		case 4: 
			Instantiate (starFx05Prefabs [currentStarFx]);
			starFxController.myStarFxController.ea = currentLevel;
				StartFirebase();
				break;
		}
	}
}