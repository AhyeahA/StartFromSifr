using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayLetterForm : MonoBehaviour
{
    public GameObject[] picArr;
    private int index = 0;
    List<Texture2D> lPrefabPool = new List<Texture2D>();
    static public string chosenAnswer =  "";
    static public bool answerSet = false;
    private string word;
    public static int NumOfQues;
    public static int NumOfAns;

    // Start is called before the first frame update
    void Start()
    {
        //clear canvas
        vfxController.CurrentSceneName = SceneManager.GetActiveScene().name;
        for (int i = 0; i < 3; i++)
        {

            Debug.Log("Picture Clearing" + picArr[i]);
            Material m = Resources.Load("New Material") as Material; //TO DO: replace with letter random
            picArr[i].GetComponent<Renderer>().material = m; //clear canvas

        }

        QuestionCreation();
        //add a shared preferences for the themes 

        //texture = Resources.Load("LetterForms/Laam2") as Texture2D; //TO DO: replace with letter random
        //material = new Material(Shader.Find("Standard"));
        //material.mainTexture = texture;
        //pic2.GetComponent<Renderer>().material = material;

        //texture = Resources.Load("LetterForms/Laam3") as Texture2D; //TO DO: replace with letter random
        //material = new Material(Shader.Find("Standard"));
        //material.mainTexture = texture;
        //pic3.GetComponent<Renderer>().material = material;


        //KEPT CAUSE DON'T KNOW WHEN WE NEED IT? 
        //Material quadMaterial = (Material)Resources.Load("Assets/LetterForms/Materials/'Ayn2.mat");
        //quad.GetComponent<Material>() = quadMaterial;
        //Texture2D myTexture = Resources.Load("'Ayn2.jpg") as Texture2D;
        //quad.GetComponent<Material>().mainTexture = myTexture;
        //rend.material.mainTexture = Resources.Load("Assets/LetterForms/Materials/'Ayn2.mat") as Texture;
    }

    private void QuestionCreation()
    {
        Debug.Log("question Creation Called");

            //te[] aPrefabs = Resources.LoadAll<Texture2D>("Level1Room3Words");

        Texture2D[] aPrefabs = Resources.LoadAll<Texture2D>("Level1Room3Words");
        int aPrefabsLength = aPrefabs.Length;
        Debug.Log("Question Creation Level 1" + aPrefabs.Length);

        if (aPrefabsLength != 0)
        {
            foreach (Texture2D prefab in aPrefabs)
            {
                
                lPrefabPool.Add(prefab);
            }
            int randomNumber = Random.Range(0, aPrefabsLength);
            Texture2D texture = Resources.Load("Level1Room3Words/" + lPrefabPool[randomNumber].name) as Texture2D; //TO DO: replace with letter random
            Debug.Log("Question creation" + lPrefabPool[randomNumber].name);
            Material material = new Material(Shader.Find("Standard"));
            material.mainTexture = texture;
            this.gameObject.GetComponent<Renderer>().material = material;

            word = lPrefabPool[randomNumber].name;
            //word = word.Substring(0, (word.Length - 1));
            string[] letterList = AssetDatabase.FindAssets(word.Substring(0, (word.Length - 1)));
            foreach (string guid in letterList)
            {
                //Debug.Log(AssetDatabase.GUIDToAssetPath(guid));
                if (AssetDatabase.GUIDToAssetPath(guid).Contains("LetterForms"))
                {
                    //replacing the frames with the medial letters
                    Debug.Log("PicArr" + picArr[index].name);
                    Debug.Log(AssetDatabase.GUIDToAssetPath(guid).Split('.')[0]);
                    Debug.Log(AssetDatabase.GUIDToAssetPath(guid).Split('/')[3].Split('.')[0]);
                    texture = Resources.Load("LetterForms/" + AssetDatabase.GUIDToAssetPath(guid).Split('/')[3].Split('.')[0]) as Texture2D; //TO DO: replace with letter random
                    material = new Material(Shader.Find("Standard"));
                    material.mainTexture = texture;
                    picArr[index++].GetComponent<Renderer>().material = material;
                }


            }
        }
    }

    static public void checkAnswer()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(NumOfQues == 2)
        {
            //set it to num of stars
            Debug.Log("before calculation num of ques: " + NumOfQues.ToString());
            Debug.Log("before calculation NumOfAns: " + NumOfAns.ToString());
            decimal x = (decimal)NumOfQues;
            decimal y = (decimal)NumOfAns;
            Debug.Log(y);
            decimal calculation = (x / y);
            Debug.Log("calculation  " + calculation);
            vfxController.CalculatedScore = Mathf.RoundToInt((float)(calculation * 3));
            Debug.Log("number of stars  " + vfxController.CalculatedScore.ToString());

            NumOfQues = 0;
            NumOfAns = 0;

            //call star effect scene
            SceneManager.LoadScene("LevelResults");
        }
        if (answerSet)
        {
            answerSet = false;
            if (chosenAnswer.Equals(word))
            {
                Debug.Log("RIGHT ANSWER!");
                NumOfQues++;
                NumOfAns++;
                //clear the materials
                for (int i = 0; i < 3; i++)
                {
                    if (!picArr[i].GetComponent<Renderer>().material.name.Contains("New Material"))
                    {
                        if (picArr[i].GetComponent<Renderer>().material.mainTexture.name.Equals(word))
                        {
                            Debug.Log("Right answer picture:" + picArr[i]);
                            Material m = Resources.Load("RightAnsGreen") as Material; //TO DO: replace with letter random
                            picArr[i].GetComponent<Renderer>().material = m; //clear canvas
                        }
                        else
                        {
                            Material m = Resources.Load("New Material") as Material; //TO DO: replace with letter random
                            picArr[i].GetComponent<Renderer>().material = m; //clear canvas
                        }
                    }
                }
                //if answered wrong decrease score move on to next question?
                //add score here
                
            }
            else
            {
                NumOfAns++;
                Debug.Log("WRONG ANSWER!");
                for (int i = 0; i < 3; i++)
                {
                    if (!picArr[i].GetComponent<Renderer>().material.name.Contains("New Material"))
                    {
                        if (picArr[i].GetComponent<Renderer>().material.mainTexture.name.Equals(chosenAnswer))
                        {
                            Debug.Log("Wrong answer picture:" + picArr[i]);
                            Material m = Resources.Load("WrongAnsRed") as Material; //TO DO: replace with letter random
                            picArr[i].GetComponent<Renderer>().material = m; //clear canvas
                        }
                        else
                        {
                            Material m = Resources.Load("New Material") as Material; //TO DO: replace with letter random
                            picArr[i].GetComponent<Renderer>().material = m; //clear canvas
                        }
                    }
                }
                

            }
            index = 0;
            chosenAnswer = "";
            StartCoroutine(DelayAction(3));
        }
        //Debug.Log("word" + word);
        ////Debug.Log("كلب".Substring(0,1));
        ////Debug.Log("Chosen Answer" + chosenAnswer);
        //Debug.Log("Chosen answer:" + chosenAnswer);
        //if (chosenAnswer.Equals(word))
        //{
        //    Debug.Log("RIGHT ANSWER!");
        //    //clear the materials
        //    for(int i = 0; i<3; i++)
        //    {
        //        Material m = Resources.Load("New Material") as Material; //TO DO: replace with letter random

        //        picArr[i].GetComponent<Renderer>().material = m; //clear canvas
        //    }
        //    index = 0;
        //    QuestionCreation();
        //    //if answered wrong decrease score move on to next question?
        //    //add score here
        //}
        //Debug.Log("WRONG ANSWER!");
    }
    IEnumerator DelayAction(float delayTime) //used to delay getting the next question
    {
        yield return new WaitForSeconds(delayTime);
        QuestionCreation();
    }
}
