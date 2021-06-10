using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingtoRooms : MonoBehaviour
{
    public Animator transition; //attached in inspector
    private int levelNum;

    private void OnCollisionEnter(Collision collision)
    {
        levelNum = PlayerPrefs.GetInt("levelNum");
        GameObject player = collision.gameObject;
        GameObject door = this.gameObject;
        int lvlRoomBuildIndex = 0;
        if (levelNum.Equals(1))
            lvlRoomBuildIndex = 4;
        else if (levelNum.Equals(2))
            lvlRoomBuildIndex = 7;
        else if (levelNum.Equals(3))
            lvlRoomBuildIndex = 10;

        if (player.CompareTag("Player"))
        {
            if (door.CompareTag("ExitDoor"))
            {
                StartCoroutine(LoadNextScene(3)); // leaving rooms - spawn back at main floor
            }
                
            else if (door.CompareTag("RoomDoor"))
            {
                GameObject doorRoom = this.gameObject.transform.parent.gameObject;
                Debug.Log("doorRoom: " + doorRoom.name);
                Debug.Log("lvlNum: " + levelNum);
                Debug.Log("lvlRoomBuildIndex: " + lvlRoomBuildIndex);
                if (doorRoom.name.Equals("Room1Door"))
                    StartCoroutine(LoadNextScene(lvlRoomBuildIndex)); // go to first room
                if (doorRoom.name.Equals("Room2Door") /*&& DestryKeyOnCollision.keyCollected2*/)
                    StartCoroutine(LoadNextScene(lvlRoomBuildIndex+1)); // go to second room
                if (doorRoom.name.Equals("Room3Door") /*&& DestryKeyOnCollision.keyCollected3*/)
                    StartCoroutine(LoadNextScene(lvlRoomBuildIndex+2)); // go to third room
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
