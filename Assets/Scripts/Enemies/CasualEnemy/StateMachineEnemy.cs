using System.Collections.Generic;

using Assets.Scripts.Player.States;

using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Enemies.CasualEnemy
{
    public class StateMachineEnemy : MonoBehaviour
    {
        #region Properties

        [Header("Speed")]
        public float CurrentSpeed = 0f;
        public float WalkSpeed = 6f;

        public GameObject Player;
        public Transform PlayerTransform => Player?.transform;

        [HideInInspector] public bool IsDead;
        [HideInInspector] public bool IsMoving;
        [HideInInspector] public bool IsAttacking;
        [HideInInspector] public bool IsHit;
        [HideInInspector] public float AttackRange = 3f;

        [HideInInspector] public Rigidbody2D Rb2dEnemy => GetComponent<Rigidbody2D>();
        [HideInInspector] public float DistanceToPlayer;


        #endregion

        #region States

        /// <summary>
        /// Liste de tous les états du player (Nom de la classe State / Objet de type State)
        /// </summary>
        private readonly Dictionary<string, State> _states = new();

        public const string STATE_IDLE = nameof(StateIdle);
        public const string STATE_WALK = nameof(StateWalk);
        public const string STATE_DEAD = nameof(StateDead);
        public const string STATE_HIT = nameof(StateHit);
        public const string STATE_ATTACK = nameof(StateAttack);

        /// <summary>
        /// Le State actuel de l'enemy
        /// </summary>
        public State currentState;

        #endregion

        #region Unity Monobehaviour

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _states.Add(STATE_IDLE, new StateIdle(this));
            _states.Add(STATE_WALK, new StateWalk(this));
            _states.Add(STATE_HIT, new StateHit(this));
            _states.Add(STATE_ATTACK, new StateAttack(this));
            _states.Add(STATE_DEAD, new StateDead(this));

            ChangeState(nameof(StateIdle));
        }

        // Update is called once per frame
        private void Update()
        {
            DistanceToPlayer = Vector2.Distance(transform.position, Player.transform.position);
            currentState?.OnUpdate();
        }

        private void FixedUpdate()
        {
            currentState?.OnFixedUpdate();
        }

        #endregion

        #region Methods

        public void ChangeState(string stateName)
        {
            currentState?.OnExit();
            currentState = _states[stateName];
            currentState.OnEnter();
        }

        #endregion
    }
}
