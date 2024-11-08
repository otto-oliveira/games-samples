using UnityEngine;

namespace DefaultNamespace
{
    public class DisplayRateProxy : AndroidJavaProxy
    {
        public DisplayRateProxy() : base("")
        {
        }
        
        public void myMethod()
        {
            Debug.Log("myMethod called from Java");
        }

        public string myMethodWithReturn(string input)
        {
            Debug.Log("myMethodWithReturn called from Java with input: " + input);
            return "Hello from C#";
        }
    }
}