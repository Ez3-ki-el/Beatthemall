using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.Scripts.Player.States
{
    public class StateDead : State
    {
        public StateDead(StateMachinePlayer player) : base(player) { }

        public override void OnEnter()
        {
            MachinePlayer.Animator.SetBool("IsDead", MachinePlayer.IsDead);
        }

        public override void OnUpdate()
        {

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
