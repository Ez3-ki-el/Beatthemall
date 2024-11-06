using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.Scripts.Enemies.Boss
{
    public class StateAttackAOE : State
    {
        private float chronoAoe = 0f;

        public StateAttackAOE(StateMachineBoss boss) : base(boss) { }

        public override void OnEnter()
        {
        
            MachineBoss.Rb2dEnemy.linearVelocity = Vector2.zero;
            //MachineBoss.IsAttackingAOE = true;
            //MachineBoss.AttackAreaAOE.SetActive(true);
        }

        public override void OnUpdate()
        {
            if (MachineBoss.IsDead)
            {
                MachineBoss.ChangeState(StateMachineBoss.STATE_DEAD);
            }
            else
            {
                if (MachineBoss.DistanceToPlayerAggro <= MachineBoss.AttackRange)
                {
                    MachineBoss.ChangeState(StateMachineBoss.STATE_ATTACK);
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
        }

        public override void OnFixedUpdate()
        {
            if (chronoAoe >= MachineBoss.DurationAOE)
            {
                //Debug.Log("Fin attack AOE");
                MachineBoss.IsAttackingAOE = false;
                MachineBoss.AttackAreaAOE.SetActive(false);
                chronoAoe = 0;
            }
            else
            {
                chronoAoe += Time.deltaTime;
                MachineBoss.AttackAreaAOE.SetActive(true);
                MachineBoss.IsAttackingAOE = true;
                //Debug.Log("Attack AOE");
            }
        }

        public override void OnTriggerEnter()
        {

        }
    }
}
