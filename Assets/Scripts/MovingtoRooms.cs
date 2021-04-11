using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingtoRooms : MonoBehaviour
{
    public Animator anim;
    public Animator transition; //attached in inspector
    private void OnCollisionEnter(Collision collision)
    {
        GameObject player = collision.gameObject;
        GameObject door = this.gameObject;

        if (player.CompareTag("Player"))
        {
            if (door.CompareTag("ExitDoor"))
                StartCoroutine(LoadNextScene(0)); // leaving rooms - spawn back at main floor
            else if (door.CompareTag("RoomDoor"))
            {
                GameObject doorRoom = this.gameObject.transform.parent.gameObject;
                if (doorRoom.name.Equals("Room1Door"))
                    StartCoroutine(LoadNextScene(1)); // go to first room
                if (doorRoom.name.Equals("Room2Door") && DestryKeyOnCollision.keyCollected2)
                    StartCoroutine(LoadNextScene(2)); // go to second room
                if (doorRoom.name.Equals("Room3Door") && DestryKeyOnCollision.keyCollected3)
                    StartCoroutine(LoadNextScene(3)); // go to third room
              
            }
        }
    }

    IEnumerator LoadNextScene(int sceneBuildIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneBuildIndex);
    }
}
