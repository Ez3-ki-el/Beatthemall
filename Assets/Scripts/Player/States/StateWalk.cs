using Assets.Scripts.Player;
using Assets.Scripts.Player.States;

using Unity.VisualScripting;

using UnityEngine;

namespace Assets.Scripts.Player.States
{
    public class StateWalk : State
    {
        public StateWalk(StateMachinePlayer player) : base(player) { }

        public override void OnEnter()
        {
            MachinePlayer.currentSpeed = MachinePlayer.walkSpeed;
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
                else if (!MachinePlayer.IsMoving)
                {
                    MachinePlayer.ChangeState(StateMachinePlayer.STATE_IDLE);
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
