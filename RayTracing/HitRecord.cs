using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RayTracing
{
    public class HitRecord
    {
        public Vector3 position;
        public float t;
        public Vector3 normal;
        public Material material;
    }
}
