using SiccarCodeTest.Exceptions;
using SiccarCodeTest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SiccarCodeTestTests.Repositories
{
    public class InMemoryRepoTests
    {
        private readonly IRepository<TestObject> _underTest;
        public InMemoryRepoTests()
        {
            _underTest = new InMemoryRepo<TestObject>();
        }

        public class GetAll : InMemoryRepoTests
        {
            [Fact]
            public async Task Should_ReturnEmptyList_When_Empty()
            {
                var result = await _underTest.GetAll();

                Assert.Empty(result);
            }

            [Fact]
            public async Task Should_ReturnStored_Data()
            {
                var result = await _underTest.GetAll();

                Assert.Empty(result);
            }
        }

        public class InsertOrUpdate : InMemoryRepoTests
        {
            [Fact]
            public async Task Should_Add_Data()
            {
                var expected = new TestObject() { Id = Guid.NewGuid().ToString(), Data = "test-data" };

                await _underTest.InsertOrUpdate(expected.Id, expected);

                var data = await _underTest.GetAll();
                Assert.Contains(expected, data);
            }

            [Fact]
            public async Task Should_Update_Data()
            {
                var expected = new TestObject() { Id = Guid.NewGuid().ToString(), Data = "test-data" };
                var newData = new TestObject() { Id = expected.Id, Data = "Updated" };
                await _underTest.InsertOrUpdate(expected.Id, expected);

                await _underTest.InsertOrUpdate(expected.Id, newData);


                var data = await _underTest.GetById(expected.Id);
                Assert.Contains(expected.Id, data.Id);
                Assert.Contains("Updated", data.Data);
            }
        }

        public class GetById : InMemoryRepoTests
        {
            [Fact]
            public async Task Should_GetData_ById()
            {
                var expected = new TestObject() { Id = Guid.NewGuid().ToString(), Data = "test-data" };

                await _underTest.InsertOrUpdate(expected.Id, expected);

                var data = await _underTest.GetById(expected.Id);
                Assert.Equal(expected, data);
            }

            [Fact]
            public async Task Should_Throw_When_KeyDoesNotExist()
            {
                var expected = new TestObject() { Id = Guid.NewGuid().ToString(), Data = "test-data" };

                var exception = await Assert.ThrowsAsync<InMemoryRepoException>(async () => await _underTest.GetById(expected.Id));

                Assert.Contains(expected.Id, exception.Message);
            }
        }

        public class TestObject
        {
            public string Id { get; set; }
            public string Data { get; set; }
        }
    }
}
