using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogBoxText;
    public string signText;
    private bool isPlayerinSign;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ( isPlayerinSign)
        {
     //     Debug.Log("½øÈë·¶Î§");
            dialogBox.SetActive(true);
            dialogBoxText.text = signText;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
     // Debug.Log("½øÈëÕÐÅÆ·¶Î§");
        isPlayerinSign = true;
        /*if (other.gameObject.CompareTag("Player")
            && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            Debug.Log("½øÈëÕÐÅÆ·¶Î§");
        }*/
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Àë¿ªÕÐÅÆ·¶Î§");
        isPlayerinSign = false;
            dialogBox.SetActive(false);
        if (other.gameObject.CompareTag("Player")
            && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            Debug.Log("Àë¿ªÕÐÅÆ·¶Î§");
        }
    }
}
