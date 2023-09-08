using System;

namespace com.limphus.utilities
{
    public class Events
    {
        public class OnIntChangedEventArgs : EventArgs { public int i; }
        public class OnFloatChangedEventArgs : EventArgs { public float i; }
        public class OnBoolChangedEventArgs : EventArgs { public bool i; }
        public class OnStringChangedEventArgs : EventArgs { public string i; }
        public class OnCharChangedEventArgs : EventArgs { public char i; }
        public class OnDoubleChangedEventArgs : EventArgs { public double i; }
    }
}