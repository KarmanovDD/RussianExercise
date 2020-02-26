using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hovering : MonoBehaviour
{
    public GameObject HoveringParticlesPrefab;
    public float amplitudeY = 0.02f;
    public float frequencyY = 1f;
    public float amplitudeZ = 0.001f;
    public float frequencyZ = 5f;
    public float phaseY;
    private Vector3 initPos;
    [HideInInspector]
    public GameObject parts;

    void Awake()
    {
        parts = Instantiate(HoveringParticlesPrefab, transform);
        parts.transform.localPosition = new Vector3(0f, -0.1f, 0f);
        
    }
    private void OnEnable()
    {
        parts.SetActive(true);
        setPos();
    }
    private void OnDisable()
    {
        parts.SetActive(false);
    }

    void setPos()
    {
        initPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        phaseY = Random.Range(0, Mathf.PI);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = new Vector3(initPos.x, initPos.y + sinSquared(amplitudeY, frequencyY, phaseY), initPos.z + sinSquared(amplitudeZ, frequencyZ, 0));
        transform.position = new Vector3(initPos.x, initPos.y + sinSquared(amplitudeY, frequencyY, phaseY), initPos.z + sinSquared(amplitudeZ, frequencyZ, 0));
    }

    float sinSquared(float amplitude, float frequency, float phase)
    {
        return amplitude * (float)Mathf.Pow(Mathf.Sin(frequency * Time.time + phase), 2);
    }
}
