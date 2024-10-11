using System.Collections.Generic;
using UnityEngine;
using SurvivalChicken.Interfaces;

namespace SurvivalChicken.CollectItems
{
    public class CollectedPanelView : MonoBehaviour
    {
        [SerializeField] private CollectedItemView _collectItemView;
        [SerializeField] private Transform _container;

        private List<CollectedItemView> _collectItemsView = new List<CollectedItemView>();

        public void Initialize(params ICollect[] collectItems)
        {
            for(int i = 0; i < collectItems.Length; i++)
            {
                if (_collectItemsView.Count > i)
                    _collectItemsView[i].Initialize(collectItems[i]);
                else
                    CreateCollectItem(collectItems[i]);
            }

            gameObject.SetActive(true);
        }

        private void CreateCollectItem(ICollect collectItems)
        {
            CollectedItemView collectedItemView = Instantiate(_collectItemView);
            collectedItemView.transform.SetParent(_container);
            collectedItemView.Initialize(collectItems);
            _collectItemsView.Add(collectedItemView);
        }

        private void ClearCollectItems()
        {
            if (_collectItemsView.Count <= 0)
                return;

            foreach (CollectedItemView item in _collectItemsView)
                Destroy(item.gameObject);
            _collectItemsView.Clear();
        }
    }
}
