using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RayTracing
{
    public class Ray
    {
        public Vector3 from;
        public Vector3 direction;



        public Ray(Vector3 from, Vector3 direction)
        {
            this.from = from;
            this.direction = direction;
        }

        public Vector3 GetPointAt(float t)
        {
            return from + direction * t;
        }
    }
}
