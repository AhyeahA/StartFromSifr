using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DisplayLetterForm : MonoBehaviour
{
    public GameObject[] picArr;
    int index = 0;
    List<Texture2D> lPrefabPool = new List<Texture2D>();
    static public string chosenAnswer =  "";
    private string word;
    // Start is called before the first frame update
    void Start()
    {
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
            word = word.Substring(0, (word.Length - 1));
            string[] letterList = AssetDatabase.FindAssets(word);
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

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("word" + word);
        //Debug.Log("كلب".Substring(0,1));
        //Debug.Log("Chosen Answer" + chosenAnswer);
        if (chosenAnswer.Contains(word))
        {
            //clear the materials
            for(int i = 0; i<3; i++)
            {
                Material m = Resources.Load("New Material") as Material; //TO DO: replace with letter random
               
                picArr[i].GetComponent<Renderer>().material = m; //clear canvas
            }
            index = 0;
            QuestionCreation();
            //if answered wrong decrease score move on to next question?
            //add score here
        }

    }
}
