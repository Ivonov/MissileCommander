using UnityEngine;
using System.Collections;

public class PlayerCameraControler : MonoBehaviour {
    public float cameraSpeed = 1.5f;
    public float traveldistance = 1f;
    public float rotateSensitivityX = 100f;
    public float rotateSensitivityY = 100f;
    public float zoomSpeed = 100f;

    private bool fisrtTime = true; //First frame that the camare gets rotated
    private float rotX;
    private float rotY;

    private Camera mainCamera;
    private Vector3 startPosition;
    private Vector3 targetCameraPosition; //The new Vector that the camera needs to move to.

    public Texture2D cursorTexture; 
    public CursorMode cursorMode = CursorMode.Auto; 
    private Vector2 cursorSpot = Vector2.zero;


	/// <summary>
    /// Use this for initialization
	/// </summary>
	void Start () {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        startPosition = mainCamera.transform.position;
        Cursor.SetCursor(cursorTexture, cursorSpot, cursorMode); 
	}
	
    /// <summary>
    /// Update is called once per frame.
    /// </summary>
	void Update () {
        checkUserInput();
	}

    /// <summary>
    /// This method checks what input has been given in this frame.
    /// </summary>
    private void checkUserInput() {
        if (Input.GetKey(KeyCode.W)) {
            setTargetPosition("W");
            moveCamera();
        }
        if (Input.GetKey(KeyCode.S)) {
            setTargetPosition("S");
            moveCamera();
        }
        if (Input.GetKey(KeyCode.A)) {
            setTargetPosition("A");
            moveCamera();
        }
        if (Input.GetKey(KeyCode.D)) {
            setTargetPosition("D");
            moveCamera();
        }
        if(Input.GetMouseButton(2)){ //Middle mouse click
            rotateCamera();
        }

        if(Input.GetMouseButton(0)){
            Debug.Log("Dylan is gay");
            tryToSelectGameObject(getGameComponentFromPixelPosition());
        }
        zoomCamera();
    }

    private void tryToSelectGameObject(Vector3 position) { 
        
    }

    private Vector3 getGameComponentFromPixelPosition() {
        Vector3 tempVector = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return tempVector;
    }

    /// <summary>
    /// Every frame the scroll gets checked and is translated in the zoom motion
    /// </summary>
    private void zoomCamera() {
        transform.Translate(Vector3.forward * Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime);
    }
    /// <summary>
    /// Manages the mouse input and translate it in the rotation
    /// </summary>
    /// <param name="direction"></param>
    private void rotateCamera() {
        if (fisrtTime) {
            setRotationCorrectionForAxisX();
        } else {
            rotX += -Input.GetAxis("Mouse Y") * rotateSensitivityY * Time.deltaTime;
            rotY += Input.GetAxis("Mouse X") * rotateSensitivityX * Time.deltaTime;
        }
        transform.localEulerAngles = new Vector3(rotX % 360, rotY % 360, 0);
    }

    /// <summary>
    /// This is the correction made so that the camera doesnt glitch when the camera rotates for the fist time.
    /// (First it went to [0,0,0], now it starts at the original start position)
    /// </summary>
    private void setRotationCorrectionForAxisX() {
        rotX = mainCamera.transform.rotation.eulerAngles.x;
        rotY += Input.GetAxis("Mouse X") * rotateSensitivityX * Time.deltaTime;
        fisrtTime = false;
    }

    /// <summary>
    /// This method will move the camera with the Lerp(smooth move) function.
    /// This is all based on keyboard and mouse input that generates the targetPosition.
    /// </summary>
    private void moveCamera() { 
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetCameraPosition, cameraSpeed * Time.deltaTime); 
    }

    /// <summary>
    /// This method gets the input given by the player and translates it into a targetPosition variable.
    /// </summary>
    /// <param name="keypress"></param>
    private void setTargetPosition(string keypress)
    {
        switch (keypress) {
            case "W":
                targetCameraPosition = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z + traveldistance);
                break;
            case "S":
                targetCameraPosition = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z - traveldistance);
                break;
            case "A":
                targetCameraPosition = new Vector3(mainCamera.transform.position.x - traveldistance, mainCamera.transform.position.y, mainCamera.transform.position.z);
                break;
            case "D":
                targetCameraPosition = new Vector3(mainCamera.transform.position.x + traveldistance, mainCamera.transform.position.y, mainCamera.transform.position.z);
                break;
        }
    }
}
