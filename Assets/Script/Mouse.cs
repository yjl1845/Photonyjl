using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; // ���콺 Ŀ�� ��Ȱ��ȭ (�Ⱥ��̰� ����)
        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ����
    }
}
