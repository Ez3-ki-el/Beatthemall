using UnityEngine;

namespace Assets.Scripts.Enemies.Boss
{
    public class StateWalk : State
    {
        private Vector2 direction;

        public StateWalk(StateMachineBoss boss) : base(boss) { }

        public override void OnEnter()
        {
            MachineBoss.CurrentSpeed = MachineBoss.WalkSpeed;
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
            direction = (MachineBoss.PlayerAggro.position - MachineBoss.transform.position).normalized;
            MachineBoss.Rb2dEnemy.linearVelocity = direction * MachineBoss.CurrentSpeed;
            RotateEnemy();
        }

        public override void OnTriggerEnter()
        {

        }

        private void RotateEnemy()
        {
            // Gestion de la rotation X du perso dans ses déplacements
            if (direction.x < 0 && MachineBoss.transform.localScale.x > 0)
            {
                MachineBoss.transform.localScale = new Vector3(-Mathf.Abs(MachineBoss.transform.localScale.x), MachineBoss.transform.localScale.y, MachineBoss.transform.localScale.z);
            }
            else if (direction.x > 0 && MachineBoss.transform.localScale.x < 0)
            {
                MachineBoss.transform.localScale = new Vector3(Mathf.Abs(MachineBoss.transform.localScale.x), MachineBoss.transform.localScale.y, MachineBoss.transform.localScale.z);
            }
        }
    }
}
