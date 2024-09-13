using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 나중에 게임매니저로 이동
public class CursorManager : MonoBehaviour
{
    public Texture2D cursorTexture; // 십자선 이미지
    private Vector2 hotSpot = Vector2.zero; // 커서의 핫스팟 (기본적으로 이미지의 좌상단)

    void Start()
    {
        hotSpot = new Vector2(Camera.main.pixelWidth/2, Camera.main.pixelHeight/2);
        // 커서 모양을 십자선 이미지로 변경하고, 커서를 화면 중앙에 고정
        //Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.Locked; // 커서를 화면 중앙에 고정
        Cursor.visible = false; 
    }

    void Update()
    {
        // ESC를 누르면 커서가 화면 중앙에 고정되는 것을 해제
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // 클릭 시 다시 커서를 화면 중앙에 고정
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
