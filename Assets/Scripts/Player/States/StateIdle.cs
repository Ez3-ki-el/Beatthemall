using Assets.Scripts.Player;
using Assets.Scripts.Player.States;

using UnityEngine;

namespace Assets.Scripts.Player.States
{
    public class StateIdle : State
    {
        public StateIdle(StateMachinePlayer player) : base(player) { }

        public override void OnEnter()
        {
            MachinePlayer.currentSpeed = 0f;
        }

        public override void OnUpdate()
        {
            if (MachinePlayer.IsDead)
            {
                MachinePlayer.ChangeState(StateMachinePlayer.STATE_DEAD);
            }
            else
            {
                if (MachinePlayer.DashPressed)
                {
                    MachinePlayer.ChangeState(StateMachinePlayer.STATE_DASH);
                }
                else if (MachinePlayer.IsMoving)
                {
                    MachinePlayer.ChangeState(StateMachinePlayer.STATE_WALK);
                }
                else if (MachinePlayer.IsAttacking)
                {
                    MachinePlayer.ChangeState(StateMachinePlayer.STATE_ATTACK);
                }
                else if (MachinePlayer.IsHit)
                {
                    MachinePlayer.ChangeState(StateMachinePlayer.STATE_HIT);
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
