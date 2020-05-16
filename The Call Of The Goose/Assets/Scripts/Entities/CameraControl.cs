﻿using System;
using Level;
using UnityEngine;

namespace Entities
{
    public class CameraControl : MonoBehaviour
    {
        public Camera playerCamera;
        private bool _global,
            _previousState,
            _input;

        private void Start()
        {
            _global = false;
        }

        // Update is called once per frame
        void Update()
        {
            _previousState = _input;
            _input = Input.GetKey(KeyCode.C);
            if (!_input || _input == _previousState) return;
            playerCamera.orthographicSize = _global ? 10f : 100f;
            _global = !_global;
        }
    }
}
