using UnityEngine;

namespace Assets.Scripts.Enemies.CasualEnemy
{
    public class StateAttack : State
    {

        private int currentAttackCounter;
        private readonly int maxAttackCounter = 4;
        private readonly float bufferAttack = 0.2f;
        private float chronoAttack = 0f;

        public StateAttack(StateMachineEnemy enemy) : base(enemy) { }

        public override void OnEnter()
        {
      
            // Gestion du compteur d'attaque pour changer les animations
            if (currentAttackCounter == maxAttackCounter)
                currentAttackCounter = 0;

            // On passe à l'anim suivante
            currentAttackCounter++;

            MachineEnemy.Animator.SetInteger("CounterAttack", currentAttackCounter);
            MachineEnemy.Animator.SetBool("IsAttacking", true);

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
                if (chronoAttack > bufferAttack)
                {
                    chronoAttack = 0;
                    if (!MachineEnemy.IsMoving)
                    {
                        MachineEnemy.ChangeState(StateMachineEnemy.STATE_IDLE);
                    }
                    else // On le force à se remettre en mouvement
                    {
                        MachineEnemy.ChangeState(StateMachineEnemy.STATE_WALK);
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
     
            MachineEnemy.IsAttacking = false;
            MachineEnemy.AttackArea.SetActive(false);
            MachineEnemy.Animator.SetBool("IsAttacking", false);
        }

        public override void OnFixedUpdate()
        {

        }

        public override void OnTriggerEnter()
        {

        }
    }
}
