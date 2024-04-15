using System.Linq;
using DG.Tweening;
using UnityEngine;

public class GearCrank : MonoBehaviour
{
    [SerializeField] private float maxGearDistance = 2.5f;
    [SerializeField] private float snapDuration = 0.4f;

    private void OnEnable()
    {
        var gears = CurrentGameState.GameState.Gears;
        var gearsByDistance = gears.ToDictionary(g => g, g => transform.XzDistanceFromSelf(g.transform));
        var closestGear = gearsByDistance
            .Where(gs => gs.Value <= maxGearDistance)
            .OrderBy(gs => gs.Value)
            .Select(gs => gs.Key)
            .FirstOrDefault();

        if (closestGear != null)
        {
            SpinGear(closestGear);
        }
    }

    private void SpinGear(FixedGear gear)
    {
        Log.Info("Gear Crank - Spinning");
        transform.DOMove(gear.GearSnapPosition, snapDuration);
        this.ExecuteAfterDelay(() =>
        {
            gear.Rotate(() => FinishSpin());
        }, snapDuration);
    }

    private void FinishSpin()
    {
        Log.Info("Gear Crank - Finished Spin");
        gameObject.SetActive(false);
    }
}
