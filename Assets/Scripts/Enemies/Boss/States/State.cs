using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Enemies.Boss
{
    public abstract class State
    {
        protected StateMachineBoss MachineBoss;

        protected State(StateMachineBoss machineBoss)
        {
            MachineBoss = machineBoss;
        }

        public abstract void OnUpdate();
        public abstract void OnFixedUpdate();
        public abstract void OnTriggerEnter();
        public abstract void OnEnter();
        public abstract void OnExit();

    }
}
