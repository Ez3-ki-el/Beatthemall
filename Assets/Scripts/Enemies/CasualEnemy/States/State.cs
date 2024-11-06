using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Enemies.CasualEnemy
{
    public abstract class State
    {
        protected StateMachineEnemy MachineEnemy;

        protected State(StateMachineEnemy machineEnemy)
        {
            MachineEnemy = machineEnemy;
        }

        public abstract void OnUpdate();
        public abstract void OnFixedUpdate();
        public abstract void OnTriggerEnter();
        public abstract void OnEnter();
        public abstract void OnExit();

    }
}
