using Assets.Scripts.Player;
using Assets.Scripts.Player.States;

using Unity.VisualScripting;

using UnityEngine;

using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Assets.Scripts.Enemies.CasualEnemy
{
    public class StateWalk : State
    {
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
                else if (MachineEnemy.IsAttacking)
                {
                    MachineEnemy.ChangeState(StateMachinePlayer.STATE_ATTACK);
                }
                else if (MachineEnemy.IsHit)
                {
                    MachineEnemy.ChangeState(StateMachinePlayer.STATE_HIT);
                }
            }
        }

        public override void OnExit()
        {

        }

        public override void OnFixedUpdate()
        {
            // Se déplacer vers le joueur
            Vector2 direction = (MachineEnemy.PlayerTransform.position - MachineEnemy.transform.position).normalized;
            MachineEnemy.Rb2dEnemy.linearVelocity = direction * MachineEnemy.CurrentSpeed;
        }

        public override void OnTriggerEnter()
        {

        }
    }
}
