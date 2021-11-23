using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalStats : MonoBehaviour
{
    public Text PostBattleStats;

    private void Start()
    {
        writeStats();
    }
    public void writeStats()
    {
        PostBattleStats.text = "Enemies Destroyed: " + TrackStats.SharedInstance.EnemiesDestroyed + '\n' + '\n' +

                    "Bullets Fired: " + TrackStats.SharedInstance.BulletsFired + '\n' + '\n' +

                   "Number of Special Abilities Used: " + TrackStats.SharedInstance.TotalSpecialAbilitiesUsed + '\n' + '\n' +

                    "Total Deaths: " + TrackStats.SharedInstance.TotalDeaths + '\n' + '\n' +

                   "Times Level One Completed: " + TrackStats.SharedInstance.NumLevelOneCompleted + '\n' + '\n' +

                   "Times Level Two Completed: " + TrackStats.SharedInstance.NumLevelTwoCompleted + '\n' + '\n' +

                   "Times Level Three Completed: " + TrackStats.SharedInstance.NumLevelThreeCompleted + '\n' + '\n';
    }
}
