using System;
using Xunit;

namespace RedSheeps.Input.Tests
{
    public class CommandTFixture
    {
        [Fact]
        public void ExecuteWhenGenericArgumentAndUnspecified()
        {
            object argument = null;
            var command = new Command<bool>(x => argument = x);
            command.Execute(true);
            Assert.Equal(true, argument);
        }

        [Fact]
        public void ExecuteWhenGenericArgumentAndGenericArgument()
        {
            object argument = null;
            var command = new Command<bool>(x => argument = x, x => true);
            command.Execute(true);
            Assert.Equal(true, argument);
            Assert.Throws<InvalidCastException>(() => command.Execute(string.Empty));
        }

        [Fact]
        public void CanExecuteWhenGenericArgumentAndUnspecified()
        {
            var command = new Command<bool>(_ => { });
            Assert.True(command.CanExecute(null));
        }

        [Fact]
        public void CanExecuteWhenGenericArgumentAndGenericArgument()
        {
            object argument = null;
            var command = new Command<bool>(x => argument = x, x =>
            {
                argument = x;
                return false;
            });
            Assert.False(command.CanExecute(true));
            Assert.Equal(true, argument);
            Assert.Throws<InvalidCastException>(() => command.Execute(string.Empty));
        }
    }
}