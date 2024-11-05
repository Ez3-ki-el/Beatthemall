using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Player.States
{
    public class StateHit : State
    {
        public StateHit(StateMachinePlayer player) : base(player) { }

        public override void OnEnter()
        {

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
                if (MachinePlayer.IsMoving)
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
