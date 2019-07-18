using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RayTracing
{
    public class Lambertian : Material
    {
        private Vector3 albedo;

        public Lambertian(Vector3 albedo)
        {
            this.albedo = albedo;
        }

        public override bool Scatter(Ray ray,HitRecord hit, out Vector3 attenuation, out Ray scattered)
        {
            Vector3 target = hit.position + hit.normal + Sphere.RandomInUnitSphere();
            scattered = new Ray(hit.position, target - hit.position);
            attenuation = albedo;

            return true;
        }
    }
}
