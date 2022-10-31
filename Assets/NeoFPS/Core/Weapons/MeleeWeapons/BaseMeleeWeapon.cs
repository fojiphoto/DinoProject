using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NeoFPS.Constants;
using NeoSaveGames.Serialization;
using NeoSaveGames;
using UnityEngine.Serialization;

namespace NeoFPS
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class BaseMeleeWeapon : MonoBehaviour, IMeleeWeapon, IWieldable, IDamageSource, ICrosshairDriver, IPoseHandler, INeoSerializableComponent
    {
        [SerializeField, NeoObjectInHierarchyField(true), Tooltip("The animator component of the weapon.")]
        private Animator m_Animator = null;

        [SerializeField, Tooltip("The crosshair to show when the weapon is drawn.")]
        private FpsCrosshair m_Crosshair = FpsCrosshair.Default;

        [Header("Base Wieldable")]

        [SerializeField, AnimatorParameterKey("m_Animator", AnimatorControllerParameterType.Trigger), Tooltip("The animation trigger for the raise animation.")]
        private string m_TriggerDraw = "Draw";

        [SerializeField, Tooltip("The time taken to lower the item on deselection.")]
        private float m_RaiseDuration = 0.5f;

        [SerializeField, AnimatorParameterKey("m_Animator", AnimatorControllerParameterType.Trigger), Tooltip("The trigger for the weapon lower animation (blank = no animation).")]
        private string m_TriggerLower = string.Empty;

        [SerializeField, Tooltip("The time taken to lower the item on deselection.")]
        private float m_LowerDuration = 0f;

        [SerializeField, Tooltip("The audio clip when raising the weapon.")]
        private AudioClip m_AudioSelect = null;

        private int m_AnimHashDraw = 0;
        private int m_AnimHashLower = 0;

        private DeselectionWaitable m_DeselectionWaitable = null;
        private ICharacter m_Wielder = null;
        private float m_SelectionTimer;

        public event UnityAction<bool> onAttackingChange;
        public event UnityAction<bool> onBlockStateChange;
        public event UnityAction<ICharacter> onWielderChanged;

        public class DeselectionWaitable : Waitable
        {
            private float m_Duration = 0f;
            private float m_StartTime = 0f;

            public DeselectionWaitable(float duration)
            {
                m_Duration = duration;
            }

            public void ResetTimer()
            {
                m_StartTime = Time.time;
            }

            protected override bool CheckComplete()
            {
                return (Time.time - m_StartTime) > m_Duration;
            }
        }

        protected Animator animator
        {
            get { return m_Animator; }
        }

        protected AudioSource audioSource
        {
            get;
            private set;
        }

        protected bool isSelecting
        {
            get { return m_SelectionTimer > 0f; }
        }

        public ICharacter wielder
        {
            get { return m_Wielder; }
            private set
            {
                if (m_Wielder != value)
                {
                    m_Wielder = value;
                    if (onWielderChanged != null)
                        onWielderChanged(m_Wielder);
                }
            }
        }

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if (m_Animator == null)
                m_Animator = GetComponentInChildren<Animator>();

            m_RaiseDuration = Mathf.Clamp(m_RaiseDuration, 0f, 5f);
            m_LowerDuration = Mathf.Clamp(m_LowerDuration, 0f, 5f);
        }
