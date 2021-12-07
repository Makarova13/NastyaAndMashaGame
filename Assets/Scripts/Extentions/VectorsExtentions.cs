using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Extentions
{
    public static class VectorsExtentions
    {
        public static bool AbsLessThan(this Vector3 vector1, Vector3 vector2)
        {
            return Math.Abs(vector1.x) < vector2.x && Math.Abs(vector1.y) < vector2.y && Math.Abs(vector1.z) < vector2.z;
        }
    }
}