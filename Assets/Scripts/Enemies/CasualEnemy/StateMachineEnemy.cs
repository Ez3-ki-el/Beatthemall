using System.Collections;
using System.Collections.Generic;

using Assets.Scripts.Player.States;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.EventSystems;

using static UnityEditor.Experimental.GraphView.GraphView;

namespace Assets.Scripts.Enemies.CasualEnemy
{
    public class StateMachineEnemy : MonoBehaviour
    {
        #region Properties

        [Header("Speed")]
        public float CurrentSpeed = 0f;
        public float WalkSpeed = 6f;

        public int LifePoints = 3;

        public GameObject AttackArea;

        [HideInInspector]  public Transform Player1Transform;
        [AllowsNull]
        [HideInInspector]  public Transform Player2Transform;

        public Animator Animator => GetComponentInChildren<Animator>();
        public SpriteRenderer SpriteEnemy => GetComponentInChildren<SpriteRenderer>();
        /// <summary>
        /// Détermine quel personnage l'enemey va aggro
        /// </summary>
        [HideInInspector] public Transform PlayerAggro;
        [HideInInspector] public float DistanceToPlayerAggro;

        [HideInInspector] public bool IsDead;
        [HideInInspector] public bool IsMoving => Rb2dEnemy.linearVelocity != Vector2.zero;
        [HideInInspector] public bool IsAttacking;
        [HideInInspector] public bool IsHit;
        [HideInInspector] public float AttackRange = 3f;
        [HideInInspector] public float chronoDashCooldown = 2f;
        [HideInInspector] private float chronoHit = 0f;
        [HideInInspector] public Rigidbody2D Rb2dEnemy => GetComponent<Rigidbody2D>();
        /// <summary>
        /// Détermine la durée durant laquelle le perso reste dans l'état 'Hit' après avoir pris un dégat
        /// </summary>
        public float HitDuration = 3f;
        private Coroutine hitCoroutine;

        #endregion

        #region States

        /// <summary>
        /// Liste de tous les états du player (Nom de la classe State / Objet de type State)
        /// </summary>
        private readonly Dictionary<string, State> _states = new();

        public const string STATE_IDLE = nameof(StateIdle);
        public const string STATE_WALK = nameof(StateWalk);
        public const string STATE_DEAD = nameof(StateDead);
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
            _states.Add(STATE_ATTACK, new StateAttack(this));
            _states.Add(STATE_DEAD, new StateDead(this));

            ChangeState(nameof(StateIdle));

            Player1Transform = GameObject.Find("Player1").transform;
            Player2Transform = GameObject.Find("Player2").transform;
        }

        // Update is called once per frame
        private void Update()
        {
            CalculAggro();
            HitOrNotHit();
            currentState?.OnUpdate();
        }

        private void FixedUpdate()
        {
            currentState?.OnFixedUpdate();
        }

        private IEnumerator CoroutineHit()
        {
            bool isRed = false;

            while (chronoHit < HitDuration)
            {
                SpriteEnemy.color = isRed ? Color.white : Color.red;
                isRed = !isRed;
                yield return new WaitForSeconds(0.2f);  // Temps de clignotement
            }

            SpriteEnemy.color = Color.white;
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
                    SpriteEnemy.color = Color.white;
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
            if (collision.CompareTag("Player1Attack") || collision.CompareTag("Player2Attack"))
            {
                LifePoints--;

                if (LifePoints <= 0)
                {
                    IsDead = true;
                    Debug.Log("dead by " + collision.tag);
                }
                else
                {
                    IsHit = true;
                    Debug.Log("hit by " + collision.tag);
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

        private void CalculAggro()
        {
            if (Player2Transform != null) // Cas du multi joueur
            {
                // Récupération des distances entre l'enemy et les players
                float distanceP1 = Vector2.Distance(transform.position, Player1Transform.position);
                float distanceP2 = Vector2.Distance(transform.position, Player2Transform.position);

                // On choisi la distance la plus courte pour l'aggro
                if (distanceP1 <= distanceP2)
                {
                    DistanceToPlayerAggro = distanceP1;
                    PlayerAggro = Player1Transform;
                }
                else
                {
                    DistanceToPlayerAggro = distanceP2;
                    PlayerAggro = Player2Transform;
                }
            }
            else
            {
                DistanceToPlayerAggro = Vector2.Distance(transform.position, Player1Transform.position);
                PlayerAggro = Player1Transform;
            }
        }



        #endregion
    }
}
