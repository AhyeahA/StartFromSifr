using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DisplayWordHarakat : MonoBehaviour
{
    public GameObject[] picArr;
    private GameObject[] mainObjects;
    int index = 0;
    List<Texture2D> lPrefabPool = new List<Texture2D>();
    public GameObject completionEffect;
    private GameObject ObjectToBeDestoryed;
    static public string chosenAnswer = "";
    private int randomNumber;
    private string answer;
    private string word;
    List<int> randomNumbers = new List<int>(); //save list of random numbers generated
    // Start is called before the first frame update
    void Start()
    {
        QuestionCreation();
    }


    private void QuestionCreation()
    {
        //get a random object from one of the themes
        mainObjects = Resources.LoadAll<GameObject>("Categories/Animals"); //gonna be later changed to 'Categories + Theme'
        int mainObjectsLength = mainObjects.Length;
        Debug.Log("Question Creation Level2" + mainObjects.Length);
        //instantiate Object
        randomNumber = Random.Range(0, mainObjectsLength); //change the size and rotation of the object
        randomNumbers.Add(randomNumber); //add to list
        if(randomNumbers.Capacity > 1)
        {
            //do randomNumber = Random.Range(0, mainObjectsLength);
            //while (randomNumbers.Contains(randomNumber));
            if(randomNumbers.Contains(randomNumber))
                randomNumber = Random.Range(0, mainObjectsLength);
            //check if the number existed
        }
        mainObjects[randomNumber].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        //mainObjects[randomNumber].transform.Rotate(0.0f, 90f, 0.0f, Space.World);
        mainObjects[randomNumber].transform.position = new Vector3(-1.49f, 1.5f, -2.83f);
        ObjectToBeDestoryed = Instantiate(mainObjects[randomNumber], mainObjects[randomNumber].transform);
        Debug.Log(mainObjects[randomNumber].name);
        //place words on quads
        word = mainObjects[randomNumber].name;
        //assigning answer
        answer = mainObjects[randomNumber].name;
        //taking word without the number
        word = word.Substring(1);
        Debug.Log(word);
        string[] letterList = AssetDatabase.FindAssets(word);
        Debug.Log(letterList.Length);
        index = 0;
        foreach (string guid in letterList)
        {
            //Debug.Log(AssetDatabase.GUIDToAssetPath(guid));
            if (AssetDatabase.GUIDToAssetPath(guid).Contains("Room3TwoOptions")) //change folder name to Level2Room3Words
            {
                //replacing the frames with the medial letters
                //Debug.Log("PicArr" + picArr[index].name);
                Debug.Log(AssetDatabase.GUIDToAssetPath(guid).Split('.')[0]);
                Debug.Log(AssetDatabase.GUIDToAssetPath(guid).Split('/')[4].Split('.')[0]);
                Texture2D texture = Resources.Load("Categories/Room3TwoOptions/" + AssetDatabase.GUIDToAssetPath(guid).Split('/')[4].Split('.')[0]) as Texture2D; //TO DO: replace with letter random
                Material material = new Material(Shader.Find("Standard"));
                material.mainTexture = texture;
                Debug.Log(index);
                picArr[index++].GetComponent<Renderer>().material = material;
                
            }


        }




    }

        // Update is called once per frame
        private void Update()
    {
        if (chosenAnswer != "")
        {
            
            if (chosenAnswer.Contains(answer))
            {
                Destroy(ObjectToBeDestoryed);
                Destroy(Instantiate(completionEffect, this.gameObject.transform), 2f);
                //increase score
                chosenAnswer = "";
                QuestionCreation();
                
            }
            else
            {
                //decrease score
                Destroy(ObjectToBeDestoryed);
                Destroy(Instantiate(completionEffect, this.gameObject.transform), 2f);
                chosenAnswer = "";
                QuestionCreation();
                
            }
        }
        //when answer chosen instantiate an effect 
        //destroy the question object
        //call question creation function
        //if answered wrong decrease score move on to next question?
        
    }
}
