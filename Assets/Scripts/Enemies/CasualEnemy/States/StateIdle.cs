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
                if (MachineEnemy.DistanceToPlayerAggro > MachineEnemy.AttackRange)
                {
                    MachineEnemy.ChangeState(StateMachinePlayer.STATE_WALK);
                }
                else
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

        }

        public override void OnTriggerEnter()
        {

        }
    }
}
