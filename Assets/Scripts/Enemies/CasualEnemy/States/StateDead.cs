using System.Linq;

using Assets.Scripts.Enemies.CasualEnemy;

using UnityEngine;

namespace Assets.Scripts.Enemies.CasualEnemy
{
    public class StateDead : State
    {
        public StateDead(StateMachineEnemy enemy) : base(enemy) { }

        public override void OnEnter()
        {

            MachineEnemy.IsDead = true;
            MachineEnemy.AttackArea.SetActive(false);
            MachineEnemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            MachineEnemy.Animator.SetBool("IsDead", MachineEnemy.IsDead);
            MachineEnemy.GetComponentsInChildren<BoxCollider2D>().ToList().ForEach(x => x.isTrigger = true);
            MachineEnemy.OnDestroy();  // Appel direct de OnDestroy
        }

        public override void OnUpdate()
        {
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
