using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundLetterTrigger : MonoBehaviour
{
    
    static public AudioSource audioSource;
    static public Animator anim;
    static public bool btnClicked;
    static public Vector3 pos;

    public GameObject effectobj;
    public GameObject goSpawn;
    static public bool spwneffect = false;
    static public bool checkMatch = false;
    private bool hasCollidedL2R1 = false;
    private bool hasCollidedL2R2 = false;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        
    }

    private void Update()
    {
        //check if player if in proximity or collides with cube
        
        if (spwneffect)
        {
            spwneffect = false;
            Debug.Log("Update true effectObject" + effectobj);
            pos = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 0.5f, this.gameObject.transform.position.z);
            Destroy(Instantiate(effectobj, pos, Quaternion.identity), 2f);
        }
        

    }


    private void OnCollisionEnter(Collision other)
    {
        //add effect when colliding instead of deleting object
        if (SceneManager.GetActiveScene().buildIndex == 4) //level 1 room 1
        {
            Debug.Log("Colliding");
            if (other.collider.gameObject.tag == "Player" && !audioSource.isPlaying)
            {
                PlayButtonSound.characterInProximity(true);
            }
            else
            {
                PlayButtonSound.characterInProximity(false);
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5) //level 1 room 2
        {
            Debug.Log("Colliding2");
            if (other.collider.gameObject.tag == "Player" && RadioTrigger.radioTriggered)
            {
                Debug.Log("Has been collided with player");
                CheckCorrectMatch(this.gameObject);
                Debug.Log(this.gameObject.name);
                SpawnBlocksInArea.blockCollision = true;
                
                RadioTrigger.radioTriggered = false;
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level 2 Room 1")) //level 2 room 1 - NEED TO CHANGE BASED ON SCENE MANAGER
        {
            if (other.collider.gameObject.tag == "Player")
            {
                Debug.Log("Level 2 Room 1 SoundLetterTrigger: " + GetComponent<AudioSource>().clip.name.Split('.')[1].Substring(1).ToCharArray()[0]);
                if (!hasCollidedL2R1)
                {
                    //what does this part do?
                    bool value = SpawnObject.checkCorrectAnswer(this.gameObject);
                    if (value)
                    {
                        Destroy(Instantiate(effectobj, this.gameObject.transform.position, Quaternion.identity),5f);
                        hasCollidedL2R1 = true;
                        Debug.Log("replace with effect"); //has been changed
                    }

                    else
                    {
                        Debug.Log("incorrect");
                    }
                }                    
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level 2 Room 2")) //level 2 room 2
        {
            if (other.collider.gameObject.tag == "Player")
            {
                Debug.Log("block audio source first letter:" + audioSource.clip.name.Split('.')[1].Substring(1).ToCharArray()[0]);
                if (!hasCollidedL2R2)
                {
                    bool value = WordPronunciationRadio.L2R2checkCorrectAnswer(this.gameObject);
                    if (value)
                    {
                        Destroy(Instantiate(effectobj, this.gameObject.transform.position, Quaternion.identity), 5f);
                        hasCollidedL2R2 = true;
                        Debug.Log("replace with effect"); //has been changed
                    }
                    else
                    {
                        WordPronunciationRadio.wrongAnsbool = true;
                    }
                }
            }
        }
    }


        static public void soundButtonClicked()
        {
            spwneffect = true;
 
            anim.Play("BlockAnimation");
            audioSource.Play();
            Debug.Log("Func soundButtonClicked() audiosource name" + audioSource.name);
            Debug.Log("Colliding and Sound will Play");
        
        } 

        
    static public void CheckCorrectMatch(GameObject chosenLetter) //to increase score if correct answer
    {
        Debug.Log("CheckCorrectMatch is being called");
        Debug.Log("obj1" + SpawnBlocksInArea.objAudioSource.clip.name);
        Debug.Log("obj2" + chosenLetter.GetComponent<AudioSource>().clip.name);
        if (chosenLetter.GetComponent<AudioSource>().clip.name.Equals(SpawnBlocksInArea.objAudioSource.clip.name))
        {
            Debug.Log("CheckCorrectMatch is true");
            Debug.Log("keyIndex" + SpawnBlocksInArea.keyIndex);
            checkMatch = true;
            SpawnBlocksInArea.keyIndex++;
            //score here?
            //increase score
            SpawnBlocksInArea.StudentScore += 1;
            Debug.Log("True SloundLetter StudentScore:" + SpawnBlocksInArea.StudentScore);

        }
        else
        {
            SpawnBlocksInArea.StudentScore += 1;
            Debug.Log("wrong answer");
            Debug.Log(" False SoundLetter StudentScore:" + SpawnBlocksInArea.StudentScore);
        }
        
        
        checkMatch = false;
    }
        
    }

    






