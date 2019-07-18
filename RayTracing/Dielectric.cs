using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RayTracing
{
    public class Dielectric : Material
    {
        private float refIndex;

        public Dielectric(float refIndex)
        {
            this.refIndex = refIndex;
            //this.ri_out = ri_out;
        }

        protected bool Refract(ref Vector3 view, ref Vector3 normal, float ni_over_nt, out Vector3 refracted)
        {
            Vector3 uv = Vector3.Normalize(view);
            float dt = Vector3.Dot(uv, normal);
            float discriminant = 1f - ni_over_nt * ni_over_nt * (1f - dt * dt);

            if (discriminant > 0)
            {
                refracted = ni_over_nt * (uv - normal * dt) - normal * MathF.Sqrt(discriminant);
                return true;
            }
            else
            {
                refracted = Vector3.Zero;
                return false;
            }
        }

        static Random random = new Random();
        public override bool Scatter( Ray ray,  HitRecord rec, out Vector3 attenuation, out Ray scattered)
        {
            Vector3 outward_normal;
            float ni_over_nt;
            attenuation = Vector3.One;
            float reflect_prob;
            float cosine;

            if (Vector3.Dot(ray.direction, rec.normal) > 0)//内部射出
            {
                outward_normal = -rec.normal;
                ni_over_nt = refIndex;
                cosine = refIndex * Vector3.Dot(ray.direction, rec.normal) / ray.direction.Length();
            }
            else //外部射入
            {

                outward_normal = rec.normal;
                ni_over_nt = 1.0f/refIndex;
                //ni_over_nt = this.ri_out / this.ri_in;
                cosine = -Vector3.Dot(ray.direction, rec.normal) / ray.direction.Length();
            }

            if (Refract(ref ray.direction, ref outward_normal, ni_over_nt, out Vector3 refracted))
            {
                reflect_prob = Schlick(cosine, refIndex);
            }
            else
            {
                //scattered = new Ray(rec.position, Vector3.Reflect(ray.direction, rec.normal));
                reflect_prob = 1f;
            }

            if (random.NextDouble() < reflect_prob)
            {
                scattered = new Ray(rec.position, Vector3.Reflect(ray.direction, rec.normal));
            }
            else
            {
                scattered = new Ray(rec.position, refracted);
            }

            return true;
        }

        private float Schlick(float cosine, float refIndex)
        {
            float r0 = (1f - refIndex) / (1f + refIndex);
            r0 *= r0;
            return r0 + (1f - r0) * MathF.Pow(1f - cosine, 5f);
        }
    }
}
