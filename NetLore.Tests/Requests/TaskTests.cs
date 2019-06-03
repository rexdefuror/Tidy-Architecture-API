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

        [DataTestMethod]
        [DataRow(0, false)]
        [DataRow(1, true)]
        [DataRow(4545, true)]
        public async Task FindTaskById_Validation(int id, bool isValid)
        {
            var request = new FindTaskByIdRequest(id);

            var validator = new FindTaskByIdValidator();
            var validationResult = await validator.ValidateAsync(request);

            Assert.AreEqual(isValid, validationResult.IsValid);
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

        [DataTestMethod]
        [DataRow("", true, false)]
        [DataRow("TestTask", true, true)]
        [DataRow(null, false, false)]
        [DataRow(null, true, false)]
        public async Task SaveTaskList_Validation(string taskName, bool instantiateModel, bool isValid)
        {
            Domain.Models.Task model = null;
            if (instantiateModel)
            {
                model = new Domain.Models.Task { Name = taskName };
            }
            var request = new SaveTaskRequest(model);
            var validator = new SaveTaskValidator();
            var validationResult = await validator.ValidateAsync(request);
            Assert.AreEqual(isValid, validationResult.IsValid);
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

        [DataTestMethod]
        [DataRow(1, "TestTask", true, true)]
        [DataRow(1, null, true, false)]
        [DataRow(1, null, false, false)]
        [DataRow(0, "TestTask", true, false)]
        [DataRow(0, null, true, false)]
        [DataRow(0, null, false, false)]
        public async Task UpdateTaskList_Validation(int id, string taskName, bool instantiateModel, bool isValid)
        {
            Domain.Models.Task model = null;
            if (instantiateModel)
            {
                model = new Domain.Models.Task { Name = taskName };
            }
            var request = new UpdateTaskRequest(id, model);
            var validator = new UpdateTaskValidator();
            var validationResult = await validator.ValidateAsync(request);
            Assert.AreEqual(isValid, validationResult.IsValid);
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

        [DataTestMethod]
        [DataRow(0, false)]
        [DataRow(1, true)]
        [DataRow(3243243, true)]
        public async Task DeleteTask_Validation(int id, bool isValid)
        {
            var request = new DeleteTaskRequest(id);

            var validator = new DeleteTaskValidator();
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
