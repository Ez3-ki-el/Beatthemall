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
        private readonly float bufferAttack = 0.6f;
        private float chronoAttack = 0f;

        public StateAttack(StateMachineBoss boss) : base(boss) { }

        public override void OnEnter()
        {
            MachineBoss.Rb2dEnemy.linearVelocity = Vector2.zero;
            MachineBoss.IsAttacking = true;
            MachineBoss.AttackArea.SetActive(true);
            MachineBoss.Animator.SetBool("IsAttacking", true);
        }

        public override void OnUpdate()
        {
            if (MachineBoss.IsDead)
            {
                MachineBoss.ChangeState(StateMachineBoss.STATE_DEAD);
            }
            else
            {
                if (chronoAttack > bufferAttack)
                {
                    chronoAttack = 0;

                    if (MachineBoss.IsAttackingAOE)
                    {
                        MachineBoss.ChangeState(StateMachineBoss.STATE_ATTACK_AOE);
                    }
                    else if (!MachineBoss.IsMoving)
                    {
                        MachineBoss.ChangeState(StateMachineBoss.STATE_IDLE);
                    }
                    else // On le force à se remettre en mouvement
                    {
                        MachineBoss.ChangeState(StateMachineBoss.STATE_WALK);
                    }
                }
                else
                {
                    chronoAttack += Time.deltaTime;
                }
            }
        }

        public override void OnExit()
        {
            MachineBoss.IsAttacking = false;
            MachineBoss.AttackArea.SetActive(false);
            MachineBoss.Animator.SetBool("IsAttacking", false);
        }

        public override void OnFixedUpdate()
        {

        }

        public override void OnTriggerEnter()
        {

        }
    }
}
