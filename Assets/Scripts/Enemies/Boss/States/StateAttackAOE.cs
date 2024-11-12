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
            MachineBoss.Animator.SetBool("IsHeavyAttacking", true);
        }

        public override void OnUpdate()
        {
            if (MachineBoss.IsDead)
            {
                MachineBoss.ChangeState(StateMachineBoss.STATE_DEAD);
            }
            else
            {
                if (chronoAoe >= MachineBoss.DurationAOE)
                {
                    MachineBoss.IsAttackingAOE = false;
                    MachineBoss.AttackAreaAOE.SetActive(false);
                    chronoAoe = 0;

                    if (MachineBoss.DistanceToPlayerAggro <= MachineBoss.AttackRange)
                    {
                        MachineBoss.ChangeState(StateMachineBoss.STATE_ATTACK);
                    }
                    else if (!MachineBoss.IsMoving)
                    {
                        MachineBoss.ChangeState(StateMachineBoss.STATE_IDLE);
                    }
                    else
                    {
                        MachineBoss.ChangeState(StateMachineBoss.STATE_WALK);
                    }
                }
                else
                {
                    chronoAoe += Time.deltaTime;
                    MachineBoss.AttackAreaAOE.SetActive(true);
                    MachineBoss.IsAttackingAOE = true;
                }
            }
        }

        public override void OnExit()
        {
            MachineBoss.Animator.SetBool("IsHeavyAttacking", false);
        }

        public override void OnFixedUpdate()
        {

        }

        public override void OnTriggerEnter()
        {

        }
    }
}
