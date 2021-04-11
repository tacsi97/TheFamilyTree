using FamilyTree.Core.Attributes;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace FamilyTree.Core.Behaviors
{
    public class DependentViewRegionBehavior : RegionBehavior
    {
        private readonly IContainerExtension _container;

        public const string BehaviorKey = "DependentViewRegionBehavior";
        readonly Dictionary<object, IList<DependentViewInfo>> _dependentViewCache = new Dictionary<object, IList<DependentViewInfo>>();

        public DependentViewRegionBehavior(IContainerExtension container)
        {
            _container = container;
        }

        protected override void OnAttach()
        {
            Region.ActiveViews.CollectionChanged += ActiveViews_CollectionChanged;
        }

        private void ActiveViews_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action.Equals(NotifyCollectionChangedAction.Add))
            {
                var dependentViews = new List<DependentViewInfo>();

                foreach (var view in e.NewItems)
                {
                    if (!_dependentViewCache.ContainsKey(view))
                    {
                        var attributes = GetCustomAttributes<DependentViewAttribute>(view.GetType());

                        foreach (var attribute in attributes)
                        {
                            var info = CreateDependentViewInfo(attribute);

                            dependentViews.Add(info);
                        }

                        _dependentViewCache.Add(view, dependentViews);
                    }
                    else
                    {
                        dependentViews = _dependentViewCache[view] as List<DependentViewInfo>;
                    }

                    dependentViews.ForEach(view => Region.RegionManager.Regions[view.Region].Add(view.View));
                }
            }
            else if (e.Action.Equals(NotifyCollectionChangedAction.Remove))
            {
                foreach (var oldView in e.OldItems)
                {
                    if (_dependentViewCache.ContainsKey(oldView))
                        _dependentViewCache[oldView].ToList().ForEach(view => Region.RegionManager.Regions[view.Region].Remove(view.View));
                }
            }
        }

        DependentViewInfo CreateDependentViewInfo(DependentViewAttribute dependentViewAttribute)
        {
            return new DependentViewInfo()
            {
                Region = dependentViewAttribute.Region,
                View = _container.Resolve(dependentViewAttribute.Type)
            };
        }

        private static IEnumerable<T> GetCustomAttributes<T>(Type type)
        {
            return type.GetCustomAttributes(typeof(T), true).OfType<T>();
        }
    }
}
