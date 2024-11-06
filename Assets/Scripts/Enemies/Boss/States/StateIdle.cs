using Assets.Scripts.Player;
using Assets.Scripts.Player.States;

using UnityEngine;

namespace Assets.Scripts.Enemies.Boss
{
    public class StateIdle : State
    {
        public StateIdle(StateMachineBoss boss) : base(boss) { }

        public override void OnEnter()
        {
            MachineBoss.CurrentSpeed = 0f;
        }

        public override void OnUpdate()
        {
            if (MachineBoss.IsDead)
            {
                MachineBoss.ChangeState(StateMachinePlayer.STATE_DEAD);
            }
            else
            {
                if (MachineBoss.IsAttackingAOE)
                {
                    MachineBoss.ChangeState(StateMachineBoss.STATE_ATTACK_AOE);
                }
                else if (MachineBoss.DistanceToPlayerAggro > MachineBoss.AttackRange)
                {
                    MachineBoss.ChangeState(StateMachinePlayer.STATE_WALK);
                }
                else
                {
                    MachineBoss.ChangeState(StateMachinePlayer.STATE_ATTACK);
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
