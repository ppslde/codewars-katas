namespace Kata.TheLift;
//https://www.codewars.com/kata/58905bfa1decb981da00009e/train/csharp
public static class Source
{
    private static int MinElevator(IEnumerable<int> list, int defaultValue)
    {
        if (list.Any())
            return list.Min();

        return defaultValue;
    }
    public static int[] TheLift(int[][] queues, int capacity)
    {
        var floorQueue = queues.Select((queue, floor) => (queue, floor))
            .ToDictionary(q => q.floor, q => q.queue.ToList());

        List<int> visitedFloors = new List<int>();
        List<int> elevator = new List<int>(capacity);

        var nextFloor = 0;
        var isDirectionUp = true;

        while (floorQueue.Values.Any(q => q.Any()) || elevator.Any() || nextFloor != 0)
        {
            var currentFloor = floorQueue.First(f => f.Key == nextFloor);

            visitedFloors.Add(currentFloor.Key);
            if (elevator.Any())
                elevator.RemoveAll(p => p == currentFloor.Key);

            if (isDirectionUp)
            {
                if (currentFloor.Value.Any(f => f > currentFloor.Key))
                {
                    // einsteigen
                    var potentials = currentFloor.Value.Where(p => p > currentFloor.Key)
                        .Select((q, i) => new { to = q, col = i })
                        .Take(elevator.Capacity - elevator.Count).ToArray();

                    elevator.AddRange(potentials.Select(p => p.to));
                    foreach (var p in potentials.Reverse())
                    {
                        currentFloor.Value.RemoveAt(p.col);
                    }

                    // UP
                    // next MIN(Floor<elevateor & UpperWarten>)
                    nextFloor = Math.Min(MinElevator(elevator, floorQueue.Keys.Max()), floorQueue.FirstOrDefault(f => f.Value.Any() && f.Key > currentFloor.Key)?.Key ?? floorQueue.Keys.Max());
                }
                else
                {
                    if (floorQueue.Any(f => f.Value.Any() && f.Key > currentFloor.Key))
                    {
                        // UP
                        // next MIN(Floor<elevateor & UpperWarten>)
                        nextFloor = Math.Min(MinElevator(elevator, floorQueue.Count - 1), floorQueue.First(f => f.Value.Any() && f.Key > currentFloor.Key).Key);
                    }
                    else
                    {
                        if (currentFloor.Value.Any(f => f < currentFloor.Key))
                        {
                            // einsteigen
                            var potentials = currentFloor.Value.Where(p => p < currentFloor.Key)
                                .Select((q, i) => new { to = q, col = i })
                                .Take(elevator.Capacity - elevator.Count).ToArray();

                            elevator.AddRange(potentials.Select(p => p.to));
                            foreach (var p in potentials.Reverse())
                            {
                                currentFloor.Value.RemoveAt(p.col);
                            }
                        }

                        // DOWN
                        isDirectionUp = false;

                        // next MAX(Floor<elevateor & UpperWarten>)
                        nextFloor = Math.Max(elevator.Max(), floorQueue.First(f => f.Value.Any() && f.Key < currentFloor.Key).Key);
                    }
                }
            }
            else
            {
                if (currentFloor.Value.Any(f => f < currentFloor.Key))
                {
                    // einsteigen
                    // DOWN
                    // next MAX(Floor<elevateor & UpperWarten>)
                }
                else
                {
                    if (floorQueue.Any(f => f.Value.Any() && f.Key < currentFloor.Key))
                    {
                        // DOWN
                        // next MAX(Floor<elevateor & UpperWarten>)
                    }
                    else
                    {
                        if (currentFloor.Value.Any(f => f > currentFloor.Key))
                        {
                            // einsteigen    
                        }

                        isDirectionUp = true;
                        // UP
                        // next MIN(Floor<elevateor & UpperWarten>)
                    }
                }
            }

            /*
                + wenn UP && WartenUP => einsteigen
                + wenn UP && NichtWartenUP, dann
                    + wenn UpperWarten => weiter UP
                    + wenn NichtUpperWarten && WartenDOWN => einsteigen und DOWN
                    + wenn NichtUpperWarten && NichtWartenDOWN => DOWN

                + wenn DOWN && WartenDOWN => einsteigen
                + wenn DOWN && NichtWartenDOWN, dann
                    + wenn LowerWarten => weiter DOWN
                    + wenn NichtLowerWarten && WartenUP => einsteigen und UP
                    + wenn NichtLowerWarten && 

             */

            //    int x = NextElevatorStop(floorQueue, elevator, isDirectionUp);
            //    isDirectionUp = x > nextFloor;

            //    if (current.Value.Any())
            //    {
            //        if (isDirectionUp)
            //        {

            //        }
            //        var potentials = current.Value.Where(p => isDirectionUp ? p > current.Key : p < current.Key)
            //            .Select((q, i) => new { to = q, col = i })
            //            .Take(elevator.Capacity - elevator.Count).ToArray();

            //        elevator.AddRange(potentials.Select(p => p.to));

            //        foreach (var p in potentials.Reverse())
            //        {
            //            current.Value.RemoveAt(p.col);
            //        }
            //    }
            //    else
            //    {

            //    }
        }

        // Your code here
        return visitedFloors.ToArray();
    }

    private static int NextElevatorStop(Dictionary<int, List<int>> floors, List<int> elevator, bool isDirectionUp)
    {
        if (isDirectionUp)
        {

        }


        if (elevator.Any())
            return isDirectionUp ? elevator.Min() : elevator.Max();

        if (!floors.Values.Any(f => f.Any()))
            return 0;

        return isDirectionUp ? floors.Keys.Max() : 0;
    }
}