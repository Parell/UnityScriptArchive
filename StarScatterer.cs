using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class StarBand
{
    [SerializeField] public float proportion, fromXZ, toXZ, fromY, toY;
}

public class StarScatterer : MonoBehaviour
{
    [SerializeField] ParticleSystem starParticleSystem;
    [SerializeField] int starCount = 1000;
    [SerializeField] StarBand[] spreads;

    private ParticleSystem.Particle[] particles;
    private byte[] alphas;
    private Color32 color32;
    private float radius, spreadSum, spreadChooser, sum, angleXZ, angleY;
    private int chosenSpread;
    private Vector3 position;

    private void Start()
    {
        alphas = new byte[starCount];
        particles = new ParticleSystem.Particle[starCount];

        starParticleSystem.Emit(starCount);
        starParticleSystem.GetParticles(particles, starCount, 0);
        radius = starParticleSystem.shape.radius;

        spreadSum = 0;
        foreach (StarBand spread in spreads)
        {
            spreadSum += spread.proportion;
        }

        for (int i = 0; i < particles.Length; i++)
        {
            alphas[i] = particles[i].startColor.a;
            spreadChooser = Random.Range(0, spreadSum);
            sum = 0;

            for (int ii = 0; ii < spreads.Length; ii++)
            {
                sum += spreads[ii].proportion;
                if(spreadChooser < sum)
                {
                    chosenSpread = ii;
                    ii = spreads.Length;
                }
            }

            angleXZ = Random.Range(Mathf.Deg2Rad * spreads[chosenSpread].fromXZ, Mathf.Deg2Rad * spreads[chosenSpread].toXZ);
            angleY = Random.Range(Mathf.Deg2Rad * spreads[chosenSpread].fromY, Mathf.Deg2Rad * spreads[chosenSpread].toY);

            position.x = radius * Mathf.Cos(angleXZ) * Mathf.Sin(angleY);
            position.z = radius * Mathf.Sin(angleXZ) * Mathf.Sin(angleY);
            position.y = radius * Mathf.Cos(angleY);

            particles[i].position = position;
        }

        starParticleSystem.SetParticles(particles, starCount);
    }

    private void Update()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            color32 = particles[i].startColor;
            color32.a = (byte)Mathf.Clamp(alphas[i] * (starParticleSystem.transform.TransformPoint(particles[i].position).y  - starParticleSystem.transform.position.y)
                / radius, 0, alphas[i]);
            particles[i].startColor = color32;
        }

        starParticleSystem.SetParticles(particles, particles.Length, 0);
    }
}
