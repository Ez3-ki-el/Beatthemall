using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Player.States
{
    public abstract class State
    {
        protected StateMachinePlayer MachinePlayer;

        protected State(StateMachinePlayer machinePlayer)
        {
            MachinePlayer = machinePlayer;
        }

        public abstract void OnUpdate();
        public abstract void OnFixedUpdate();
        public abstract void OnTriggerEnter();
        public abstract void OnEnter();
        public abstract void OnExit();

    }
}
