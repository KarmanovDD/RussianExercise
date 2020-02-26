using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letterTriggerHandler : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "letter")
        {
            FindObjectOfType<WordBehavour>().handleInputLetter(name, other);
            //other.GetComponent<letterBehaviour>().returnToBase();
            //Destroy(other.gameObject);
        }

    }
}
