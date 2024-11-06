using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unity.VisualScripting;

using UnityEditorInternal;

using UnityEngine;

namespace Assets.Scripts.Enemies.Boss
{
    public class StateAttack : State
    {
        public StateAttack(StateMachineBoss boss) : base(boss) { }

        public override void OnEnter()
        {
            MachineBoss.Rb2dEnemy.linearVelocity = Vector2.zero;
            MachineBoss.IsAttacking = true;
            MachineBoss.AttackArea.SetActive(true);

            //Debug.Log("Boss attack");
        }

        public override void OnUpdate()
        {
            if (MachineBoss.IsDead)
            {
                MachineBoss.ChangeState(StateMachineBoss.STATE_DEAD);
            }
            else
            {
                if (MachineBoss.IsAttackingAOE)
                {
                    MachineBoss.ChangeState(StateMachineBoss.STATE_ATTACK_AOE);
                }
                else if (!MachineBoss.IsMoving)
                {
                    MachineBoss.ChangeState(StateMachineBoss.STATE_IDLE);
                }
                else if (MachineBoss.IsMoving)
                {
                    MachineBoss.ChangeState(StateMachineBoss.STATE_WALK);
                }
            }
        }

        public override void OnExit()
        {
            MachineBoss.IsAttacking = false;
            MachineBoss.AttackArea.SetActive(false);
        }

        public override void OnFixedUpdate()
        {

        }

        public override void OnTriggerEnter()
        {

        }
    }
}
