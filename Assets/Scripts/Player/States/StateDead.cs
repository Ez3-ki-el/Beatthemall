using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Player.States
{
    public class StateDead : State
    {
        public StateDead(StateMachinePlayer player) : base(player) { }

        public override void OnEnter()
        {
            MachinePlayer.Animator.SetBool("IsDead", MachinePlayer.IsDead);
            MachinePlayer.GetComponentsInChildren<Rigidbody2D>().ToList().ForEach(x => x.constraints = RigidbodyConstraints2D.FreezeAll);

            SceneManager.LoadScene("LoadScene");
            //SceneManager.UnloadSceneAsync("Level 1");

            GameObject.Destroy(MachinePlayer.gameObject, 2f);
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
