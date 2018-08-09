﻿using FluentAssertions;

using LightInject;

using Moq;

using NUnit.Framework;

using System.Collections.Generic;
using System.Linq;

using Zilon.Core.Client;
using Zilon.Core.Persons;
using Zilon.Core.Tactics;
using Zilon.Core.Tactics.Behaviour;
using Zilon.Core.Tactics.Spatial;
using Zilon.Core.Tests.TestCommon;

namespace Zilon.Core.Commands.Tests
{
    [TestFixture]
    public class PropTrasferCommandTests
    {
        private ServiceContainer _container;

        /// <summary>
        /// Тест проверяет, что можно использовать экипировку.
        /// </summary>
        [Test]
        public void CanExecuteTest()
        {
            // ARRANGE
            var command = _container.GetInstance<PropTransferCommand>();



            // ACT
            var canExecute = command.CanExecute();


            // ASSERT
            canExecute.Should().Be(true);
        }

        /// <summary>
        /// Тест проверяет, что при выполнении команды корректно фисируется намерение игрока на атаку.
        /// </summary>
        [Test]
        public void ExecuteTest()
        {
            var command = _container.GetInstance<PropTransferCommand>();
            var humanTaskSourceMock = _container.GetInstance<Mock<IHumanActorTaskSource>>();



            // ACT
            command.Execute();


            // ASSERT
            humanTaskSourceMock.Verify(x => x.IntentTransferProps(It.IsAny<IEnumerable<PropTransfer>>()));
        }

        [SetUp]
        public void SetUp()
        {
            _container = new ServiceContainer();

            var testMap = new TestGridGenMap();

            var sectorMock = new Mock<ISector>();
            var sector = sectorMock.Object;

            var sectorManagerMock = new Mock<ISectorManager>();
            sectorManagerMock.SetupProperty(x => x.CurrentSector, sector);
            var sectorManager = sectorManagerMock.Object;
            _container.Register(factory => sectorManager);


            var actorMock = new Mock<IActor>();
            var actorNode = testMap.Nodes.OfType<HexNode>().SelectBy(0, 0);
            actorMock.SetupGet(x => x.Node).Returns(actorNode);
            var actor = actorMock.Object;

            var actorVmMock = new Mock<IActorViewModel>();
            actorVmMock.SetupProperty(x => x.Actor, actor);
            var actorVm = actorVmMock.Object;

            var humanTaskSourceMock = new Mock<IHumanActorTaskSource>();
            _container.Register(factory => humanTaskSourceMock);
            var humanTaskSource = humanTaskSourceMock.Object;

            var playerStateMock = new Mock<IPlayerState>();
            playerStateMock.SetupProperty(x => x.ActiveActor, actorVm);
            playerStateMock.SetupProperty(x => x.TaskSource, humanTaskSource);
            var playerState = playerStateMock.Object;
            _container.Register(factory => playerState);

            var inventory = CreateStore();
            var container = CreateStore();
            var transferMachine = new PropTransferMachine(inventory, container);

            _container.Register<PropTransferCommand>();
            _container.Register(factory => transferMachine);
        }

        private IPropStore CreateStore() {
            var storeMock = new Mock<IPropStore>();
            return storeMock.Object;
        }
    }
}