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
            MachinePlayer.Animator.SetFloat("Speed", 0);
        }

        public override void OnUpdate()
        {
            if (MachinePlayer.IsDead)
            {
                MachinePlayer.ChangeState(StateMachinePlayer.STATE_DEAD);
            }
            else
            {
                if (MachinePlayer.IsUlting)
                {
                    MachinePlayer.ChangeState(StateMachinePlayer.STATE_ULTI);
                }
                else if (MachinePlayer.DashPressed)
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
