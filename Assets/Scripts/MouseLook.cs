﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MouseLook : MonoBehaviour {
    public static MouseLook Instance;

    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public bool clampVerticalRotation = true;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
    public bool smooth;
    public float smoothTime = 5f;

    public bool ThroughPortal;
    public Quaternion Portal;

    private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;


    public void Init(Transform character, Transform camera)
    {
        Instance = this;
        m_CharacterTargetRot = character.localRotation;
        m_CameraTargetRot = camera.localRotation;
    }


    public void LookRotation(Transform character, Transform camera)
    {
        float yRot = Input.GetAxis("Mouse X") * XSensitivity;
        float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

        if (ThroughPortal)
        {
            m_CharacterTargetRot = Portal;
            character.localRotation = Portal;
        }
        else if(Player1Controller.Instance.Dead)
        {
            if (m_CharacterTargetRot.eulerAngles.z < 90)
            {
                m_CharacterTargetRot *= Quaternion.Euler(0f, 0f, 30f * Time.deltaTime);
                character.localRotation = m_CharacterTargetRot;
            }
            else
            {
                //Destroy(Camera.main);
                SceneManager.LoadScene("__StartScene");
            }
        }
        else {
            m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
            m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

            if (clampVerticalRotation)
                m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);

            if (smooth)
            {
                character.localRotation = Quaternion.Slerp(character.localRotation, m_CharacterTargetRot,
                    smoothTime * Time.deltaTime);
                camera.localRotation = Quaternion.Slerp(camera.localRotation, m_CameraTargetRot,
                    smoothTime * Time.deltaTime);
            }
            else
            {
                character.localRotation = m_CharacterTargetRot;
                camera.localRotation = m_CameraTargetRot;
            }
        }
        ThroughPortal = false;
    }


    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

}
