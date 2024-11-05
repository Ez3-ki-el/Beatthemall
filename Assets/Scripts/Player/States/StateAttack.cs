using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unity.VisualScripting;

using UnityEditorInternal;

using UnityEngine;

namespace Assets.Scripts.Player.States
{
    public class StateAttack : State
    {
        public StateAttack(StateMachinePlayer player) : base(player) { }

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
                if (!MachinePlayer.IsMoving && !MachinePlayer.IsAttacking)
                {
                    MachinePlayer.ChangeState(StateMachinePlayer.STATE_IDLE);
                }
                else if (MachinePlayer.IsMoving)
                {
                    MachinePlayer.ChangeState(StateMachinePlayer.STATE_WALK);
                }
                else if (MachinePlayer.IsHit)
                {
                    MachinePlayer.ChangeState(StateMachinePlayer.STATE_HIT);
                }
                else if (MachinePlayer.DashPressed)
                {
                    MachinePlayer.ChangeState(StateMachinePlayer.STATE_DASH);
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
