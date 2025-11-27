using UnityEngine;

public class Player_Cam : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;

    [Header("Camera Settings")]
    public float distance = 5f;
    public float height = 2f;
    public float rotationSpeed = 5f;
    public float smoothSpeed = 10f;

    [Header("Mouse Angle Limit")]
    public float minAngel = -20f;
    public float maxAngel = 60f;
    private float currentX = 0f;
    private float currentY = 20f; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(target == null)
        {
            Debug.Log("카메라 지정 필요");
            return;
        }
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target == null) return;

        currentX += Input.GetAxis("Mouse X") * rotationSpeed;
        currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        currentY = Mathf.Clamp(currentY, minAngel, maxAngel);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 offSet = rotation * new Vector3(0, height, -distance);
        Vector3 desirePosition = target.position + offSet;

        transform.position = Vector3.Lerp(transform.position, desirePosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(target.position + Vector3.up * height);
    }
    public Vector3 GetCameraForward()
    {
        Vector3 forward = transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    public Vector3 GetCameraRight()
    {
        Vector3 right = transform.right;
        right.y = 0;
        return right.normalized;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
