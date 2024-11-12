using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.Scripts.Enemies.CasualEnemy
{
    public class StateDead : State
    {
        public StateDead(StateMachineEnemy enemy) : base(enemy) { }

        public override void OnEnter()
        {
            MachineEnemy.IsDead = true;
            MachineEnemy.Animator.SetBool("IsDead", MachineEnemy.IsDead);
            GameObject.Destroy(MachineEnemy.gameObject, 2f);
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
