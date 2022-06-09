using UnityEngine;

public class MaxCamera : MonoBehaviour
{
    
    // 相机视角视觉中心的高度，这个高度十分重要，根据实际的使用修改，最常使用的是地面高度
    public float groundHeight = 0f;
    //相机的视觉中心点，这个点根据groundHeight动态生产方式比较好，因为1.在脚本中平移需要改变target的位置 2.相机根据在编辑器设置的视角去生成target，不会在运行时的时候调到另一个视角
    private Transform target;
    
    // 相机与target的距离，之所以使用距离是为了较平滑的过渡
    private float distance = 0f;
    
    public float minDistance = 0.6f;
    
    // 这个值并不是设定了就有效的，相机定好初始化位置后，如果初始化位置大于maxDistance，会重置最大距离为初始位置
    public float maxDistance = 20;
  
    public float xSpeed = 200.0f;
    public float ySpeed = 200.0f;
    public int yMinLimit = -80;
    public int yMaxLimit = 80;
    public int zoomRate = 40;
    public float panSpeed = 0.3f;
    public float zoomDampening = 5.0f;

    private float currentDistance;
    private float desiredDistance;
    private Quaternion currentRotation;
    private Quaternion desiredRotation;
    
    // private Quaternion dRotation;//用于不频繁新建Quaternion对象
    // private Vector3 dPosition;

    // 旋转缓存
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
    
    protected float CalculateDistanceFromPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        float distance = Mathf.Abs((pos.y - groundHeight) / Mathf.Cos(Mathf.Deg2Rad * (90 - rot.eulerAngles.x)));
        return distance;
    }

    public void Init()
    {
        //If there is no target, create a temporary target at 'distance' from the cameras current viewpoint
        distance = CalculateDistanceFromPositionAndRotation(transform.position, transform.rotation);

        if (distance > maxDistance)
        {
            maxDistance = distance;
        }
        // GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cylinder);  
        GameObject go = new GameObject("Camera Target")
        {
            transform =
            {
                position = transform.position + (transform.forward.normalized * distance)
            }
        };
        target = go.transform;

        distance = Vector3.Distance(transform.position, target.position);
        currentDistance = distance;
        desiredDistance = distance;

        //be sure to grab the current rotations as starting points.
        // dPosition = transform.position;
        // dRotation = transform.rotation;
        currentRotation = transform.rotation;
        desiredRotation = transform.rotation;

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

        // If middle mouse and left alt are selected? ORBIT
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Input.GetAxis(Mouse X):" + Input.GetAxis("Mouse X"));
            Debug.Log("Input.GetAxis(Mouse Y):" + Input.GetAxis("Mouse Y"));
            
            float xDeg = Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            float yDeg = Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

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
            transform.rotation = Quaternion.Lerp(currentRotation, desiredRotation, 1);
            // transform.rotation = dRotation;
           
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
        transform.position = target.position - (transform.rotation * Vector3.forward * currentDistance);
        // transform.position = dPosition;
        
       
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