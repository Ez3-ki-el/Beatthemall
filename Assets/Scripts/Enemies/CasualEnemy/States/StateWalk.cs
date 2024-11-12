using Assets.Scripts.Player;
using Assets.Scripts.Player.States;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.EventSystems;

using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Assets.Scripts.Enemies.CasualEnemy
{
    public class StateWalk : State
    {
        private Vector2 direction;
        public StateWalk(StateMachineEnemy enemy) : base(enemy) { }

        public override void OnEnter()
        {
            MachineEnemy.CurrentSpeed = MachineEnemy.WalkSpeed;
        }

        public override void OnUpdate()
        {
            if (MachineEnemy.IsDead)
            {
                MachineEnemy.ChangeState(StateMachinePlayer.STATE_DEAD);
            }
            else
            {
                if (!MachineEnemy.IsMoving)
                {
                    MachineEnemy.ChangeState(StateMachinePlayer.STATE_IDLE);
                }
                else if (MachineEnemy.DistanceToPlayerAggro <= MachineEnemy.AttackRange)
                {
                    MachineEnemy.ChangeState(StateMachinePlayer.STATE_ATTACK);
                }
            }
        }

        public override void OnExit()
        {

        }

        public override void OnFixedUpdate()
        {
            // Se déplacer vers le joueur
            direction = (MachineEnemy.PlayerAggro.position - MachineEnemy.transform.position).normalized;
            MachineEnemy.Rb2dEnemy.linearVelocity = direction * MachineEnemy.CurrentSpeed;
            RotateEnemy();
        }

        public override void OnTriggerEnter()
        {

        }


        private void RotateEnemy()
        {
            // Gestion de la rotation X du perso dans ses déplacements
            if (direction.x < 0 && MachineEnemy.transform.localScale.x > 0)
            {
                MachineEnemy.transform.localScale = new Vector3(-Mathf.Abs(MachineEnemy.transform.localScale.x), MachineEnemy.transform.localScale.y, MachineEnemy.transform.localScale.z);
            }
            else if (direction.x > 0 && MachineEnemy.transform.localScale.x < 0)
            {
                MachineEnemy.transform.localScale = new Vector3(Mathf.Abs(MachineEnemy.transform.localScale.x), MachineEnemy.transform.localScale.y, MachineEnemy.transform.localScale.z);
            }
        }
    }
}
