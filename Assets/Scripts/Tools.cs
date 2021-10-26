using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Tools
{
    public static void PrintLine(string message)
    {
        #if(UNITY_EDITOR)
            Debug.Log(message);
        #else
            return;
        #endif
    }
    // Start is called before the first frame update
    
   
}
