using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera MainCamera;
    public GameObject follow;

    private bool disabledLevelMap;

    public void Start()
    {
        if (MainCamera == null)
        {
            MainCamera = Camera.main;
        }

       

        
    }

    public void Update()
    {
        
    }

    

    public void LateUpdate()
    {
        MainCamera.transform.position = new Vector3(follow.transform.position.x, follow.transform.position.y, MainCamera.transform.position.z);
    }
}
