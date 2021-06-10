using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ArabicSupport;
using System;
using Random = UnityEngine.Random;
using System.Linq;

public class SpawnObject : MonoBehaviour //spawn object on table and letter blocks for that object
{
    public GameObject objSpawnPoint;
    public Text onScreenText;
    static private GameObject staticObjSpawnPoint;
    static private GameObject staticBlockSpawnPoint;
    static private GameObject toSpawnObject;
    static private GameObject[] QuestionObjects;
    static private GameObject[] allLetterBlocks; //contains all the letter blocks
    static private List<GameObject> spawnedLetters = new List<GameObject>();
    private GameObject objtmp, objtmp2;
    static public int checkCorrectIndex = 0;
    static private int questionIndex = 0;
    static string ChosenWords = "";
    static bool rightAns = false;
    static bool fullAns = false;

    // Start is called before the first frame update
    void Start()
    {
        onScreenText.text = "";
        staticObjSpawnPoint = objSpawnPoint;
        staticBlockSpawnPoint = this.gameObject;
        //get the arabic characters of all letter blocks
        allLetterBlocks = Resources.LoadAll<GameObject>("LetterBlocksPrefabs");
        QuestionObjects = new GameObject[5];

        //Spawn object on table;
        GameObject[] tempArr = Resources.LoadAll<GameObject>("Categories/Vehicles");
        int temp = 0;
        while (temp < QuestionObjects.Length)
        {
            int randNumIndex = Random.Range(0, tempArr.Length);
            if (!QuestionObjects.Contains(tempArr[randNumIndex]))
            {
                QuestionObjects[temp] = tempArr[randNumIndex];
                temp++;
            }
        }
        NextQuestion();
    }

    public void generateLetterBlocks(string objectName)
    {
        Debug.Log("ObjectName:" + objectName);
        //dissect object name
        char[] objectLetters = objectName.ToCharArray();
        int index = -1;

        List<int> indexNumbers = new List<int>();
        //spawnLetterBlocks
        for (int i = 0; i < objectLetters.Length; i++)
        {
            for (int j = 0; j < allLetterBlocks.Length; j++)
            {
                char c;//different for alef, nuun and o
                if (allLetterBlocks[j].name.Equals("AlefPF"))
                    c = 'ا'; //audio file name isn't ا
                else if (allLetterBlocks[j].name.Equals("ZbTaa"))
                    c = 'ة';
                else
                {
                    string audioFileName = allLetterBlocks[j].GetComponent<AudioSource>().clip.name; //get audio name of block to associate arabic letter with block
                    if (allLetterBlocks[j].name.Equals("NuunPF"))
                        c = audioFileName.Substring(0).ToCharArray()[0]; //audio file name doesnt have number to split
                    else
                        c = audioFileName.Split('.')[1].Substring(1).ToCharArray()[0]; //split to remove number then take first letter
                }

                if (c.Equals(objectLetters[i]))
                {
                    index = j;
                    break;
                }
            }
            indexNumbers.Add(index);
        }

        for (int i = 0; i < indexNumbers.Count; i++)
        {
            objtmp = Instantiate(allLetterBlocks[indexNumbers[i]], staticBlockSpawnPoint.transform);
            spawnedLetters.Add(objtmp);
        }
        Debug.Log("SpawnedLetter size : " + spawnedLetters.Count);
    }

    // Update is called once per frame
    void Update()
    { 
        if(ChosenWords != "")
        {
            
            if (rightAns & fullAns)
            {
                fullAns = false;
                rightAns = false;
                NextQuestion();
                
                
            }
            else if (rightAns)
            {
                onScreenText.color = Color.green;
                onScreenText.text = ChosenWords;
                rightAns = false;

            }
            else
            {
                onScreenText.color = Color.red;
                onScreenText.text = ChosenWords;
                rightAns = false;
                NextQuestion();

            }
            ChosenWords = "";  
            
            
        }
    }




    static public bool checkCorrectAnswer(GameObject selectedLetter) //check if letter chosen is the right letter in order
    {
        Debug.Log("checkCorrect spawnedletters count:" + spawnedLetters.Count);
        Debug.Log("checkCorrect checkCorrectIndex:" + checkCorrectIndex);
        if (checkCorrectIndex + 1 == spawnedLetters.Count)//if they got all correct 
        {
            fullAns = true;
            ChosenWords = " ";
        }
        else
        {
            //incorrect
            ChosenWords = ArabicFixer.Fix("غير صحيح", false, false);
            rightAns = false;
            
        }

        if (checkCorrectIndex < spawnedLetters.Count)
        {
            if (spawnedLetters[checkCorrectIndex].name.Substring(0, selectedLetter.name.Length - 7) == selectedLetter.name.Substring(0, selectedLetter.name.Length - 7))
            {
                ChosenWords = ArabicFixer.Fix("صحيح", false, false);
                rightAns = true;
                checkCorrectIndex++;
                return true;
            }
        }
        return false;
    }



    public void NextQuestion()
    {
        checkCorrectIndex = 0; //reset correct answer
        //onScreenText.color = Color.white;
        //onScreenText.text = "";
        if (questionIndex > 0) //not first question
        {
            Destroy(toSpawnObject); //destroy object on table
            foreach (GameObject obj in spawnedLetters)
            {
                Destroy(obj);
            }
            spawnedLetters.Clear();
        }
        objtmp2 = Instantiate(QuestionObjects[questionIndex], staticObjSpawnPoint.transform);
        toSpawnObject = objtmp2;
        questionIndex = (questionIndex + 1) % QuestionObjects.Length;
        generateLetterBlocks(toSpawnObject.name.Substring(0, toSpawnObject.name.Length - 7));
    }
}