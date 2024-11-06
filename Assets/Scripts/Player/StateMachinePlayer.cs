using System.Collections.Generic;

using Assets.Scripts.Player.States;

using UnityEditor;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
    public class StateMachinePlayer : MonoBehaviour
    {

        #region Unity Editor Properties
        [Header("Speeds")]
        public float currentSpeed = 0f;
        public float walkSpeed = 3f;
        public float dashSpeed = 6f;

        [Header("Times")]
        public float dashDuration = 0.2f;
        public float dashCooldown = 2f;
        // Il faut que la valeur d'init de chronoDashCooldown soit la même que dashCooldown pour éviter d'avoir un cooldown au lancement du jeu
        [HideInInspector] public float chronoDashCooldown = 2f;

        /// <summary>
        /// Représente le collider d'attack
        /// </summary>
        public GameObject AttackArea;

        public int LifePoints = 3;
        #endregion

        #region Properties 

        /// <summary>
        /// Liste de tous les états du player (Nom de la classe State / Objet de type State)
        /// </summary>
        private readonly Dictionary<string, State> _states = new();

        public const string STATE_IDLE = nameof(StateIdle);
        public const string STATE_WALK = nameof(StateWalk);
        public const string STATE_DEAD = nameof(StateDead);
        public const string STATE_HIT = nameof(StateHit);
        public const string STATE_ATTACK = nameof(StateAttack);
        public const string STATE_DASH = nameof(StateDash);

        /// <summary>
        /// Le State actuel du joueur
        /// </summary>
        public State currentState;

        [HideInInspector] public Rigidbody2D Rb2dPlayer => GetComponent<Rigidbody2D>();
        [HideInInspector] public Vector2 MoveDirection;
        [HideInInspector] public bool IsMoving => MoveDirection != Vector2.zero;
        [HideInInspector] public bool IsAttacking;
        [HideInInspector] public bool IsDead;
        [HideInInspector] public bool IsHit;

        [HideInInspector]public bool DashPressed;
        [HideInInspector]public bool DashAvailable;
        [HideInInspector] public bool CanDash => DashPressed && DashAvailable;
        #endregion

        #region MonoBehaviour 

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            _states.Add(STATE_IDLE, new StateIdle(this));
            _states.Add(STATE_WALK, new StateWalk(this));
            _states.Add(STATE_HIT, new StateHit(this));
            _states.Add(STATE_ATTACK, new StateAttack(this));
            _states.Add(STATE_DEAD, new StateDead(this));
            _states.Add(STATE_DASH, new StateDash(this));

            ChangeState(nameof(StateIdle));
        }

        // Update is called once per frame
        private void Update()
        {
            currentState?.OnUpdate();
        }

        private void FixedUpdate()
        {
            currentState?.OnFixedUpdate();

            Rb2dPlayer.linearVelocity = MoveDirection * currentSpeed;

            // Gestion de la rotation du player
            RotatePlayer();

            chronoDashCooldown += Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("EnemyAttack"))
            {
                LifePoints--;

                if (LifePoints <= 0)
                {
                    IsDead = true;
                }
                else
                {
                    IsHit = true;
                }
            }
        }

        #endregion

        #region Methods

        public void ChangeState(string stateName)
        {
            currentState?.OnExit();
            currentState = _states[stateName];
            currentState.OnEnter();
        }

        private void RotatePlayer()
        {
            // Gestion de la rotation X du perso dans ses déplacements
            if (MoveDirection.x < 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (MoveDirection.x > 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
        #endregion

        #region Events

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                IsAttacking = true;
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                IsAttacking = false;
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                MoveDirection = context.ReadValue<Vector2>();
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                MoveDirection = Vector2.zero;
            }
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                if (chronoDashCooldown > dashCooldown)
                {
                    DashPressed = true;
                    chronoDashCooldown = 0f;
                }
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                DashPressed = false;
            }
        }

        #endregion

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            Gizmos.color = Color.red;

            if (currentState != null)
                Handles.Label(new Vector2(-1, 1), currentState.ToString());

            Handles.Label(new Vector2(-1, 5), Rb2dPlayer.linearVelocity.ToString());
            Handles.Label(new Vector2(-1, 4), "DashPressed " + DashPressed.ToString());
            Handles.Label(new Vector2(-1, 3), "DashAvailable " + DashAvailable.ToString());
           
#endif
        }
    }
}