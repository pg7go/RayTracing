using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RayTracing
{
    public abstract class Material
    {
        public abstract bool Scatter(Ray ray, HitRecord hit, out Vector3 attenuation, out Ray scattered);
    }
}
