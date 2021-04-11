using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnBlocksInArea : MonoBehaviour
{
    [SerializeField] GameObject[] LettersPrefabs;
    static public GameObject obj;
    public GameObject keyObj;
    public GameObject shinyEffect;
    public Vector3 center;
    public Vector3 size;
    private bool keyCreated = true;
    static public bool blockCollision = false;
    public float interval;
    static public bool isPressed = false;
    private int index = 0;
    static public int keyIndex = 0;
    private List<int> randIndex;
    static public AudioSource objAudioSource;
    private GameObject[] Room2Letters;
    public float keyCenterpos;


    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
            InvokeRepeating("SpawnLetterBlocks", 0, 15f);
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Room2Letters = new GameObject[3];
            SpawnRand3Blocks();
        }

    }

    private void Update()// Update is called once per frame
    {
        if (keyIndex == 5 && keyCreated)
        {
            Instantiate(keyObj, new Vector3(-3.82f, keyCenterpos, -0.11f), Quaternion.identity);
            Instantiate(shinyEffect, new Vector3(-3.82f, keyCenterpos, -0.11f), Quaternion.identity);
            keyCreated = false;
            keyIndex = 0;

        }

        if (blockCollision) //For Level 1 Room 2
        {
            //Debug.Log("KeyIndex" + keyIndex);
            Debug.Log("BlockCollision is true");
            for (int i = 0; i < 3; i++)
            {
                Destroy(Room2Letters[i]);
            }
            blockCollision = false;
            SpawnRand3Blocks();
        }

    }

    public void SpawnLetterBlocks()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.x / 2, size.x / 2));
        if (index < 28)
        {
            Destroy(Instantiate(LettersPrefabs[Random.Range(0, 27)], pos, Quaternion.identity), 15f);
            isPressed = false;
        }
        else
        {
            index = 0; //reset the index when it reaches limit
        }

    }

    public void SpawnRand3Blocks()
    {
        randIndex = new List<int>();
        int temp;
        while (randIndex.Count < 3)
        {
            temp = Random.Range(0, 27);

            if (!randIndex.Contains(temp))
                randIndex.Add(temp);

        }

        for (int i = 0; i < 3; i++)
        {
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.x / 2, size.x / 2));
            Room2Letters[i] = Instantiate(LettersPrefabs[randIndex[i]], pos, Quaternion.identity);
        }
        temp = Random.Range(0, 2);
        Debug.Log("Temp var" + temp);
        obj = LettersPrefabs[randIndex[temp]];
        Debug.Log("Func SpawnRand3Block() object name" + obj.name);
        AudioClip objAudio = obj.GetComponent<AudioSource>().clip;
        GetComponent<AudioSource>().clip = objAudio;
        objAudioSource = GetComponent<AudioSource>();
    }

    static public void playRadioSound()
    {

        Debug.Log("Func playRadioSound() audiosource name" + objAudioSource.clip.name);
        if (!objAudioSource.isPlaying)
            objAudioSource.Play();
    }


    

    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = new Color(1, 0, 0, 5f);
        Gizmos.DrawCube(center, size);
    }
}
