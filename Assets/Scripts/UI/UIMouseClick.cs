using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMouseClick : StandaloneInputModule
{
    private readonly MouseState m_MouseState = new MouseState();
    public bool newMode;
    protected override MouseState GetMousePointerEventData(int id)
    {
        // Populate the left button...
        PointerEventData leftData;
        var created = GetPointerData(kMouseLeftId, out leftData, true);

        leftData.Reset();

        if (created)
            leftData.position = GetMousePositionRelativeToMainDisplayResolution();

        Vector2 pos = GetMousePositionRelativeToMainDisplayResolution();
        /*if (Cursor.lockState == CursorLockMode.Locked)
        {
            // We don't want to do ANY cursor-based interaction when the mouse is locked
            leftData.position = new Vector2(-1.0f, -1.0f);
            leftData.delta = Vector2.zero;
        }*/

        leftData.delta = pos - leftData.position;
        leftData.position = pos;
        
        leftData.scrollDelta = input.mouseScrollDelta;
        leftData.button = PointerEventData.InputButton.Left;
        eventSystem.RaycastAll(leftData, m_RaycastResultCache);
        var raycast = FindFirstRaycast(m_RaycastResultCache);
        leftData.pointerCurrentRaycast = raycast;
        m_RaycastResultCache.Clear();

        // copy the apropriate data into right and middle slots
        PointerEventData rightData;
        GetPointerData(kMouseRightId, out rightData, true);
        CopyFromTo(leftData, rightData);
        rightData.button = PointerEventData.InputButton.Right;

        PointerEventData middleData;
        GetPointerData(kMouseMiddleId, out middleData, true);
        CopyFromTo(leftData, middleData);
        middleData.button = PointerEventData.InputButton.Middle;

        m_MouseState.SetButtonState(PointerEventData.InputButton.Left, StateForMouseButton(0), leftData);
        m_MouseState.SetButtonState(PointerEventData.InputButton.Right, StateForMouseButton(1), rightData);
        m_MouseState.SetButtonState(PointerEventData.InputButton.Middle, StateForMouseButton(2), middleData);

        return m_MouseState;
    }
    
    public Vector2 GetMousePositionRelativeToMainDisplayResolution()
    {
        Vector3 position = Input.mousePosition;
        position = newMode ? new Vector3(Screen.width/2f, Screen.height/2f, 0) : Input.mousePosition;
        #if !UNITY_EDITOR
            if (Display.main.renderingHeight != Display.main.systemHeight)
            {
                // The position is relative to the main render area, we need to adjust this so
                // it is relative to the system resolution in order to correctly determine the position on other displays.

                // Correct the y position if we are outside the main display.
                if (position.y < 0 || position.y > Display.main.renderingHeight ||
                    position.x < 0 || position.x > Display.main.renderingWidth)
                {
                    position.y += Display.main.systemHeight - Display.main.renderingHeight;
                }
            }
        #endif
        return position;
    }
    
    protected override void ProcessMove(PointerEventData pointerEvent)
    {
        var targetGO = pointerEvent.pointerCurrentRaycast.gameObject;
        HandlePointerExitAndEnter(pointerEvent, targetGO);
    }
    
    public override void Process()
    {
        
        bool usedEvent = SendUpdateEventToSelectedObject();

        // case 1004066 - touch / mouse events should be processed before navigation events in case
        // they change the current selected gameobject and the submit button is a touch / mouse button.

        // touch needs to take precedence because of the mouse emulation layer
      
        ProcessMouseEvent();

           if (!usedEvent)
                usedEvent |= SendMoveEventToSelectedObject();

            if (!usedEvent)
                SendSubmitEventToSelectedObject();
        
    }


}
