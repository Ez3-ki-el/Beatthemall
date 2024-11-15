using UnityEngine;

namespace Assets.Scripts.Player.States
{
    public class StateAttack : State
    {
        private int currentAttackCounter;
        private readonly int maxAttackCounter = 4;
        private readonly float bufferAttack = 0.2f;
        private float chronoAttack = 0f;

        public StateAttack(StateMachinePlayer player) : base(player) { }

        public override void OnEnter()
        {
            Debug.LogWarning("DEBUT ATTACK");
            // Gestion du compteur d'attaque pour changer les animations
            if (currentAttackCounter == maxAttackCounter)
                currentAttackCounter = 0;

            // On passe à l'anim suivante
            currentAttackCounter++;

            MachinePlayer.Animator.SetInteger("CounterAttack", currentAttackCounter);
            MachinePlayer.AttackArea.SetActive(true);
            MachinePlayer.AudioSource.pitch = UnityEngine.Random.Range(0.7f, 1f);
            MachinePlayer.AudioSource.volume = UnityEngine.Random.Range(0.9f, 1f);
            MachinePlayer.AnimatorHit.SetBool("IsHitting", true);
        }

        public override void OnUpdate()
        {
            if (MachinePlayer.IsDead)
            {
                MachinePlayer.ChangeState(StateMachinePlayer.STATE_DEAD);
            }
            else
            {
                if (chronoAttack > bufferAttack)
                {
                    chronoAttack = 0;
                    if (MachinePlayer.IsUlting)
                    {
                        MachinePlayer.ChangeState(StateMachinePlayer.STATE_ULTI);
                    }
                    else if (MachinePlayer.DashPressed)
                    {
                        MachinePlayer.ChangeState(StateMachinePlayer.STATE_DASH);
                    }
                    else if (!MachinePlayer.IsMoving)
                    {
                        MachinePlayer.ChangeState(StateMachinePlayer.STATE_IDLE);
                    }
                    else if (MachinePlayer.IsMoving)
                    {
                        MachinePlayer.ChangeState(StateMachinePlayer.STATE_WALK);
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
            Debug.LogWarning("FIN ATTACK");
            MachinePlayer.AttackArea.SetActive(false);
            MachinePlayer.IsAttacking = false;
            MachinePlayer.AnimatorHit.SetBool("IsHitting", false);
        }

        public override void OnFixedUpdate()
        {

        }

        public override void OnTriggerEnter()
        {

        }
    }
}
