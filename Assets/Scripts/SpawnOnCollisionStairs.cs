using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnCollisionStairs : MonoBehaviour
{
    [SerializeField]
    GameObject character;

    [SerializeField]
    GameObject spawnPoint;

    [SerializeField]
    string strTag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.tag == strTag)
        {

            character.transform.position = spawnPoint.transform.position;
        }
    }
}
