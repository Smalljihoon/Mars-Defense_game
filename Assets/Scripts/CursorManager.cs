using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���߿� ���ӸŴ����� �̵�
public class CursorManager : MonoBehaviour
{
    public Texture2D cursorTexture; // ���ڼ� �̹���
    private Vector2 hotSpot = Vector2.zero; // Ŀ���� �ֽ��� (�⺻������ �̹����� �»��)

    void Start()
    {
        hotSpot = new Vector2(Camera.main.pixelWidth/2, Camera.main.pixelHeight/2);
        // Ŀ�� ����� ���ڼ� �̹����� �����ϰ�, Ŀ���� ȭ�� �߾ӿ� ����
        //Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.Locked; // Ŀ���� ȭ�� �߾ӿ� ����
        Cursor.visible = false; 
    }

    void Update()
    {
        // ESC�� ������ Ŀ���� ȭ�� �߾ӿ� �����Ǵ� ���� ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Ŭ�� �� �ٽ� Ŀ���� ȭ�� �߾ӿ� ����
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
