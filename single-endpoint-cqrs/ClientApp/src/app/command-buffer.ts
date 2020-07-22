import { Injectable, Inject, Injector } from "@angular/core";
import { ConnectivityState } from "./connectivity-state";
import { HttpClient } from "@angular/common/http";
import { ICommand, todoHandlers } from "./todo-command-handler";
import { of, from } from "rxjs";

@Injectable()
export class CommandBuffer {

  constructor(private connState: ConnectivityState
    , private http: HttpClient
    , @Inject('BASE_URL') private baseUrl: string
    , private  injector: Injector
  ) {}

  public commands: Array<ICommand> = [];

  issueCommand(command: ICommand) {
    const handler = this.findHandler(command);
    // Do a local imitation
    handler.imitate(command, this.injector);
    if (this.connState.isOnline) {
      this.sendCommand(command);
    }
    else {
      if (handler.isOnlineOnly) throw new Error(`Cannot execute ${command.type} while offline`);
      this.commands.push(command);
    }
  }

  private async sendCommand(cmd) {
    console.log('Sending command', cmd);
    const req = this.http.post(this.baseUrl + 'commands', cmd);
    try {
      await req.toPromise();
    }
    catch (e) {
      console.log('Command failed',e);
      alert('Command failed. You should refresh.');
    }
    console.log("Command successful", cmd);
  }

  private findHandler(command: ICommand) {
    const handler = todoHandlers[command.type];
    if(!handler) throw new Error(`No handler for ${command.type}`)
    return handler;
  }

  async flush() {
    if (!this.connState.isOnline) {
      throw new Error('Cannot flush commands when offline');
    }
    while (this.commands.length > 0) {
      const cmd = this.commands.unshift();
      await this.sendCommand(cmd);
    }
  }
}
