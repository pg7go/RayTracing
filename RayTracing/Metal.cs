using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RayTracing
{
    public class Metal : Material
    {
        private Vector3 albedo;
        private float fuzz;

        public Metal(Vector3 albedo,float fuzz)
        {
            this.albedo = albedo;
            this.fuzz = fuzz < 1 ? fuzz : 1;
        }

        public override bool Scatter(Ray ray,HitRecord hit, out Vector3 attenuation, out Ray scattered)
        {
            var direction = Vector3.Normalize(ray.direction);
            Vector3 reflected = Vector3.Reflect(direction, hit.normal);
            scattered = new Ray(hit.position, reflected + fuzz * Sphere.RandomInUnitSphere());
            attenuation = albedo;

            //return true;
            return (Vector3.Dot(scattered.direction, hit.normal) > 0);
        }
    }
}
