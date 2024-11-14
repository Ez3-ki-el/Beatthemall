using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.Scripts.Player.States
{
    public class StateUlti : State
    {
        private float chronoUlti = 0f;
        public StateUlti(StateMachinePlayer player) : base(player) { }

        public override void OnEnter()
        {
            MachinePlayer.currentSpeed = 0;
            MachinePlayer.Animator.SetBool("IsUlting", true);
        }

        public override void OnExit()
        {
            MachinePlayer.IsUlting = false;
            MachinePlayer.Animator.SetBool("IsUlting", false);
            MachinePlayer.playerPoints.UltiPoints = 0;
        }

        public override void OnFixedUpdate()
        {
            if (MachinePlayer.IsDead)
            {
                MachinePlayer.ChangeState(StateMachinePlayer.STATE_DEAD);
            }
            else
            {
                if (chronoUlti >= MachinePlayer.UltiDuration)
                {
                    MachinePlayer.IsUlting = false;
                    MachinePlayer.AttackAreaUlti.SetActive(false);
                    chronoUlti = 0;

                    if (MachinePlayer.DashPressed)
                    {
                        MachinePlayer.ChangeState(StateMachinePlayer.STATE_DASH);
                    }
                    else if (MachinePlayer.IsAttacking)
                    {
                        MachinePlayer.ChangeState(StateMachinePlayer.STATE_ATTACK);
                    }
                    else if (!MachinePlayer.IsMoving)
                    {
                        MachinePlayer.ChangeState(StateMachinePlayer.STATE_IDLE);
                    }
                    else if (MachinePlayer.IsMoving)
                    {
                        MachinePlayer.ChangeState(StateMachinePlayer.STATE_WALK);
                    }
                }
                else
                {
                    chronoUlti += Time.deltaTime;
                    MachinePlayer.AttackAreaUlti.SetActive(true);
                    MachinePlayer.IsUlting = true;
                }
            }
        }

        public override void OnTriggerEnter()
        {
        }

        public override void OnUpdate()
        {
        }
    }
}
