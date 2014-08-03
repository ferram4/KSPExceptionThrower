/* The MIT License (MIT)
 *
 * Copyright (c) 2014 Michael Ferrara
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this 
 * software and associated documentation files (the "Software"), to deal in the Software
 * without restriction, including without limitation the rights to use, copy, modify, 
 * merge, publish, distribute, sublicense, and/or sell copies of the Software, and to 
 * permit persons to whom the Software is furnished to do so, subject to the following 
 * conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies 
 * or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
 * PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE 
 * LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE
 * OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using KSP;

namespace KSPExceptionThrower
{
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    public class KSPExceptionThrower : UnityEngine.MonoBehaviour
    {
        private Rect windowRect = new Rect(100, 100, 300, 140);
        private string numExceptions_str = "0";
        private int numExceptions = 0;

        //Make sure that it is enabled so OnGUI works
        public void Start()
        {
            this.enabled = true;
        }

        public void OnGUI()
        {
            windowRect = GUI.Window(this.GetHashCode(), windowRect, ExceptionThrowerGUI, "KSP OnGUI Exception Thrower v1.0");
        }

        private void ExceptionThrowerGUI(int id)
        {
            GUILayout.Label("This is to check exception handling in KSP");
            if (GUILayout.Button("Throw Exception"))
            {
                throw new PurposefulException();
            }
            GUILayout.BeginHorizontal();
            GUILayout.Label("Num Exceptions:");
            numExceptions_str = Regex.Replace(GUILayout.TextField(numExceptions_str), @"[\D]", "");
            GUILayout.EndHorizontal();
            if (GUILayout.Button("Throw Multiple Exception"))
            {
                numExceptions = int.Parse(numExceptions_str);
            }

            if (numExceptions > 0)
            {
                numExceptions--;
                throw new PurposefulException();
            }

            GUI.DragWindow();
        }
    }

    [Serializable]
    public class PurposefulException : Exception
    {
      public PurposefulException() { }
      public PurposefulException( string message ) : base( message ) { }
      public PurposefulException( string message, Exception inner ) : base( message, inner ) { }
      protected PurposefulException( 
	    System.Runtime.Serialization.SerializationInfo info, 
	    System.Runtime.Serialization.StreamingContext context ) : base( info, context ) { }
    }
}
