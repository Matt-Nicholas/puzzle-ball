using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class SnapParams:ScriptableObject
    {
        public Transform snappableTransform;
        public bool xFlip;
        public bool yFlip;

    }
}
