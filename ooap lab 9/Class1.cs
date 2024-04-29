using System;
using System.Collections.Generic;

// Originator - class that creates Memento and can restore its state from Memento
class BrakeSystem
{
    private string leftSide;
    private string rightSide;

    public void SetLeftSide(string leftSide)
    {
        Console.WriteLine("Saving the state of the left side of the brake system...");
        this.leftSide = leftSide;
    }

    public void SetRightSide(string rightSide)
    {
        Console.WriteLine("Saving the state of the right side of the brake system...");
        this.rightSide = rightSide;
    }

    public Memento Save()
    {
        Console.WriteLine("Saving Memento...");
        return new Memento(leftSide);
    }

    public void Restore(Memento memento)
    {
        leftSide = memento.GetState();
        Console.WriteLine("Restoring the brake system state from Memento...");
    }

    public void ShowState()
    {
        Console.WriteLine($"Brake system state: Left side: {leftSide}, Right side: {rightSide}");
    }
}

// Memento - class that stores the state of the Originator
class Memento
{
    private readonly string state;

    public Memento(string state)
    {
        this.state = state;
    }

    public string GetState()
    {
        return state;
    }
}

// Caretaker - class responsible for storing and restoring Mementos
class Mechanic
{
    private readonly Stack<Memento> mementos = new Stack<Memento>();

    public void Backup(Memento memento)
    {
        Console.WriteLine("Mechanic saving Memento...");
        mementos.Push(memento);
    }

    public Memento Undo()
    {
        Console.WriteLine("Mechanic restoring previous state...");
        return mementos.Pop();
    }
}

class Program
{
    static void Main(string[] args)
    {
        BrakeSystem brakeSystem = new BrakeSystem();
        Mechanic mechanic = new Mechanic();

        // Performing work on the left side
        brakeSystem.SetLeftSide("Disc brakes");
        Memento memento = brakeSystem.Save();
        mechanic.Backup(memento);
        brakeSystem.ShowState();

        // Performing work on the right side
        brakeSystem.SetRightSide("Drum brakes");
        brakeSystem.ShowState();

        // Restoring state from Memento
        Memento undoMemento = mechanic.Undo();
        brakeSystem.Restore(undoMemento);
        brakeSystem.ShowState();
    }
}
