using Unity.VisualScripting;

using UnityEngine;

using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Assets.Scripts.Enemies.Boss
{
    public class StateWalk : State
    {
        public StateWalk(StateMachineBoss boss) : base(boss) { }

        public override void OnEnter()
        {
            MachineBoss.CurrentSpeed = MachineBoss.WalkSpeed;
        }

        public override void OnUpdate()
        {

            //Debug.Log(MachineBoss.DistanceToPlayerAggro + " DistanceToPlayerAggro");
            //Debug.Log(MachineBoss.AttackRange + " AttackRange");
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
                else if (MachineBoss.DistanceToPlayerAggro <= MachineBoss.AttackRange)
                {
                    MachineBoss.ChangeState(StateMachineBoss.STATE_ATTACK);
                }
            }
        }

        public override void OnExit()
        {

        }

        public override void OnFixedUpdate()
        {
            // Se déplacer vers le joueur
            Vector2 direction = (MachineBoss.PlayerAggro.position - MachineBoss.transform.position).normalized;
            MachineBoss.Rb2dEnemy.linearVelocity = direction * MachineBoss.CurrentSpeed;
        }

        public override void OnTriggerEnter()
        {

        }
    }
}
