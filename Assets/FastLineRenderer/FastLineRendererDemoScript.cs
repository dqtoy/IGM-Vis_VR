using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

namespace DigitalRuby.FastLineRenderer
{
    [ExecuteInEditMode]
    public class FastLineRendererDemoScript : MonoBehaviour
    {
        public FastLineRenderer LineRenderer;
        public float MoveSpeed = 50.0f;
        public bool ShowCurves;

        private float deltaTime = 0.0f;
        private float msec = 0.0f;
        private float fps = 0.0f;
        private int lineCount;

        private void Start()
        {
            AddCurvesAndSpline();
        }

        private void Update()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

            if (Camera.main.orthographic)
            {
                // 1 pixel = 1 world unit
                Camera.main.orthographicSize = Screen.height * 0.5f;
                Camera.main.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, -10.0f);
            }

        }

        private void AddCurvesAndSpline()
        {
            if (ShowCurves)
            {
                const float animationTime = 0.025f;

                ShowCurves = false;
                FastLineRendererProperties props = new FastLineRendererProperties();
                props.GlowIntensityMultiplier = 0.5f;
                props.Radius = 4.0f;
                props.Color = UnityEngine.Color.white;
                // props.Start = new Vector3(Screen.width * 0.1f, Screen.height * 0.1f, 0.0f);
                // props.End = new Vector3(Screen.width * 1.1f, Screen.height * 1.0f, 0.0f);
                // LineRenderer.AppendCurve(props,
                //     new Vector3(Screen.width * 0.33f, Screen.height * 0.67f, 0.0f), // control point 1
                //     new Vector3(Screen.width * 0.67f, Screen.height * 0.33f, 0.0f), // control point 2
                //     16, true, true, animationTime);

                // props.Color = UnityEngine.Color.red;
                // props.Start = new Vector3(0.0f, Screen.height * 0.2f, 0.0f);
                // props.End = new Vector3(Screen.width * 1.2f, Screen.height * 0.2f, 0.0f);

                // Vector3[] spline = new Vector3[]
                // {
                //     props.Start,
                //     new Vector3(Screen.width * 0.2f, Screen.height * 0.8f, 0.0f),
                //     new Vector3(Screen.width * 0.4f, Screen.height * 0.2f, 0.0f),
                //     new Vector3(Screen.width * 0.6f, Screen.height * 0.8f, 0.0f),
                //     new Vector3(Screen.width * 0.8f, Screen.height * 0.2f, 0.0f),
                //     new Vector3(Screen.width, Screen.height * 0.8f, 0.0f),
                //     props.End
                // };
                LineRenderer.AppendLine(props);
                // LineRenderer.AppendSpline(props, spline, 128, FastLineRendererSplineFlags.StartCap | FastLineRendererSplineFlags.EndCap, animationTime);

                // add a circle and arc
                // props.Color = Color.green;
                // LineRenderer.AppendCircle(props, new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f), 100.0f, 64, Vector3.forward, animationTime);
                // LineRenderer.AppendArc(props, new Vector3(Screen.width * 0.25f, Screen.height * 0.5f, 0.0f), 100.0f, 270.0f, 90.0f, 32, Vector3.forward, false, animationTime);
                // LineRenderer.AppendArc(props, new Vector3(Screen.width * 0.75f, Screen.height * 0.5f, 0.0f), 100.0f, 0.0f, 360.0f, 32, Vector3.forward, false, animationTime);

                LineRenderer.Apply();
            }
        }

    }
}