using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AggCharacter : AggregateRoot
{
    public string Id { get; set; }
    public string Image { get; set; }
    public string Name { get; set; }
    public IDirection direction { get; set; }

    public VOPosition currentPosition { get; set; }
    public VOPosition Move()
    {
        switch (direction)
        {
            case IDirection.LEFT:
                currentPosition.X -= 1.0;
                break;
            case IDirection.UP:
                currentPosition.Y -= 1.0;
                break;
            case IDirection.RIGHT:
                currentPosition.X += 1.0;
                break;
            case IDirection.DOWN:
                currentPosition.Y += 1.0;
                break;
        }
        return currentPosition;
    }

}
