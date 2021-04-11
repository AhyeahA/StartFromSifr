using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Random = UnityEngine.Random;
using System.Linq;

public class SpawnObject : MonoBehaviour //spawn object on table and letter blocks for that object
{
    public Vector3 center, size;
    private Vector3 pos;
    public GameObject objSpawnPoint;
    static private GameObject staticObjSpawnPoint;
    private GameObject toSpawnObject;
    static private GameObject[] QuestionObjects;
    static private GameObject[] allLetterBlocks; //contains all the letter blocks
    static private List<GameObject> spawnedLetters;
    private GameObject objtmp;
    static public bool destroyAllBlocksL2R1=false;
    static public int checkCorrectIndex=0;
    static private int questionIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        staticObjSpawnPoint = objSpawnPoint;
        //get the arabic characters of all letter blocks
        allLetterBlocks = Resources.LoadAll<GameObject>("LetterBlocksPrefabs");
        QuestionObjects = new GameObject[5];

        //Spawn object on table;
        GameObject[] tempArr = Resources.LoadAll<GameObject>("Categories/Vehicles");
        int temp = 0;
        while(temp < QuestionObjects.Length)
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
        //dissect object name
        char[] objectLetters = objectName.ToCharArray();
        //spawnedLetters = new GameObject[objectLetters.Length];
        int index=-1;
        //spawnedLetters.Clear();
        spawnedLetters = new List<GameObject>();
        List<int> indexNumbers = new List<int>();
        //spawnLetterBlocks
        for (int i=0; i < objectLetters.Length; i++)
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
            objtmp = Instantiate(allLetterBlocks[indexNumbers[i]], this.gameObject.transform);
            spawnedLetters.Add(objtmp);
        }
            

        //foreach (GameObject toSpawnLetter in spawnedLetters) //--------PROBLEM
        //{
        //    //toSpawnLetter.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        //    pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.x / 2, size.x / 2));
        //    Debug.Log("pos" + pos);
        //    Instantiate(toSpawnLetter, pos, Quaternion.identity);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyAllBlocksL2R1 && spawnedLetters.Capacity > 2)
        {
            Debug.Log("SpawnedLetter size:" + spawnedLetters.Capacity);
            foreach (GameObject obj in spawnedLetters)
            {
                Debug.Log("SpawnedLetters before: " + obj.name);
                
            }
            //foreach (GameObject obj in spawnedLetters)
            //{
            //    Destroy(obj);
            //}
            destroyAllBlocksL2R1 = false;
        }    
    }


    static public bool checkCorrectAnswer(GameObject selectedLetter) //check if letter chosen is the right letter in order
    {
        if(checkCorrectIndex+1 == spawnedLetters.Capacity)//if they got all correct
            //NextQuestion();

        if (checkCorrectIndex < spawnedLetters.Capacity)
        {
            if (spawnedLetters[checkCorrectIndex].name == selectedLetter.name.Substring(0, selectedLetter.name.Length - 7))
            {
                checkCorrectIndex++;
                return true;
            }
        }
        return false;
    }

    public void NextQuestion()
    {
        checkCorrectIndex = 0; //reset correct answer
        Destroy(toSpawnObject); //destroy object on table
        Debug.Log("Destroyed spawnObject ------ " + toSpawnObject);
        destroyAllBlocksL2R1 = true;
        toSpawnObject = QuestionObjects[questionIndex++];
        Debug.Log("toSpawnObject after reinitializing" + toSpawnObject);
        toSpawnObject.transform.localScale = new Vector3(1.5f, 2f, 4f);
        Instantiate(toSpawnObject, staticObjSpawnPoint.transform);
        generateLetterBlocks(toSpawnObject.name);
    }

    //static public void DestroyBlock(GameObject L2R1Blocks)
    //{
        
    //    Debug.Log("In destroy function : " + L2R1Blocks.name);
    //    for(int i = 0; i < spawnedLetters.Length; i++)
    //    {
    //        if (spawnedLetters[i].name.Equals(L2R1Blocks.name))
    //        {
    //            Debug.Log(spawnedLetters[i].name + " EQUAL " + L2R1Blocks.name);
    //            spawnedLetters = spawnedLetters.Where(val => val != spawnedLetters[i]).ToArray(); //remove the about to be deleted one from array
    //            Debug.Log("spawnedLetters length: " + spawnedLetters.Length);
    //            Destroy(spawnedLetters[i]);
    //        }
    //    }
    //    //foreach (GameObject obj in spawnedLetters)
    //    //{
    //    //    if (obj.name.Equals(L2R1Blocks.name))
    //    //    {
    //    //        Debug.Log("EQUAL");
    //    //        spawnedLetters = spawnedLetters.Where(val => val != obj).ToArray(); 
    //    //        Destroy(obj);
    //    //    }
    //    //}
    //    //spawnedLetters = spawnedLetters.Where(val => val != L2R1Blocks).ToArray();
    //    //foreach (GameObject obj in spawnedLetters)
    //    //{
    //    //    Debug.Log("SpawnedLetters before: " + obj);
    //    //}
    //    //Destroy(L2R1Blocks);
    //}

    

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(center, size);
    }
}
