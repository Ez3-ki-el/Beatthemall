using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Enemies.Boss
{
    public class StateDead : State
    {
        public StateDead(StateMachineBoss boss) : base(boss) { }

        public override void OnEnter()
        {
            MachineBoss.IsDead = true;
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
