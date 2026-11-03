using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sickle_hit : MonoBehaviour
{
   // private Transform playerTransform; // 玩家 Transform 组件的引用  
    public GameObject sickle;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Instantiate(sickle, transform.position, transform.rotation);
        }
    }
}