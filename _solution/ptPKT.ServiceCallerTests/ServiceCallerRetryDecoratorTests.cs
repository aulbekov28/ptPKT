using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using ptPKT.ServiceCaller.ServiceCaller;
using ptPKT.ServiceCaller.ServiceCaller.Decorators;
using Xunit;

namespace ptPKT.ServiceCallerTests
{
    public class UnitTest1
    {
        [Fact]
        public async Task TaskManagerRetryDecorator_CalledTwiceOnFailure_ThrowsException()
        {
            // Arrange
            var tmServiceCaller = Substitute.For<IServiceCallWrapper>();
            var settingsService = Substitute.For<IConfiguration>();
            var logService = Substitute.For<ILogger>();

            settingsService["retryCount"].Returns("2");
            settingsService["retryInterval"].Returns("0");

            tmServiceCaller.WrapTmServiceCall(default, default).Throws(new Exception());

            var tmRetryDecorator = new RetryWrapTmServiceCallDecorator(tmServiceCaller, settingsService, logService);

            // Act
            // Assert
            await Assert.ThrowsAsync<Exception>(async () => await tmRetryDecorator.WrapTmServiceCall(default, default));

            _ = tmServiceCaller.Received(2).WrapTmServiceCall(default, default);
        }

        [Fact]
        public async Task TaskManagerRetryDecorator_CalledTwiceOnFailure_Success()
        {
            // Arrange
            var tmServiceCaller = Substitute.For<IServiceCallWrapper>();
            var settingsService = Substitute.For<IConfiguration>();
            var logService = Substitute.For<ILogger>();

            settingsService["retryCount"].Returns("2");
            settingsService["retryInterval"].Returns("0");

            var results = new Results<Task>(() => throw new Exception())
                .Then(Task.CompletedTask);

            tmServiceCaller.WrapTmServiceCall(default, default).Returns(x => results.Next());
            var tmRetryDecorator = new RetryWrapTmServiceCallDecorator(tmServiceCaller, settingsService, logService);

            // Act
            await tmRetryDecorator.WrapTmServiceCall(default, default);

            // Assert
            _ = tmServiceCaller.Received(2).WrapTmServiceCall(default, default);
        }
    }

    public class Results<T>
    {
        private readonly Queue<Func<T>> values = new Queue<Func<T>>();
        public Results(T result) { values.Enqueue(() => result); }
        public Results(Func<T> value) { values.Enqueue(value); }
        public Results<T> Then(T value) { return Then(() => value); }
        public Results<T> Then(Func<T> value)
        {
            values.Enqueue(value);
            return this;
        }
        public T Next() { return values.Dequeue()(); }
    }
}
