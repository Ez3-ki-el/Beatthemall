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
            Vector2 direction = (MachineEnemy.PlayerAggro.position - MachineEnemy.transform.position).normalized;
            MachineEnemy.Rb2dEnemy.linearVelocity = direction * MachineEnemy.CurrentSpeed;
        }

        public override void OnTriggerEnter()
        {

        }
    }
}
