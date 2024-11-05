using System.Collections.Generic;

using Assets.Scripts.Player.States;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
    public class StateMachinePlayer : MonoBehaviour
    {
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
            currentState.OnUpdate();
        }

        private void FixedUpdate()
        {
            currentState.OnFixedUpdate();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {

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

        #region Events

        public void OnAttack(InputAction.CallbackContext context)
        {
        }

        public void OnMove(InputAction.CallbackContext context)
        {
        }

        #endregion
    }
}