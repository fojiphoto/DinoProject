using UnityEngine;

namespace NeoFPS
{
    public class ParticleSystemPlayOnEnable : MonoBehaviour
    {
        [SerializeField, Tooltip("The particle systems to play on enabling the object")]
        private ParticleSystem[] m_ParticleSystems = { };

        private void OnEnable()
        {
            for (int i = 0; i < m_ParticleSystems.Length; ++i)
            {
                // Play the particle system
                if (m_ParticleSystems[i] != null)
                    m_ParticleSystems[i].Play();
            }
        }
    }
}