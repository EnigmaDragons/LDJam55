using MagicPigGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class ManaBar : OnMessage<GameStateChanged>
    {
        
        private float t = 0;

        [SerializeField] private int MaxMana = 100;
        [SerializeField] private ProgressBar ManaProgressBar;
        [SerializeField] private float ManaRegenRate = 1;

        protected override void Execute(GameStateChanged msg)
        {
            ManaProgressBar.SetProgress(CurrentGameState.GameState.Mana / (float)MaxMana);
        }

        private void Start()
        {
            CurrentGameState.UpdateState(x => x.Mana = MaxMana);
            ManaProgressBar.SetProgress(CurrentGameState.GameState.Mana / (float)MaxMana);
        }

        private void Update()
        {
            if (CurrentGameState.GameState.Mana >= MaxMana)
            {
                t = 0;
                return;
            }

            t += Time.deltaTime;
            if (t > ManaRegenRate)
            {
                t -= ManaRegenRate;
                CurrentGameState.UpdateState(x => x.Mana++);
            }
        }
    }
}