#endif

        protected virtual void Awake()
        {
            m_AnimHashDraw = Animator.StringToHash(m_TriggerDraw);
            m_AnimHashLower = Animator.StringToHash(m_TriggerLower);

            // Get the audio source
            audioSource = GetComponent<AudioSource>();

            // Set up deselection waitable
            if (m_LowerDuration > 0.001f)
                m_DeselectionWaitable = new DeselectionWaitable(m_LowerDuration);

            // Set up pose handler
            m_PoseHandler = new PoseHandler(transform, Vector3.zero, Quaternion.identity);
        }

        protected virtual void Start()
        {
            if (wielder == null)
                Destroy(gameObject);
        }

        protected virtual void OnEnable()
        {
            wielder = GetComponentInParent<ICharacter>();

            // Play draw audio
            if (m_AudioSelect != null)
                audioSource.PlayOneShot(m_AudioSelect);

            // Trigger draw animation
            if (m_AnimHashDraw != -1 && m_Animator != null)
            {
                m_Animator.SetTrigger(m_AnimHashDraw);

                // Start cooldown to prevent input until raised
                m_SelectionTimer = m_RaiseDuration;
            }
        }

        protected virtual void OnDisable()
        {
            blocking = false;

            // Reset pose
            m_PoseHandler.OnDisable();
        }

        protected virtual void FixedUpdate()
        {
            m_SelectionTimer -= Time.deltaTime;
            if (m_SelectionTimer < 0f)
                m_SelectionTimer = 0f;
        }

        public abstract void PrimaryPress();
        public abstract void PrimaryRelease();
        public abstract void SecondaryPress();
        public abstract void SecondaryRelease();

        public void Select()
        {
            // Play lower animation
            if (m_AnimHashDraw != -1 && m_Animator != null)
                m_Animator.SetTrigger(m_AnimHashDraw);
        }

        public void DeselectInstant()
        { }

        public Waitable Deselect()
        {
            // Play lower animation
            if (m_AnimHashLower != 0 && m_Animator != null)
                m_Animator.SetTrigger(m_AnimHashLower);

            // Wait for deselection
            if (m_DeselectionWaitable != null)
                m_DeselectionWaitable.ResetTimer();

            return m_DeselectionWaitable;
        }

        private bool m_Attacking = false;
        public bool attacking
        {
            get { return m_Attacking; }
            protected set
            {
                if (m_Attacking != value)
                {
                    m_Attacking = value; 
                    OnAttackingChange(m_Attacking);
                }
            }
        }

        private bool m_Blocking = false;
        public bool blocking
        {
            get { return m_Blocking; }
            protected set
            {
                if (m_Blocking != value)
                {
                    m_Blocking = value;
                    OnBlockStateChange(m_Blocking);
                }
            }
        }

        protected virtual void OnAttackingChange(bool to)
        {
            if (onAttackingChange != null)
                onAttackingChange(m_Attacking);
        }

        protected virtual void OnBlockStateChange(bool to)
        {
            if (onBlockStateChange != null)
                onBlockStateChange(m_Blocking);
        }

        #region POSE

        private PoseHandler m_PoseHandler = null;

        public void SetPose(Vector3 position, Quaternion rotation, float duration)
        {
            m_PoseHandler.SetPose(position, rotation, duration);
        }

        public void SetPose(Vector3 position, CustomPositionInterpolation posInterp, Quaternion rotation, CustomRotationInterpolation rotInterp, float duration)
        {
            m_PoseHandler.SetPose(position, posInterp, rotation, rotInterp, duration);
        }

        public void ResetPose(float duration)
        {
            m_PoseHandler.ResetPose(duration);
        }

        public void ResetPose(CustomPositionInterpolation posInterp, CustomRotationInterpolation rotInterp, float duration)
        {
            m_PoseHandler.ResetPose(posInterp, rotInterp, duration);
        }

        void Update()
        {
            m_PoseHandler.UpdatePose();
        }

        #endregion

        #region IDamageSource implementation

        private DamageFilter m_OutDamageFilter = DamageFilter.AllDamageAllTeams;
        public DamageFilter outDamageFilter
        {
            get
            {
                return m_OutDamageFilter;
            }
            set
            {
                m_OutDamageFilter = value;
            }
        }

        public IController controller
        {
            get
            {
                if (wielder != null)
                    return wielder.controller;
                else
                    return null;
            }
        }

        public Transform damageSourceTransform
        {
            get
            {
                return transform;
            }
        }

        public string description
        {
            get
            {
                return name;
            }
        }

        #endregion

        #region ICrosshairDriver IMPLEMENTATION

        private bool m_HideCrosshair = false;

        public FpsCrosshair crosshair
        {
            get { return m_Crosshair; }
            protected set
            {
                m_Crosshair = value;
                if (onCrosshairChanged != null)
                    onCrosshairChanged(m_Crosshair);
            }
        }

        private float m_Accuracy = 1f;
        public float accuracy
        {
            get { return m_Accuracy; }
            protected set
            {
                m_Accuracy = value;
                if (onAccuracyChanged != null)
                    onAccuracyChanged(m_Accuracy);
            }
        }

        public event UnityAction<FpsCrosshair> onCrosshairChanged;
        public event UnityAction<float> onAccuracyChanged;

        public void HideCrosshair()
        {
            if (!m_HideCrosshair)
            {
                bool triggerEvent = (onCrosshairChanged != null && crosshair == FpsCrosshair.None);

                m_HideCrosshair = true;

                if (triggerEvent)
                    onCrosshairChanged(FpsCrosshair.None);
            }
        }

        public void ShowCrosshair()
        {
            if (m_HideCrosshair)
            {
                // Reset
                m_HideCrosshair = false;

                // Fire event
                if (onCrosshairChanged != null && crosshair != FpsCrosshair.None)
                    onCrosshairChanged(crosshair);
            }
        }

        #endregion

        #region INeoSerializableComponent IMPLEMENTATION

        private static readonly NeoSerializationKey k_BlockingKey = new NeoSerializationKey("blocking");
        private static readonly NeoSerializationKey k_AttackingKey = new NeoSerializationKey("attacking");
        private static readonly NeoSerializationKey k_AccuracyKey = new NeoSerializationKey("accuracy");

        public virtual void WriteProperties(INeoSerializer writer, NeoSerializedGameObject nsgo, SaveMode saveMode)
        {
            // Write properties if relevant
            if (saveMode == SaveMode.Default)
            {
                writer.WriteValue(k_BlockingKey, blocking);
                writer.WriteValue(k_AttackingKey, blocking);
                writer.WriteValue(k_AccuracyKey, accuracy);
            }
        }

        public virtual void ReadProperties(INeoDeserializer reader, NeoSerializedGameObject nsgo)
        {
            // Read properties
            float floatResult;
            if (reader.TryReadValue(k_AccuracyKey, out floatResult, 1f))
                accuracy = floatResult;

            bool boolResult;
            if (reader.TryReadValue(k_BlockingKey, out boolResult, false))
                blocking = boolResult;
            if (reader.TryReadValue(k_AttackingKey, out boolResult, false))
                attacking = boolResult;
        }

        #endregion
    }
}