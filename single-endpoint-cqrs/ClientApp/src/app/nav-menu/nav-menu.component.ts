import { Component } from '@angular/core';
import { ConnectivityState } from '../connectivity-state';
import { CommandBuffer } from '../command-buffer';

@Component({
  selector: 'nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
})
export class NavMenuComponent {

  constructor(private connState: ConnectivityState, private cmdBuffer: CommandBuffer) {}

  toggle() {
    this.connState.toggleConnectivity();
  }

}
