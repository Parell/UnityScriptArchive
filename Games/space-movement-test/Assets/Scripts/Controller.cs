using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public Rigidbody rb;
    [Space]
    [Range(0, 1)]
    public float throttle = 1f;
    [Space]
    public float speed = 150f;
    public float rollSpeed = 50f;
    public float turnSpeed = 0.1f;
    public float fuel = 100f;
    public int angularDrag = 1;
    [Space]
    public bool cursorLocked = true;
    public bool invertY = false;
    public bool sas = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Sas();
        if (fuel <= 0)
        {
            fuel = 0;
            throttle = 0;
            sas = false;
        }
        else
        {
            Dash();
            Throttle();
            rb.AddRelativeForce(Force(), ForceMode.VelocityChange);
            rb.AddRelativeTorque(Torque(), ForceMode.VelocityChange);
            if (cursorLocked == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    private Vector3 Force()
    {
        var force = new Vector3();
        if (Keyboard.current.wKey.isPressed)
        {
            rb.AddRelativeForce(Vector3.forward * speed * throttle * Time.deltaTime);
            fuel = fuel - 0.01f * throttle;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            rb.AddRelativeForce(Vector3.back * speed * throttle * Time.deltaTime);
            fuel = fuel - 0.01f * throttle;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            rb.AddRelativeForce(Vector3.left * speed * throttle * Time.deltaTime);
            fuel = fuel - 0.01f * throttle;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            rb.AddRelativeForce(Vector3.right * speed * throttle * Time.deltaTime);
            fuel = fuel - 0.01f * throttle;
        }
        if (Keyboard.current.rKey.isPressed)
        {
            rb.AddRelativeForce(Vector3.up * speed * throttle * Time.deltaTime);
            fuel = fuel - 0.01f * throttle;
        }
        if (Keyboard.current.fKey.isPressed)
        {
            rb.AddRelativeForce(Vector3.down * speed * throttle * Time.deltaTime);
            fuel = fuel - 0.01f * throttle;
        }
        return force;
    }

    private Vector3 Torque()
    {
        float yaw = Input.GetAxis("Mouse X");
        //rb.AddRelativeTorque(transform.up * turnSpeed * yaw);
        float pitch = Input.GetAxis("Mouse Y") * (invertY ? 1 : -1);
        //rb.AddRelativeTorque(transform.up * turnSpeed * pitch);
        if (Keyboard.current.qKey.isPressed)
        {
            rb.AddRelativeTorque(Vector3.forward * rollSpeed * Time.deltaTime);
            fuel = fuel - 0.01f * throttle;
        }
        if (Keyboard.current.eKey.isPressed)
        {
            rb.AddRelativeTorque(Vector3.back * rollSpeed * Time.deltaTime);
            fuel = fuel - 0.01f * throttle;
        }
        return new Vector3(pitch, yaw) * turnSpeed;
    }

    private void Throttle()
    {
        throttle += Input.GetAxis("Mouse ScrollWheel");

        throttle = Mathf.Clamp(throttle, 0f, 1f);
    }

    private void Sas()
    {
        if (sas == true)
        {
            rb.angularDrag = angularDrag;
        }
        else
        {
            rb.angularDrag = 0f;
        }
    }

    private void Dash()
    {
        // timer
        if (Mouse.current.rightButton.isPressed)
        {

        }
    }
}