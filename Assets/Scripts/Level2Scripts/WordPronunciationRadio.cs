using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordPronunciationRadio : MonoBehaviour
{
    public AudioClip[] audioClips;
    static private AudioSource[] audioSources = new AudioSource[3]; //change to 5
    static private GameObject[] allLetterBlocks;
    public GameObject[] letterSpawnPoints;
    private GameObject objtmp;
    static private List<GameObject> spawnedLetters = new List<GameObject>();
    static private int questionIndex = 0;
    static public int checkCorrectIndex = 0;
    static public bool checkCorrect = false;
    static public bool wrongAnsbool = false;

    static GameObject RadioStaticObj;
    static AudioClip[] audioStaticClips = new AudioClip[3];
    static Transform[] staticSpawnPoints;

    void Start()
    {
        allLetterBlocks = Resources.LoadAll<GameObject>("LetterBlocksPrefabs");
        RadioStaticObj = this.gameObject;
        for(int i = 0; i< audioClips.Length;i++)
        {
            audioStaticClips[i] = audioClips[i];
        }
        staticSpawnPoints = new Transform[3];
        for (int i = 0; i < letterSpawnPoints.Length; i++)
        {
            staticSpawnPoints[i] = letterSpawnPoints[i].transform;
        }
        L2R2NextQuestion();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("cOLLIDING WITH RADIO!");
            audioSources[questionIndex-1] = RadioStaticObj.AddComponent<AudioSource>();
            audioSources[questionIndex-1].loop = false;
            audioSources[questionIndex-1].playOnAwake = true;
            audioSources[questionIndex-1].volume = 0.2f;
            audioSources[questionIndex-1].clip = audioStaticClips[questionIndex-1];
            audioSources[questionIndex-1].Play();
        }
    }

    public void L2R2NextQuestion()
    {
        checkCorrectIndex = 0; //reset correct answer
        
        if (questionIndex > 0) //not first question
        {
            foreach (GameObject obj in spawnedLetters)
            {
                Destroy(obj);
            }
            spawnedLetters.Clear();            
        }
        if (questionIndex < audioSources.Length)
        {
            audioSources[questionIndex] = RadioStaticObj.AddComponent<AudioSource>();
            audioSources[questionIndex].loop = false;
            audioSources[questionIndex].playOnAwake = true;
            audioSources[questionIndex].volume = 0.2f;
            audioSources[questionIndex].clip = audioStaticClips[questionIndex];
            Debug.Log("Audio clip Name:" + audioStaticClips[questionIndex].name);
            //audioSources[questionIndex].Play();
            Debug.Log(audioSources[questionIndex].clip.name);
            generateLetterBlocks(audioSources[questionIndex].clip.name);
            questionIndex++;

        }
        
    }

    public void generateLetterBlocks(string audioFileName)
    {
        char[] audioLetters = audioFileName.Split('.')[1].Substring(1).ToCharArray();
        int index = -1;
        List<int> indexNumbers = new List<int>();
        //spawnLetterBlocks
        Debug.Log("audio Letters [] : " + audioLetters);
        for (int i = 0; i < audioLetters.Length; i++)
        {
            Debug.Log("audio Letters:" + audioLetters[i]);
            if (audioLetters[i].Equals('أ'))
                audioLetters[i] = 'ا';

            for (int j = 0; j < allLetterBlocks.Length; j++)
            {
                char c;//different for alef, nuun and o
                if (allLetterBlocks[j].name.Equals("AlefPF"))
                    c = 'ا'; //audio file name isn't ا
                else if (allLetterBlocks[j].name.Equals("ZbTaa"))
                    c = 'ة';
                else
                {
                    string audioFN = allLetterBlocks[j].GetComponent<AudioSource>().clip.name; //get audio name of block to associate arabic letter with block
                    c = audioFN.Split('.')[1].Substring(1).ToCharArray()[0]; //split to remove number then take first letter
                }

                if (c.Equals(audioLetters[i]))
                {
                    index = j;
                    break;
                }
            }
            indexNumbers.Add(index);
        }

        for (int i = 0; i < indexNumbers.Count; i++)
        {
            int x = Random.Range(0, 3);
            if(indexNumbers[i] != -1)
            {
                objtmp = Instantiate(allLetterBlocks[indexNumbers[i]], staticSpawnPoints[x]);
                Debug.Log("Been spawned" + objtmp.name);
                spawnedLetters.Add(objtmp);
            }
        }
        Debug.Log("SpawnedLetter size : " + spawnedLetters.Count);
    }

    static public bool L2R2checkCorrectAnswer(GameObject selectedLetter) //check if letter chosen is the right letter in order
    {
        Debug.Log("checkCorrect spawnedletters count:" + spawnedLetters.Count);
        Debug.Log("checkCorrect checkCorrectIndex:" + checkCorrectIndex);
        if (checkCorrectIndex + 1 == spawnedLetters.Count)//if they got all correct 
        {
            checkCorrect = true;
        }

        if (checkCorrectIndex < spawnedLetters.Count)
        {
            if (spawnedLetters[checkCorrectIndex].name.Substring(0, selectedLetter.name.Length - 7) == selectedLetter.name.Substring(0, selectedLetter.name.Length - 7))
            {
                checkCorrectIndex++;
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (checkCorrect)
        {
            checkCorrect = false;
            L2R2NextQuestion();
        }
        else
        {
            if (wrongAnsbool)
            {
                wrongAnsbool = false;
                L2R2NextQuestion();
            }
        }
    }
}
