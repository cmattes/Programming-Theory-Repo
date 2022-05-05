using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject playerBody;
    public float xRotation = 0f;
    private float mouseSensitivity = 500;
    private float movementSpeed = 10;

    private const float UpperZBound = 24.0f;
    private const float LowerZBound = -24.0f;
    private const float RightXBound = 24.0f;
    private const float LeftXBound = -24.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMouseLock();
        GetMouseMovement();
        GetKeyboardMovement();
        CheckBoundaries();
    }

    private void CheckMouseLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void GetMouseMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.gameObject.transform.Rotate(Vector3.up * mouseX);
    }

    private void GetKeyboardMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        playerBody.gameObject.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime * verticalInput);
        playerBody.gameObject.transform.Translate(Vector3.right * movementSpeed * Time.deltaTime * horizontalInput);
    }

    // Make sure player stay in our game bounds
    private void CheckBoundaries()
    {
        if (playerBody.gameObject.transform.position.z > UpperZBound)
        {
            playerBody.gameObject.transform.position = new Vector3(playerBody.gameObject.transform.position.x, playerBody.gameObject.transform.position.y, UpperZBound);
        }

        if (playerBody.gameObject.transform.position.z < LowerZBound)
        {
            playerBody.gameObject.transform.position = new Vector3(playerBody.gameObject.transform.position.x, playerBody.gameObject.transform.position.y, LowerZBound);
        }

        if (playerBody.gameObject.transform.position.x > RightXBound)
        {
            playerBody.gameObject.transform.position = new Vector3(RightXBound, playerBody.gameObject.transform.position.y, playerBody.gameObject.transform.position.z);
        }

        if (playerBody.gameObject.transform.position.x < LeftXBound)
        {
            playerBody.gameObject.transform.position = new Vector3(LeftXBound, playerBody.gameObject.transform.position.y, playerBody.gameObject.transform.position.z);
        }
    }
}
