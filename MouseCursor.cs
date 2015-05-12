using UnityEngine;
using System.Collections;

public class MouseCursor : MonoBehaviour {
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

	/// <summary>
    /// Use this for initialization
	/// </summary>
	void Start () {
        Cursor.visible = true;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        Debug.Log("Test");
	}
	
	/// <summary>
    /// Update is called once per frame
	/// </summary>
	void Update () {
	
	}


}
