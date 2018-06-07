using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_1 : MonoBehaviour {

	private IState_1 _currentlyRunningState;
	private IState_1 _previousState;

	public void ChangeState(IState_1 newState) {

		if(this._currentlyRunningState != null) {
			this._currentlyRunningState.Exit();
		}

		this._previousState = this._currentlyRunningState;
		this._currentlyRunningState = newState;
		this._currentlyRunningState = Enter();


	}

	public void ExecuteStateUpdate() {
		var _runningState = this._currentlyRunningState;
		
		if(_runningState != null) {
			_runningState.Execute();
		}


	}

	public void SwitchToPreviousState() {

		this._currentlyRunningState.Exit();
		this._currentlyRunningState = this._previousState;
		this._currentlyRunningState.Enter();


	}
}
