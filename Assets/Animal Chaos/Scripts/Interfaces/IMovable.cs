using UnityEngine;

public interface IMovable {
    public int MoveSpeed { get; }

    public void Move();
}
