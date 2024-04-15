using System.Linq;
using UnityEngine;

namespace Cheats
{
    public class GameWinCheat : MonoBehaviour
    {
        [SerializeField] private KeyCode[] keyCodes;

        private void Update()
        {
            if (keyCodes.All(x => Input.GetKey(x)))
                Message.Publish(new GameWon());
        }
    }
}