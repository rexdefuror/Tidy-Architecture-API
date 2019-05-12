using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetLore.Application.Read.Tasks;
using NetLore.Application.Write.Tasks;
using NetLore.Data.Contexts;
using NetLore.Tests.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetLore.Tests.Requests
{
    [TestClass]
    public class TaskTests
    {
        private NoTrackingContext _noTrackingContext;
        private TrackingContext _trackingContext;

        [TestInitialize]
        public void Initialize()
        {
            var inMemoryDatabaseReference = Guid.NewGuid().ToString();

            var optionsNoTrackingContext = new DbContextOptionsBuilder<NoTrackingContext>().UseInMemoryDatabase(inMemoryDatabaseReference).Options;
            _noTrackingContext = new NoTrackingContext(optionsNoTrackingContext);

            var optionsTrackingContext = new DbContextOptionsBuilder<TrackingContext>().UseInMemoryDatabase(inMemoryDatabaseReference).Options;
            _trackingContext = new TrackingContext(optionsTrackingContext);

            Mapper.Initialize(x => x.AddProfile<Infrastructure.Profiles.TaskProfile>());
        }

        [TestMethod]
        public void ProfileConfiguration()
        {
            Mapper.AssertConfigurationIsValid();
        }

        [TestMethod]
        public async Task FindAllTasks_Success()
        {
            var expectedResultCount = 5;

            var dataGenerationResult = _noTrackingContext.AddTestData<Domain.Entities.Task>(expectedResultCount);
            Assert.IsTrue(dataGenerationResult.Item1);

            var handler = new FindAllTasksRequestHandler(_noTrackingContext, Mapper.Instance);
            var result = await handler.Handle(new FindAllTasksRequest(), default(CancellationToken));

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResultCount, result.Count);
        }

        [TestMethod]
        public async Task FindTaskById_Success()
        {
            var dataGenerationResult = _noTrackingContext.AddTestData<Domain.Entities.Task>(1);
            Assert.IsTrue(dataGenerationResult.Item1);

            var selectedId = dataGenerationResult.Item2[0];
            var request = new FindTaskByIdRequest(selectedId);

            var handler = new FindTaskByIdRequestHandler(_noTrackingContext, Mapper.Instance);
            var result = await handler.Handle(request, default(CancellationToken));
            Assert.IsNotNull(result);
            Assert.AreEqual(selectedId, result.Id);
        }

        [TestMethod]
        public async Task SaveTask_Success()
        {
            var model = new Domain.Models.Task { Name = "TestTask", Description = "TestTask description" };
            var request = new SaveTaskRequest(model);

            var handler = new SaveTaskRequestHandler(_trackingContext, Mapper.Instance);
            var result = await handler.Handle(request, default(CancellationToken));
            Assert.AreEqual(result, Unit.Value);
        }

        [TestMethod]
        public async Task UpdateTask_Success()
        {
            var dataGenerationResult = _trackingContext.AddTestData<Domain.Entities.Task>(5);
            var selectedId = dataGenerationResult.Item2[1];
            var updatedModel = new Domain.Models.Task { Id = selectedId, Name = "NewTaskName" };
            var request = new UpdateTaskRequest(selectedId, updatedModel);

            var handler = new UpdateTaskRequestHandler(_trackingContext, Mapper.Instance);
            var result = await handler.Handle(request, default(CancellationToken));
            Assert.AreEqual(Unit.Value, result);
        }

        [TestMethod]
        public async Task DeleteTask_Success()
        {
            var dataGenerationResult = _trackingContext.AddTestData<Domain.Entities.Task>(1);
            Assert.IsTrue(dataGenerationResult.Item1);

            var selectedId = dataGenerationResult.Item2[0];
            var request = new DeleteTaskRequest(selectedId);

            var handler = new DeleteTaskRequestHandler(_trackingContext);
            var result = await handler.Handle(request, default(CancellationToken));
            Assert.AreEqual(Unit.Value, result);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _trackingContext.Dispose();
            _noTrackingContext.Dispose();
            Mapper.Reset();
        }
    }
}
