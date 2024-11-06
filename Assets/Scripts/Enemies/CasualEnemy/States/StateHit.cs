using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Enemies.CasualEnemy
{
    public class StateHit : State
    {
        public StateHit(StateMachineEnemy enemy) : base(enemy) { }

        public override void OnEnter()
        {

        }

        public override void OnUpdate()
        {
            if (MachineEnemy.IsDead)
            {
                MachineEnemy.ChangeState(StateMachineEnemy.STATE_DEAD);
            }
            else
            {
                if (!MachineEnemy.IsMoving)
                {
                    MachineEnemy.ChangeState(StateMachineEnemy.STATE_IDLE);
                }
                if (MachineEnemy.IsMoving)
                {
                    MachineEnemy.ChangeState(StateMachineEnemy.STATE_WALK);
                }
                else if (MachineEnemy.IsAttacking)
                {
                    MachineEnemy.ChangeState(StateMachineEnemy.STATE_ATTACK);
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
