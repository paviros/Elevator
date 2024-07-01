using System;
using System.Collections.Generic;
					
public enum ElevatorDirection
{
    Idle,
    Up,
    Down
}

public class Elevator
{
    public int CurrentFloor { get; private set; }
    public ElevatorDirection Direction { get; private set; }
    public bool DoorsOpen { get; private set; }
	public List<int> destinations;

    public Elevator()
    {
        CurrentFloor = 1; 
        Direction = ElevatorDirection.Idle;
        DoorsOpen = false;
		destinations = new List<int>();
    }

    public void MoveToFloor(int destinationFloor)
    {
        if (destinationFloor == CurrentFloor)
            return;

        Direction = destinationFloor > CurrentFloor ? ElevatorDirection.Up : ElevatorDirection.Down;

        if (!destinations.Contains(destinationFloor))
        {
            destinations.Add(destinationFloor);
            destinations.Sort(); 
        }

        while (CurrentFloor != destinationFloor)
        {
            if (Direction == ElevatorDirection.Up)
                CurrentFloor++;
            else if (Direction == ElevatorDirection.Down)
                CurrentFloor--;

            Console.WriteLine(String.Format("Moving to floor {0}...", CurrentFloor));
        }

        OpenDoors();
    }

    public void OpenDoors()
    {
        Console.WriteLine("Doors opening...");
        DoorsOpen = true;
		CloseDoors();
    }

    public void CloseDoors()
    {
        Console.WriteLine("Closing door...");
        DoorsOpen = false;

        destinations.Remove(CurrentFloor);

        if (destinations.Count > 0)
        {
            MoveToFloor(destinations[0]);
        }
        else
        {
            Direction = ElevatorDirection.Idle;
			 Console.WriteLine("Waiting for input floor button...");
        }
    }

	public void CallElevator(int floor, ElevatorDirection direction)
    {
        Console.WriteLine(String.Format("Call from floor {0} to go {1}", floor, direction));

		if (floor == CurrentFloor)
        {
            OpenDoors();
            return;
        }

        if (Direction == ElevatorDirection.Idle)
        {
            MoveToFloor(floor);
        }
        else
        {
            if (direction == ElevatorDirection.Up && Direction == ElevatorDirection.Up && floor > CurrentFloor)
            {
                destinations.Add(floor);
                destinations.Sort();
            }
            else if (direction == ElevatorDirection.Down && Direction == ElevatorDirection.Down && floor < CurrentFloor)
            {
                destinations.Add(floor);
                destinations.Sort();
            }
        }
    }

    public void PressFloorButton(int floor)
    {
        Console.WriteLine(String.Format("Floor {0} button pressed inside the elevator.", floor));

        if (floor == CurrentFloor)
        {
            OpenDoors();
            return;
        }

        destinations.Add(floor);
        destinations.Sort();
        
        if (Direction == ElevatorDirection.Idle)
        {
            MoveToFloor(destinations[0]);
        }
    }
}

public class Program  
{  
	public static void Main(string[] args)  
	{  
		Elevator elevator = new Elevator();  

        elevator.CallElevator(3, ElevatorDirection.Up);  
        elevator.PressFloorButton(5);  
        elevator.PressFloorButton(2); 
	  elevator.PressFloorButton(4);  
	  elevator.CallElevator(3, ElevatorDirection.Down);
    }
}