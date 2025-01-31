﻿using UnityEngine;
using System.Collections;

namespace Crux.Example.Player
{
    public class CruxHideMouse : MonoBehaviour
    {
        private bool MouseToggle;

        void Start()
        {
            MouseToggle = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MouseToggle = !MouseToggle;
            }

            if (MouseToggle)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
            }

            if (!MouseToggle)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}