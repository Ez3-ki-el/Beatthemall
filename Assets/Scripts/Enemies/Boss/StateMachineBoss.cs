using System.Collections.Generic;
using System.Collections;

using Assets.Scripts.Player;

using UnityEditor;

using UnityEngine;

namespace Assets.Scripts.Enemies.Boss
{
    public class StateMachineBoss : MonoBehaviour
    {
        #region Properties

        [Header("Speed")]
        public float CurrentSpeed = 0f;
        public float WalkSpeed = 2f;


        public int LifePoints = 3;

        public GameObject AttackArea;
        public GameObject AttackAreaAOE;

        [HideInInspector] public GameObject Player1;
        [HideInInspector] public GameObject Player2;

        public float MinRangeCooldownAoe = 3f;
        public float MaxRangeCooldownAoe = 6f;


        private float CooldownAOE;
        private float ChronoAOE = 0;
        /// <summary>
        /// Détermine la durée de l'AOE
        /// </summary>
        public float DurationAOE = 1f;

        [HideInInspector] public StateMachinePlayer MachinePlayer1;
        [HideInInspector] public StateMachinePlayer MachinePlayer2;


        /// <summary>
        /// Détermine quel personnage l'enemey va aggro
        /// </summary>
        [HideInInspector] public Transform PlayerAggro;
        [HideInInspector] public float DistanceToPlayerAggro;

        [HideInInspector] public bool IsDead;
        [HideInInspector] public bool IsMoving => Rb2dEnemy.linearVelocity != Vector2.zero;
        [HideInInspector] public bool IsAttacking;
        [HideInInspector] public bool IsAttackingAOE;
        [HideInInspector] public bool IsHit;
        [HideInInspector] public float AttackRange = 1.3f;
        [HideInInspector] private float chronoHit = 0f;
        [HideInInspector] public Rigidbody2D Rb2dEnemy => GetComponent<Rigidbody2D>();

        public Animator Animator => GetComponentInChildren<Animator>();
        public SpriteRenderer SpriteBoss => GetComponentInChildren<SpriteRenderer>();


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
        public const string STATE_ATTACK_AOE = nameof(StateAttackAOE);

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
            _states.Add(STATE_ATTACK_AOE, new StateAttackAOE(this));
            _states.Add(STATE_DEAD, new StateDead(this));

            ChangeState(nameof(StateIdle));
            CooldownAOE = Random.Range(MinRangeCooldownAoe, MaxRangeCooldownAoe);

            Player1 = GameObject.Find("Player1");
            Player2 = GameObject.Find("Player2");

            MachinePlayer1 = Player1.GetComponent<StateMachinePlayer>();

            if (Player2 != null)
                MachinePlayer2 = Player2.GetComponent<StateMachinePlayer>();

        }

        // Update is called once per frame
        private void Update()
        {
            CalculAggro();

            if (ChronoAOE >= CooldownAOE)
            {
                IsAttackingAOE = true;
                ChronoAOE = 0f;
                CooldownAOE = Random.Range(MinRangeCooldownAoe, MaxRangeCooldownAoe);
            }
            else
            {
                ChronoAOE += Time.deltaTime;
            }

            currentState?.OnUpdate();
        }


        private IEnumerator CoroutineHit()
        {
            bool isRed = false;

            while (chronoHit < HitDuration)
            {
                SpriteBoss.color = isRed ? Color.white : Color.red;
                isRed = !isRed;
                yield return new WaitForSeconds(0.2f);  // Temps de clignotement
            }

            SpriteBoss.color = Color.white;
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
                    SpriteBoss.color = Color.white;
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


        private void FixedUpdate()
        {
            currentState?.OnFixedUpdate();
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
            if (Player2 != null)
            {
                // Le boss aggro le joueur avec le moins de PV
                PlayerAggro = MachinePlayer1.LifePoints <= MachinePlayer2.LifePoints ? Player1.transform : Player2.transform;
            }
            else
            { // Sinon on prend player1
                PlayerAggro = Player1.transform;
            }

            DistanceToPlayerAggro = Vector2.Distance(transform.position, PlayerAggro.transform.position);
        }

        #endregion

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            Gizmos.color = Color.red;

            if (currentState != null)
                Handles.Label(new Vector2(-1, 2), currentState.ToString());


#endif
        }
    }
}
