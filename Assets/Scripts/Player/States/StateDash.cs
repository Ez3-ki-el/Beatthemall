﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.Scripts.Player.States
{
    public class StateDash : State
    {
        private float chrono = 0f;
        public StateDash(StateMachinePlayer player) : base(player) { }

        public override void OnEnter()
        {
            MachinePlayer.currentSpeed = MachinePlayer.dashSpeed;
            MachinePlayer.Animator.SetFloat("Speed", MachinePlayer.dashSpeed);
            MachinePlayer.Dash.SetActive(true);
            chrono = 0f;
        }

        public override void OnUpdate()
        {
            if (MachinePlayer.IsDead)
            {
                MachinePlayer.ChangeState(StateMachinePlayer.STATE_DEAD);
            }
            else
            {
                if (!MachinePlayer.DashPressed)
                {
                    if (MachinePlayer.IsUlting)
                    {
                        MachinePlayer.ChangeState(StateMachinePlayer.STATE_ULTI);
                    }
                    else if (MachinePlayer.IsAttacking)
                    {
                        MachinePlayer.ChangeState(StateMachinePlayer.STATE_ATTACK);
                    }
                    if (!MachinePlayer.IsMoving)
                    {
                        MachinePlayer.ChangeState(StateMachinePlayer.STATE_IDLE);
                    }
                    else if (MachinePlayer.IsMoving)
                    {
                        MachinePlayer.ChangeState(StateMachinePlayer.STATE_WALK);
                    }
                }
            }
        }

        public override void OnExit()
        {
            MachinePlayer.currentSpeed = MachinePlayer.walkSpeed * MachinePlayer.multiplier;
            MachinePlayer.Dash.SetActive(false);
        }

        public override void OnFixedUpdate()
        {
            if (chrono >= MachinePlayer.dashDuration)
            {
                MachinePlayer.DashPressed = false;
                MachinePlayer.DashAvailable = false;
                chrono = 0f;
            }
            else
            {
               
                chrono += Time.deltaTime;
            }
        }

        public override void OnTriggerEnter()
        {

        }
    }
}
