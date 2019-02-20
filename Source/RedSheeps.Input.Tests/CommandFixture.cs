using Xunit;

namespace RedSheeps.Input.Tests
{
    public class CommandFixture
    {
        [Fact]
        public void ExecuteWhenNoArgumentsAndUnspecified()
        {
            var isCalled = false;
            var command = new Command(() => isCalled = true);
            command.Execute(null);
            Assert.True(isCalled);
        }

        [Fact]
        public void ExecuteWhenNoArgumentsAndNoArguments()
        {
            var isCalled = false;
            var command = new Command(() => isCalled = true, () => true);
            command.Execute(null);
            Assert.True(isCalled);
        }

        [Fact]
        public void ExecuteWhenObjectArgumentAndUnspecified()
        {
            object argument = null;
            var command = new Command(x => argument = x);
            var param = new object();
            command.Execute(param);
            Assert.Equal(param, argument);
        }

        [Fact]
        public void ExecuteWhenObjectArgumentAndObjectArgument()
        {
            object argument = null;
            var command = new Command(x => argument = x, x => true);
            var param = new object();
            command.Execute(param);
            Assert.Equal(param, argument);
        }

        [Fact]
        public void CanExecuteWhenNoArgumentsAndUnspecified()
        {
            var command = new Command(() => { });
            Assert.True(command.CanExecute(null));
        }

        [Fact]
        public void CanExecuteWhenNoArgumentsAndNoArguments()
        {
            var command = new Command(() => { }, () => false);
            Assert.False(command.CanExecute(null));
        }

        [Fact]
        public void CanExecuteWhenObjectArgumentAndUnspecified()
        {
            var command = new Command(_ => { });
            Assert.True(command.CanExecute(null));
        }

        [Fact]
        public void CanExecuteWhenObjectArgumentAndObjectArgument()
        {
            object argument = null;
            var command = new Command(x => argument = x, x =>
            {
                argument = x;
                return false;
            });
            var param = new object();
            Assert.False(command.CanExecute(param));
            Assert.Equal(param, argument);
        }

        [Fact]
        public void NotifyCanExecuteChanged()
        {
            var command = new Command(() => { });
            // Confirm that no exception is thrown
            command.NotifyCanExecuteChanged();

            var isCalledCanExecuteChanged = false;
            command.CanExecuteChanged += (sender, args) => isCalledCanExecuteChanged = true;
            command.NotifyCanExecuteChanged();
            Assert.True(isCalledCanExecuteChanged);
        }
    }
}