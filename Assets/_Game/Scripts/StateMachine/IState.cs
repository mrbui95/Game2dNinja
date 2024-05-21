using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void onEnter(Enemy enemy);
    public void onExecute(Enemy enemy);
    public void onExit(Enemy enemy);
}
