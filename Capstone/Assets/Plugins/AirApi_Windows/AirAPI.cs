// MIT License
// Copyright (c) 2023 MSmithDev

// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class AirAPI : MonoBehaviour
{
    [DllImport("AirAPI_Windows", CallingConvention = CallingConvention.Cdecl)]
        static extern int StartConnection();

    [DllImport("AirAPI_Windows", CallingConvention = CallingConvention.Cdecl)]
        static extern int StopConnection();

    [DllImport("AirAPI_Windows", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr GetQuaternion();

    [DllImport("AirAPI_Windows", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr GetEuler();

    float[] EulerArray = new float[3];
    IntPtr EulerPtr;

    float[] QuaternionArray = new float[4];
    IntPtr QuaternionPtr;

    public bool gyroEnabled = false;

    void Start()
    {
        // Start the connection
       var res = StartConnection();
       if(res == 1)
       {
           Debug.Log("Connection started");
       }
       else
       {
           Debug.Log("Connection failed");
       }
    }

    void Update()
    {
        if (gyroEnabled)
        {
            // Get array data from memory
            // QuaternionPtr = GetQuaternion();
            // Marshal.Copy(QuaternionPtr, QuaternionArray, 0, 4);

            EulerPtr = GetEuler();
            Marshal.Copy(EulerPtr, EulerArray, 0, 3);

            // Print data
            // Debug.Log("Quaternion: " + QuaternionArray[0] + " " + QuaternionArray[1] + " " + QuaternionArray[2] + " " + QuaternionArray[3]);
            // Debug.Log("Euler: " + EulerArray[0] + " " + EulerArray[1] + " " + EulerArray[2]);

            Quaternion localRotation = Quaternion.Euler((-EulerArray[1]) + 90.0f, -EulerArray[2], -EulerArray[0]);
            this.transform.rotation = localRotation;
        }
    }
}
