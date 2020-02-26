using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleBehaviour : MonoBehaviour
{
    TMPro.TextMeshPro text;
    bool isUpdated = false;
    string initStr = "Console initialized";
    string inputData;
    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<TMPro.TextMeshPro>();
        text.text = initStr;
        isUpdated = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isUpdated)
        {
            text.text += "\n" + inputData;
            isUpdated = true;
        }
    }

    public void write(string data)
    {
        inputData = data;
        isUpdated = false;
    }
}
