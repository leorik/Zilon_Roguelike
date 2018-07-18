﻿using System;
using System.Collections.Generic;
using System.Linq;
using Zilon.Core.Persons;

namespace Zilon.Core.Client
{
    public class PropTransferStore : IPropStore
    {
        private readonly IPropStore _propStore;
        private readonly List<IProp> _propAdded;
        private readonly List<IProp> _propRemoved;

        public PropTransferStore(IPropStore propStore)
        {
            _propStore = propStore;

            _propAdded = new List<IProp>();
            _propRemoved = new List<IProp>();
        }

        public IProp[] Items
        {
            get
            {
                return CalcItems();
            }
        }

        private IProp[] CalcItems()
        {
            var result = new List<IProp>();

            foreach (var prop in _propStore.Items)
            {
                switch (prop)
                {
                    case Resource resource:
                        var removedResource = _propRemoved.OfType<Resource>()
                            .SingleOrDefault(x => x.Scheme == resource.Scheme);

                        var addedResource = _propAdded.OfType<Resource>()
                            .SingleOrDefault(x => x.Scheme == resource.Scheme);

                        var addedCount = addedResource?.Count;
                        var removedCount = removedResource?.Count;
                        var remainsCount = resource.Count + addedCount.GetValueOrDefault() - removedCount.GetValueOrDefault();

                        if (remainsCount > 0)
                        {
                            var remainsResource = new Resource(resource.Scheme, remainsCount);
                            result.Add(remainsResource);
                        }
                        
                        break;

                    case Equipment equipment:
                    case Recipe recipe:
                        var isRemoved = _propRemoved.Contains(prop);

                        if (!isRemoved)
                        {
                            result.Add(prop);
                        }
                        break;
                }
            }

            foreach (var prop in _propAdded)
            {
                result.Add(prop);
            }

            return result.ToArray();
        }

        public event EventHandler<PropStoreEventArgs> Added;
        public event EventHandler<PropStoreEventArgs> Removed;
        public event EventHandler<PropStoreEventArgs> Changed;

        public void Add(IProp prop)
        {
            switch (prop)
            {
                case Resource resource:

                    TransferResource(resource, _propRemoved, _propAdded);
                    break;

                case Equipment equipment:
                case Recipe recipe:
                    TransferNoCount(prop, _propRemoved, _propAdded);
                    break;
            }
        }

        private void TransferResource(Resource resource, IList<IProp> bittenList, IList<IProp> oppositList)
        {
            var removedResource = bittenList.OfType<Resource>().SingleOrDefault(x => x.Scheme == resource.Scheme);
            if (removedResource != null)
            {
                var removedRemains = removedResource.Count - resource.Count;
                if (removedRemains <= 0)
                {
                    bittenList.Remove(removedResource);

                    if (removedRemains < 0)
                    {
                        var addCount = Math.Abs(removedRemains);
                        var addedResource = new Resource(resource.Scheme, resource.Count);
                        oppositList.Add(addedResource);
                    }
                }
            }
            else
            {
                var addedResource = oppositList.OfType<Resource>().SingleOrDefault(x => x.Scheme == resource.Scheme);
                if (addedResource == null)
                {
                    addedResource = new Resource(resource.Scheme, resource.Count);
                    oppositList.Add(addedResource);
                }
                else
                {
                    addedResource.Count += resource.Count;
                }
            }
        }

        public void Remove(IProp prop)
        {
            switch (prop)
            {
                case Resource resource:

                    TransferResource(resource, _propAdded, _propRemoved);
                    break;

                case Equipment equipment:
                case Recipe recipe:
                    TransferNoCount(prop, _propAdded, _propRemoved);
                    break;
            }
        }

        private void TransferNoCount(IProp prop, IList<IProp> bittenList, IList<IProp> oppositList)
        {
            var isRemoved = bittenList.Contains(prop);
            if (isRemoved)
            {
                bittenList.Remove(prop);
            }

            var inOriginal = _propStore.Items.Contains(prop);
            if (!inOriginal)
            {
                oppositList.Add(prop);
            }
        }
    }
}