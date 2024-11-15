using System.Collections;
using System.Collections.Generic;

using Assets.Scripts.Player.States;

using UnityEditor;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Assets.Scripts.ScriptableObjects;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class StateMachinePlayer : MonoBehaviour
    {

        #region Unity Editor Properties
        [Header("Player")]
        //public PlayerLifePoints.Player player;

        [Header("Speeds")]
        public float currentSpeed = 0f;
        public float walkSpeed = 3f;
        public float dashSpeed = 6f;
        public float multiplier = 1f;

        [Header("Times")]
        public float dashDuration = 0.2f;
        public float dashCooldown = 2f;
        public float UltiDuration = 3f;

        // Il faut que la valeur d'init de chronoDashCooldown soit la même que dashCooldown pour éviter d'avoir un cooldown au lancement du jeu
        [HideInInspector] public float chronoDashCooldown = 2f;
        [HideInInspector] private float chronoHit = 0f;
        /// <summary>
        /// Représente le collider d'attack
        /// </summary>
        [HideInInspector] public GameObject AttackArea;
        [HideInInspector] public GameObject AttackAreaUlti;

        [Header("Life")]
        public float LifePoints
        {
            get
            {
                return playerPoints.LifePoints;
            }
            set
            {
                playerPoints.LifePoints = value;

                if (playerPoints.LifePoints > playerPoints.MaxLifePoints)
                    playerPoints.LifePoints = playerPoints.MaxLifePoints;
            }
        }

        [Header("Ulti")]
        public float UltiPoints => playerPoints.UltiPoints;
        public float MaxUltiPoints => playerPoints.MaxUltiPoints;

        /// <summary>
        /// Détermine la durée durant laquelle le perso reste dans l'état 'Hit' après avoir pris un dégat
        /// </summary>
        public float HitDuration = 3f;

        public SpriteRenderer SpritePlayer => GetComponentInChildren<SpriteRenderer>();
        public Animator Animator => GetComponentInChildren<Animator>();
        [HideInInspector] public GameObject Dash;
        public PlayerPoints playerPoints;


        #endregion

        #region Properties 

        private Coroutine hitCoroutine;
        /// <summary>
        /// Liste de tous les états du player (Nom de la classe State / Objet de type State)
        /// </summary>
        private readonly Dictionary<string, State> _states = new();

        public const string STATE_IDLE = nameof(StateIdle);
        public const string STATE_WALK = nameof(StateWalk);
        public const string STATE_DEAD = nameof(StateDead);
        public const string STATE_ATTACK = nameof(StateAttack);
        public const string STATE_DASH = nameof(StateDash);
        public const string STATE_ULTI = nameof(StateUlti);

        /// <summary>
        /// Le State actuel du joueur
        /// </summary>
        public State currentState;

        [HideInInspector] public Rigidbody2D Rb2dPlayer => GetComponent<Rigidbody2D>();
        [HideInInspector] public Vector2 MoveDirection;
        [HideInInspector] public bool IsMoving => MoveDirection != Vector2.zero;
        [HideInInspector] public bool IsAttacking;
        [HideInInspector] public bool IsDead;
        [HideInInspector] public bool IsUlting;
        public bool IsHit;

        [HideInInspector] public bool DashPressed;
        [HideInInspector] public bool DashAvailable;
        [HideInInspector] public bool CanDash => DashPressed && DashAvailable;
        #endregion

        #region MonoBehaviour 

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            _states.Add(STATE_IDLE, new StateIdle(this));
            _states.Add(STATE_WALK, new StateWalk(this));
            _states.Add(STATE_ATTACK, new StateAttack(this));
            _states.Add(STATE_DEAD, new StateDead(this));
            _states.Add(STATE_DASH, new StateDash(this));
            _states.Add(STATE_ULTI, new StateUlti(this));

            ChangeState(nameof(StateIdle));

            AttackArea = transform.Find("Attack").gameObject;
            AttackAreaUlti = transform.Find("AttackUlti").gameObject;
            Dash = transform.Find("Dash").gameObject;
            LifePoints = playerPoints.MaxLifePoints;
        }

        // Update is called once per frame
        private void Update()
        {
            currentState?.OnUpdate();
        }

        private void FixedUpdate()
        {
            currentState?.OnFixedUpdate();
            Rb2dPlayer.linearVelocity = currentSpeed * multiplier * MoveDirection;

            // Animations
            Animator.SetBool("IsAttacking", IsAttacking);

            // Gestion de la rotation du player
            RotatePlayer();

            chronoDashCooldown += Time.deltaTime;

            HitOrNotHit();


        }

        private IEnumerator CoroutineHit()
        {
            bool isRed = false;

            while (chronoHit < HitDuration)
            {
                SpritePlayer.color = isRed ? Color.white : Color.red;
                isRed = !isRed;
                yield return new WaitForSeconds(0.2f);  // Temps de clignotement
            }

            SpritePlayer.color = Color.white;
            hitCoroutine = null;
        }

        private void HitOrNotHit()
        {
            // Gestion du hit
            if (IsHit)
            {
                if (chronoHit > HitDuration)
                {
                    IsHit = false;
                    SpritePlayer.color = Color.white;
                    chronoHit = 0f;
                }
                else
                {
                    chronoHit += Time.deltaTime;
                    hitCoroutine ??= StartCoroutine(CoroutineHit());
                }
            }
            else if (hitCoroutine != null)
            {
                StopCoroutine(hitCoroutine);
                hitCoroutine = null;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("EnemyAttack"))
            {
                if (!IsUlting) // S'il fait son ulti il est invincible
                {
                    if (!IsHit)
                    {
                        LifePoints--;
                        IsDead = playerPoints.LifePoints <= 0;
                        IsHit = playerPoints.LifePoints > 0;
                    }
                }
            }
            // Même si le code AoeAttack est similaire à EnemyAttack, je laisse son if pour si on veut un étourdissement dû à l'aoe
            else if (collision.CompareTag("AoeAttack"))
            {
                if (!IsUlting) // S'il fait son ulti il est invincible
                {
                    if (!IsHit)
                    {
                        LifePoints--;
                        IsDead = playerPoints.LifePoints <= 0;
                        IsHit = playerPoints.LifePoints > 0;
                    }
                }
            }
            else if (collision.CompareTag("RedCan"))
            {
                LifePoints += 2;
            }
            else if (collision.CompareTag("BlueCan"))
            {
                playerPoints.UltiPoints = MaxUltiPoints;
            }
            else if (collision.CompareTag("GreenCan"))
            {
                StartCoroutine(Speedy());
            }
        }

        private IEnumerator Speedy()
        {
            multiplier = 2;
            yield return new WaitForSeconds(10f);
            multiplier = 1;
        }

        #endregion

        #region Methods

        public void ChangeState(string stateName)
        {
            Debug.Log("Changement de state pour : " + stateName);
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
            if (context.phase == InputActionPhase.Started) // Input en mode "tap"
            {
                IsAttacking = true;
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                IsAttacking = false;
            }
        }

        public void OnUlti(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started) // Input en mode "tap"
            {
                if (playerPoints.UltiPoints >= playerPoints.MaxUltiPoints)
                {
                    IsUlting = true;
                }
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                IsUlting = false;
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


#endif
        }
    }
}