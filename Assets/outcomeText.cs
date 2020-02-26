using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using System;


public class outcomeText : MonoBehaviour
{
    public float fadeTime = 5f;
    public float initScale = 0.001f;
    public float finishScale = 5f;
    public Color32 correctColor = new Color32(0, 255, 0, 0);
    public Color32 mistakeColor = new Color32(255, 0, 0, 0);

    byte curAlpha = 0;
    float curScale;
    float initTime;

    bool started = false;
    bool isTextUpdated = false;
    enum outcome { Wrong,Right };
    outcome outcomeSign;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<TMPro.TextMeshPro>().faceColor = new Color32(0, 0, 0, 0);
        //MeshRenderer rend = GetComponent<MeshRenderer>();
        //rend.material.shaderKeywords = null;//.HasProperty("Outline");
        //rend.material.HasProperty.SetFloat("Thickness", 0.0f /* Your value here */);
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            GetComponent<MeshRenderer>().enabled = true;
            switch (outcomeSign)
            {
                case outcome.Right:
                    {
                        flushText("BEPHO!", correctColor);
                        break;

                    }
                case outcome.Wrong:
                    {
                        flushText("HEBEPHO!", mistakeColor);
                        break;
                    }
            }
        }
    }

    void flushText(string text, Color32 textColor)
    {
        try
        {
            if (!isTextUpdated)
            {
                GetComponent<TMPro.TextMeshPro>().text = text;
                isTextUpdated = true;
            }

            if (curAlpha > 0)
            {
                if (Mathf.RoundToInt(255 - 255 * (Time.time - initTime) / fadeTime) > 0)
                    curAlpha = Convert.ToByte(Mathf.RoundToInt(255 - 255 * Mathf.Pow((Time.time - initTime) / fadeTime, 1f / 2f)));
                else
                    curAlpha = 0;
                curScale = initScale + finishScale / fadeTime * (Time.time - initTime);
                GetComponent<TMPro.TextMeshPro>().faceColor = new Color32(textColor.r, textColor.g, textColor.b, curAlpha);//new Color32(0, 255, 0, curAlpha);
                transform.localScale = new Vector3(curScale, curScale, curScale);
            }
            else
            {
                //GetComponent<TMPro.TextMeshPro>().faceColor = new Color32(0,0,0,0);
                started = false;
                isTextUpdated = false;
                GetComponent<MeshRenderer>().enabled = false;
                Debug.Log(GetComponent<MeshRenderer>().enabled);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    void resetAlphaAndScale()
    {
        curScale = initScale;
        curAlpha = 255;
    }

    public void onRightWrite()
    {
        started = true;
        initTime = Time.time;
        outcomeSign = outcome.Right;
        resetAlphaAndScale();
    }
    public void onFailWrite()
    {
        started = true;
        initTime = Time.time;
        outcomeSign = outcome.Wrong;
        resetAlphaAndScale();
    }
}
