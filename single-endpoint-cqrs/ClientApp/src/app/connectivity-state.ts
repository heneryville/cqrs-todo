import { Injectable } from "@angular/core";
import { BehaviorSubject, Subject } from "rxjs";

@Injectable()
export class ConnectivityState {

  private isConnected$: BehaviorSubject<boolean> = new BehaviorSubject(true);

  toggleConnectivity() {
    this.isConnected$.next(!this.isOnline);
    console.log('You are now ', this.isOnline ? 'Online' : 'Offline');
  }

  get isOnline$() {
    return this.isConnected$.asObservable();
  }

  get isOnline() {
    return this.isConnected$.value;
  }
}
