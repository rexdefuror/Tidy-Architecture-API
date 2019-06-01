using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetLore.Application.Read.TaskLists;
using NetLore.Application.Write.TaskLists;
using NetLore.Data.Contexts;
using NetLore.Tests.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetLore.Tests.Requests
{
    [TestClass]
    public class TaskListTests
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

            Mapper.Initialize(x => x.AddProfile<Infrastructure.Profiles.TaskListProfile>());
        }

        [TestMethod]
        public void ProfileConfiguration()
        {
            Mapper.AssertConfigurationIsValid();
        }

        [TestMethod]
        public async Task FindAllTaskLists_Success()
        {
            var expectedResultCount = 5;

            var dataGenerationResult = _noTrackingContext.AddTestData<Domain.Entities.TaskList>(expectedResultCount);
            Assert.IsTrue(dataGenerationResult.Item1);

            var handler = new FindAllTaskListsRequestHandler(_noTrackingContext, Mapper.Instance);
            var result = await handler.Handle(new FindAllTaskListsRequest(), default(CancellationToken));

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResultCount, result.Count);
        }

        [TestMethod]
        public async Task FindTaskListById_Success()
        {
            var dataGenerationResult = _noTrackingContext.AddTestData<Domain.Entities.TaskList>(1);
            Assert.IsTrue(dataGenerationResult.Item1);

            var selectedId = dataGenerationResult.Item2[0];
            var request = new FindTaskListByIdRequest(selectedId);

            var handler = new FindTaskListByIdRequestHandler(_noTrackingContext, Mapper.Instance);
            var result = await handler.Handle(request, default(CancellationToken));
            Assert.IsNotNull(result);
            Assert.AreEqual(selectedId, result.Id);
        }

        [DataTestMethod]
        [DataRow(0, false)]
        [DataRow(1, true)]
        [DataRow(3243243, true)]
        public async Task FindTaskListById_Validation(int id, bool isValid)
        {
            var request = new FindTaskListByIdRequest(id);

            var validator = new FindTaskListByIdValidator();
            var validationResult = await validator.ValidateAsync(request);

            Assert.AreEqual(isValid, validationResult.IsValid);
        }


        [TestMethod]
        public async Task SaveTaskList_Success()
        {
            var model = new Domain.Models.TaskList { Name = "TestTaskList" };
            var request = new SaveTaskListRequest(model);

            var handler = new SaveTaskListRequestHandler(_trackingContext, Mapper.Instance);
            var result = await handler.Handle(request, default(CancellationToken));
            Assert.AreEqual(result, Unit.Value);
        }

        [TestMethod]
        public async Task UpdateTaskList_Success()
        {
            var dataGenerationResult = _trackingContext.AddTestData<Domain.Entities.TaskList>(5);
            var selectedId = dataGenerationResult.Item2[1];
            var updatedModel = new Domain.Models.TaskList { Id = selectedId, Name = "NewTaskListName" };
            var request = new UpdateTaskListRequest(selectedId, updatedModel);

            var handler = new UpdateTaskListRequestHandler(_trackingContext, Mapper.Instance);
            var result = await handler.Handle(request, default(CancellationToken));
            Assert.AreEqual(Unit.Value, result);
        }

        [TestMethod]
        public async Task DeleteTask_Success()
        {
            var dataGenerationResult = _trackingContext.AddTestData<Domain.Entities.TaskList>(1);
            Assert.IsTrue(dataGenerationResult.Item1);

            var selectedId = dataGenerationResult.Item2[0];
            var request = new DeleteTaskListRequest(selectedId);

            var handler = new DeleteTaskListRequestHandler(_trackingContext);
            var result = await handler.Handle(request, default(CancellationToken));
            Assert.AreEqual(Unit.Value, result);
        }

        [DataTestMethod]
        [DataRow(0, false)]
        [DataRow(1, true)]
        [DataRow(3243243, true)]
        public async Task DeleteTaskList_Validation(int id, bool isValid)
        {
            var request = new DeleteTaskListRequest(id);

            var validator = new DeleteTaskListValidator();
            var validationResult = await validator.ValidateAsync(request);

            Assert.AreEqual(isValid, validationResult.IsValid);
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
