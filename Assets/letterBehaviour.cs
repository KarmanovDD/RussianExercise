using UnityEngine;


public class letterBehaviour : MonoBehaviour
{
    public float resetTime = 10;
    Vector3 initPos;
    float prevPosX;
    float prevPosZ;
    Vector3 initRot;
    float lastTimeMoved = 0;
    Vector3 currentAngle;
    Vector3 prevPos;
    Vector3 prevRot;
    Rigidbody body;
    Hovering hover;
    Collider collider;
    bool stopHovering = false;
    public bool GoHome { get; set; }
    void Start()
    {
        body = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
        initPos = transform.position;
        initRot = transform.eulerAngles;
        currentAngle = transform.eulerAngles;
        prevPos = initPos;
        prevRot = initRot;
        //prevPosX = initPos.x;
        //prevPosZ = initPos.z;
        hover = GetComponent<Hovering>();
        hover.enabled = true;
        GoHome = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if (!(collision.gameObject.name == "HandColliderRight(Clone)" || collision.gameObject.name == "HandColliderLeft(Clone)"))
            //stopHovering = true;
    }

    private void OnTransformParentChanged()
    {
        stopHovering = true;
    }
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (!(collision.gameObject.name == "HandColliderRight" || collision.gameObject.name == "HandColliderLeft"))
    //        stopHovering = false;
    //}

    private void FixedUpdate()
    {
        if (Time.time - lastTimeMoved > resetTime)// && ( (transform.position-initPos).magnitude > 0.1 || (transform.eulerAngles - initRot).magnitude > 3 ))
            returnToBase();
        if ( (transform.position - prevPos).magnitude > 0.001 || (transform.eulerAngles - prevRot).magnitude > 3 )
        {
            //stopHovering = true;
            prevPos = transform.position;
            prevRot = transform.eulerAngles;
            lastTimeMoved = Time.time;
        }
        
        if(stopHovering && hover.enabled)
            hover.enabled = false;
        if (!stopHovering && !hover.enabled)
            hover.enabled = true;

        if (GoHome)
        {
            getHome(10f, 0.002f);
            lastTimeMoved = Time.time;
            if ((transform.position - initPos).magnitude < 0.01)
                resetState();
        }
    }


    public void returnToBase()
    {
        GoHome = true;
        //body.isKinematic = true;
        collider.enabled = false;
        currentAngle = transform.eulerAngles;
        stopHovering = true;
        hover.parts.SetActive(true);
    }

    void getHome(float Kp, float angKp)
    {
        Vector3 gravity = Physics.gravity * GetComponent<Rigidbody>().mass;
        float forceX = (transform.position.x - initPos.x);
        float forceY = (transform.position.y - initPos.y);
        float forceZ = (transform.position.z - initPos.z);
        body.AddForce(-Kp * new Vector3(forceX, forceY, forceZ) - gravity);
        //Debug.Log(-Kp * new Vector3(forceX, forceY, forceZ)); 

        //currentAngle = new Vector3(
        //Mathf.LerpAngle(currentAngle.x, initRot.x, Time.deltaTime),
        //Mathf.LerpAngle(currentAngle.y, initRot.y, Time.deltaTime),
        //Mathf.LerpAngle(currentAngle.z, initRot.z, Time.deltaTime));

        //transform.eulerAngles = currentAngle;

        float deltax = Mathf.DeltaAngle(initRot.x, transform.localRotation.eulerAngles.x);
        //float deltaxx = Mathf.DeltaAngle( initRot.x, transform.localRotation.eulerAngles.x);
        ////float deltaxxx = 180 / Mathf.PI * (initRot.x - transform.localRotation.eulerAngles.x);
        float deltay = Mathf.DeltaAngle(initRot.y, transform.localRotation.eulerAngles.y);
        //float deltayy = Mathf.DeltaAngle(initRot.y, transform.localRotation.eulerAngles.y);

        float deltaz = Mathf.DeltaAngle(initRot.z, transform.localRotation.eulerAngles.z);
        //float deltazz = Mathf.DeltaAngle(initRot.z, transform.localRotation.eulerAngles.z);

        //Vector3 angVelocity = -angKp * new Vector3(deltaxx, deltayy, deltazz);

        //currentAngle += angVelocity * Time.deltaTime;
        //transform.eulerAngles = currentAngle;




        body.AddRelativeTorque(-angKp * new Vector3(deltax, deltay, deltaz));

        //body.AddRelativeTorque(-angKp * new Vector3(deltax, 0, 0));
        //body.AddRelativeTorque(-angKp * new Vector3(0, deltay, 0));
        //body.AddRelativeTorque(-angKp * new Vector3(0, 0, deltaz));

        //body.AddTorque(/*-angKp **/-0.01f * (transform.rotation.eulerAngles - initRot).normalized);
        //print(transform.rotation.eulerAngles.ToString() + initRot.eulerAngles.ToString()+ " " + "delta: " + deltax.ToString() + " "+ deltay.ToString() + " " + deltaz.ToString() + "\n" + "Torque: " + (1000*(-angKp * new Vector3(deltax, deltay, deltaz))).ToString());
    }


    public void resetState()
    {
        GoHome = false;
        transform.position = initPos;
        transform.eulerAngles = initRot;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        collider.enabled = true;
        stopHovering = false;
    }

}
