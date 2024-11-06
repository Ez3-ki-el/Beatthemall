using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unity.VisualScripting;

using UnityEditorInternal;

using UnityEngine;

namespace Assets.Scripts.Enemies.CasualEnemy
{
    public class StateAttack : State
    {
        public StateAttack(StateMachineEnemy enemy) : base(enemy) { }

        public override void OnEnter()
        {
            MachineEnemy.Rb2dEnemy.linearVelocity = Vector2.zero;
            MachineEnemy.IsAttacking = true;
            MachineEnemy.AttackArea.SetActive(true);
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
                else if (MachineEnemy.IsMoving)
                {
                    MachineEnemy.ChangeState(StateMachineEnemy.STATE_WALK);
                }
                else if (MachineEnemy.IsHit)
                {
                    MachineEnemy.ChangeState(StateMachineEnemy.STATE_HIT);
                }
            }
        }

        public override void OnExit()
        {
            MachineEnemy.IsAttacking = false;
            MachineEnemy.AttackArea.SetActive(false);
        }

        public override void OnFixedUpdate()
        {

        }

        public override void OnTriggerEnter()
        {

        }
    }
}
