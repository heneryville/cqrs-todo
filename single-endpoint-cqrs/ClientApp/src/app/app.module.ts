import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { TodoComponent } from './todo/todo.component';
import { ConnectivityState } from './connectivity-state';
import { CommandBuffer } from './command-buffer';
import { LocalTodoState } from './local-todo-state';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    TodoComponent

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: TodoComponent, pathMatch: 'full' },
    ])
  ],
  providers: [
    ConnectivityState,
    CommandBuffer,
    LocalTodoState,
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(connState: ConnectivityState, cmdBuffer: CommandBuffer) {
    connState.isOnline$.subscribe(isOnline => {
      if (isOnline) {
        cmdBuffer.flush();
      }
    });
  }
}
