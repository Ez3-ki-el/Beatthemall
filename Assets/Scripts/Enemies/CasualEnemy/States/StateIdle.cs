using Assets.Scripts.Player;
using Assets.Scripts.Player.States;

using UnityEngine;

namespace Assets.Scripts.Enemies.CasualEnemy
{
    public class StateIdle : State
    {
        public StateIdle(StateMachineEnemy enemy) : base(enemy) { }

        public override void OnEnter()
        {
            MachineEnemy.CurrentSpeed = 0f;
        }

        public override void OnUpdate()
        {
            if (MachineEnemy.IsDead)
            {
                MachineEnemy.ChangeState(StateMachinePlayer.STATE_DEAD);
            }
            else
            {
                if (MachineEnemy.DistanceToPlayer > MachineEnemy.AttackRange)
                {
                    MachineEnemy.ChangeState(StateMachinePlayer.STATE_WALK);
                }
                else
                {
                    MachineEnemy.ChangeState(StateMachinePlayer.STATE_ATTACK);
                }
                //if (MachineEnemy.IsMoving)
                //{
                //    MachineEnemy.ChangeState(StateMachinePlayer.STATE_WALK);
                //}
                //else if (MachineEnemy.IsAttacking)
                //{
                //    MachineEnemy.ChangeState(StateMachinePlayer.STATE_ATTACK);
                //}
                //else if (MachineEnemy.IsHit)
                //{
                //    MachineEnemy.ChangeState(StateMachinePlayer.STATE_HIT);
                //}
            }
        }

        public override void OnExit()
        {

        }

        public override void OnFixedUpdate()
        {

        }

        public override void OnTriggerEnter()
        {

        }
    }
}
