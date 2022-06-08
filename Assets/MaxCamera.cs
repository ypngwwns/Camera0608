using UnityEngine;

[AddComponentMenu("Camera-Control/3dsMax Camera Style")]
public class MaxCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 targetOffset;
    public float distance = 5.0f;
    public float maxDistance = 20;
    public float minDistance = .6f;
    public float xSpeed = 200.0f;
    public float ySpeed = 200.0f;
    public int yMinLimit = -80;
    public int yMaxLimit = 80;
    public int zoomRate = 40;
    public float panSpeed = 0.3f;
    public float zoomDampening = 5.0f;

    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
    private float currentDistance;
    private float desiredDistance;
    private Quaternion currentRotation;
    private Quaternion desiredRotation;
    private Quaternion rotation;
    private Vector3 position;
    private bool inited = false;

    private float pitch;
    private float yaw;

    void Start()
    {
        Init();
    }

    void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        //If there is no target, create a temporary target at 'distance' from the cameras current viewpoint
        if (!target)
        {
            GameObject go = new GameObject("Cam Target");
            // new Vector3(Screen.width/2,Screen.height/2,0);
            go.transform.position = transform.position + (transform.forward * distance);
            target = go.transform;
        }

        distance = Vector3.Distance(transform.position, target.position);
        currentDistance = distance;
        desiredDistance = distance;

        //be sure to grab the current rotations as starting points.
        position = transform.position;
        rotation = transform.rotation;
        currentRotation = transform.rotation;
        desiredRotation = transform.rotation;

        // xDeg = Vector3.Angle(Vector3.right, transform.right);
        // yDeg = Vector3.Angle(Vector3.up, transform.up);
        pitch = transform.eulerAngles.x;
        yaw = transform.eulerAngles.y;
    }

    /*
     * Camera logic on LateUpdate to only update after all character movement logic has been handled. 
     */
    void LateUpdate()
    {
        // If Control and Alt and Middle button? ZOOM!
        if (Input.GetMouseButton(2))
        {
            desiredDistance -= Input.GetAxis("Mouse Y") * Time.deltaTime * zoomRate * 0.125f *
                               Mathf.Abs(desiredDistance);
        }

        if (Input.GetMouseButtonDown(0))
        {
            
            Debug.Log("yDeg");
        }

        // If middle mouse and left alt are selected? ORBIT
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Input.GetAxis(Mouse X):" + Input.GetAxis("Mouse X"));
            Debug.Log("Input.GetAxis(Mouse Y):" + Input.GetAxis("Mouse Y"));
            
            xDeg = Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            yDeg = Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            ////////OrbitAngle

            pitch -= yDeg;
            //Clamp the vertical axis for the orbit
            pitch = ClampAngle(pitch, yMinLimit, yMaxLimit);
            yaw += xDeg;
            // set camera rotation 
            currentRotation = transform.rotation;
            desiredRotation = Quaternion.Euler(pitch, yaw, 0);

            Debug.Log("xDeg:" + xDeg);
            Debug.Log("yDeg:" + yDeg);
            Debug.Log("currentRotation:" + currentRotation);
            Debug.Log("desiredRotation:" + desiredRotation);
            rotation = Quaternion.Lerp(currentRotation, desiredRotation, 1);
            transform.rotation = rotation;
           
        }
        // otherwise if middle mouse is selected, we pan by way of transforming the target in screenspace
        else if (Input.GetMouseButton(1))
        {
            //grab the rotation of the camera so we can move in a psuedo local XY space
            target.rotation = transform.rotation;
            target.Translate(Vector3.right * -Input.GetAxis("Mouse X") * panSpeed);
            target.Translate(transform.up * -Input.GetAxis("Mouse Y") * panSpeed, Space.World);
        }

        ////////Orbit Position

        // affect the desired Zoom distance if we roll the scrollwheel
        desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
        //clamp the zoom min/max
        desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
        // For smoothing of the zoom, lerp distance
        currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * zoomDampening);

        // calculate position based on the new currentDistance 
        position = target.position - (rotation * Vector3.forward * currentDistance + targetOffset);
        transform.position = position;
        
       
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}