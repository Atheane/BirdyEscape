using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LayerLab
{
    public class PanelMonoRound : MonoBehaviour
    {
        [SerializeField] private GameObject[] otherPanels = new GameObject[1];

        public void OnEnable()
        {
            for (int i = 0; i < otherPanels.Length; i++) otherPanels[i].SetActive(true);
        }

        public void OnDisable()
        {
            for (int i = 0; i < otherPanels.Length; i++) otherPanels[i].SetActive(false);
        }
    }
}
