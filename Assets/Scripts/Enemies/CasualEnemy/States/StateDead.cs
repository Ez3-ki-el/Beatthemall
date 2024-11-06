using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Enemies.CasualEnemy
{
    public class StateDead : State
    {
        public StateDead(StateMachineEnemy enemy) : base(enemy) { }

        public override void OnEnter()
        {
            MachineEnemy.IsDead = true;
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
