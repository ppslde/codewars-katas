using FluentAssertions;
using Xunit;

namespace Kata.TheLift.Tests
{
    /// <summary>
    /// Lift Rules
    ///     + The Lift only goes up or down!
    ///     + Each floor has both UP and DOWN Lift-call buttons(except top and ground floors which have only DOWN and UP respectively)
    ///     + The Lift never changes direction until there are no more people wanting to get on/off in the direction it is already travelling
    ///     + When empty the Lift tries to be smart.For example,
    ///     + If it was going up then it may continue up to collect the highest floor person wanting to go down
    ///     + If it was going down then it may continue down to collect the lowest floor person wanting to go up
    ///     + The Lift has a maximum capacity of people
    ///     + When called, the Lift will stop at a floor even if it is full, although unless somebody gets off nobody else can get on!
    ///     + If the lift is empty, and no people are waiting, then it will return to the ground floor
    ///
    ///
    /// People Rules
    ///     + People are in "queues" that represent their order of arrival to wait for the Lift
    ///     + All people can press the UP/DOWN Lift-call buttons
    ///     + Only people going the same direction as the Lift may enter it
    ///     + Entry is according to the "queue" order, but those unable to enter do not block those behind them that can
    ///     + If a person is unable to enter a full Lift, they will press the UP/DOWN Lift-call button again after it has departed without them
    ///
    /// Input
    ///     + queues a list of queues of people for all floors of the building.
    ///     + The height of the building varies
    ///     + 0 = the ground floor
    ///     + Not all floors have queues
    ///     + Queue index [0] is the "head" of the queue
    ///     + Numbers indicate which floor the person wants go to
    ///     + capacity maximum number of people allowed in the lift
    /// </summary>
    public class SourceTests
    {
        [Fact]
        public void TestUp()
        {
            int[][] queues =
            {
                new int[0], // G
                new int[0], // 1
                new int[]{5,5,5}, // 2
                new int[0], // 3
                new int[0], // 4
                new int[0], // 5
                new int[0], // 6
            };
            var result = Source.TheLift(queues, 5);
            result.Should().Equal(new[] { 0, 2, 5, 0 });
        }

        [Fact]
        public void TestDown()
        {
            int[][] queues =
            {
                new int[0], // G
                new int[0], // 1
                new int[]{1,1}, // 2
                new int[0], // 3
                new int[0], // 4
                new int[0], // 5
                new int[0], // 6
            };
            var result = Source.TheLift(queues, 5);

            result.Should().Equal(new[] { 0, 2, 1, 0 });
        }

        [Fact]
        public void TestUpAndUp()
        {
            int[][] queues =
            {
                new int[0], // G
                new int[]{3}, // 1
                new int[]{4}, // 2
                new int[0], // 3
                new int[]{5}, // 4
                new int[0], // 5
                new int[0], // 6
            };
            var result = Source.TheLift(queues, 5);

            result.Should().Equal(new[] { 0, 1, 2, 3, 4, 5, 0 });
        }

        [Fact]
        public void TestDownAndDown()
        {
            int[][] queues =
            {
                new int[0], // G
                new int[]{0}, // 1
                new int[0], // 2
                new int[0], // 3
                new int[]{2}, // 4
                new int[]{3}, // 5
                new int[0], // 6
            };
            var result = Source.TheLift(queues, 5);

            result.Should().Equal(new[] { 0, 5, 4, 3, 2, 1, 0 });
        }
    }
}