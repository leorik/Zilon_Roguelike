﻿using NUnit.Framework;
using Zilon.Core.Tactics.Behaviour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zilon.Core.MapGenerators.PrimitiveStyle;
using Moq;
using Zilon.Core.Tactics.Spatial;
using FluentAssertions;
using Zilon.CoreTestsTemp.Tactics.Behaviour.TestCases;

namespace Zilon.Core.Tactics.Behaviour.Tests
{
    [TestFixture()]
    public class FowHelperTests
    {
        /// <summary>
        /// Тест проверяет, что на пустой карте данные тумана войны не пустые.
        /// </summary>
        [Test()]
        [TestCaseSource(typeof(FowHelperTestCaseDataSource), nameof(FowHelperTestCaseDataSource.TestCases))]
        public async Task UpdateFowData_SquareMap_ObseringNodesIsNotEmpty(int mapSize, int baseX, int baseY, int radius)
        {
            // ARRANGE

            var map = new SectorHexMap();
            for (var i = 0; i < mapSize; i++)
            {
                for (var j = 0; j < mapSize; j++)
                {
                    var hexNode = new HexNode(i, j);
                    map.AddNode(hexNode);
                }
            }

            var nodeList = new List<SectorMapFowNode>();
            var fowDataMock = new Mock<ISectorFowData>();
            fowDataMock.Setup(x => x.AddNodes(It.IsAny<IEnumerable<SectorMapFowNode>>()))
                .Callback<IEnumerable<SectorMapFowNode>>(nodes => nodeList.AddRange(nodes));
            var fowData = fowDataMock.Object;

            var baseNode = map.HexNodes.Single(x => x.OffsetX == baseX && x.OffsetY == baseY);

            // ACT

            FowHelper.UpdateFowData(fowData, map, baseNode, radius);

            // ARRANGE
            nodeList.Where(x => x.State == SectorMapNodeFowState.Observing).Should().NotBeEmpty();
        }
    }
}