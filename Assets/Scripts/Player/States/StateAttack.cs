using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unity.VisualScripting;

using UnityEditorInternal;

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
            // Gestion du compteur d'attaque pour changer les animations
            if (currentAttackCounter == maxAttackCounter)
                currentAttackCounter = 0;

            // On passe à l'anim suivante
            currentAttackCounter++;

            MachinePlayer.Animator.SetInteger("CounterAttack", currentAttackCounter);
            MachinePlayer.AttackArea.SetActive(true);
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
            MachinePlayer.AttackArea.SetActive(false);
            MachinePlayer.IsAttacking = false;
        }

        public override void OnFixedUpdate()
        {

        }

        public override void OnTriggerEnter()
        {

        }
    }
}
