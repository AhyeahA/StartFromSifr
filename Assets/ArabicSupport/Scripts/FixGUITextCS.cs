using UnityEngine;
using System.Collections;
using ArabicSupport;
using UnityEngine.UI;

public class FixGUITextCS : MonoBehaviour {

	public Text textBox;
	public string arabicText;
	public bool tashkeel = true;
	public bool hinduNumbers = true;
	
	// Use this for initialization
	void Start () {
		textBox.text = ArabicFixer.Fix(arabicText, tashkeel, hinduNumbers);
		Debug.Log(ArabicFixer.Fix(arabicText, tashkeel, hinduNumbers));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
